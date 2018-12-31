using Cometd.Bayeux;
using Cometd.Client.Transport;
using Cometd.Common;
using Cumulocity.SDK.Client.Rest.API.Notification.Common;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Transport
{
	public class MessageExchange
	{
		protected HttpListener _listener;
		private const int ASCII_SPACE = 0x20;
		private const string CHAR_SPACE = " ";
		private readonly HttpClient client;
		private readonly ITransportListener listener;
		private readonly IList<MessageExchangeListener> listeners = new List<MessageExchangeListener>();
		private readonly IList<IMutableMessage> messages;
		private readonly CumulocityLongPollingTransportMsgEx transport;
		private readonly UnauthorizedConnectionWatcher unauthorizedConnectionWatcher;
		private CancellationTokenSource cts = new CancellationTokenSource();
		private TimeSpan reconnectionWaitingTime = TimeSpan.FromMilliseconds(1000);
		private ConnectionHeartBeatWatcher watcher;

		public MessageExchange(CumulocityLongPollingTransportMsgEx transport,
							   ITransportListener listener,
							   ConnectionHeartBeatWatcher watcher,
							   UnauthorizedConnectionWatcher unauthorizedConnectionWatcher,
							   IList<IMutableMessage> messages)
		{
			this.transport = transport;
			this.listener = listener;
			this.messages = messages;
			this.watcher = watcher;
			this.unauthorizedConnectionWatcher = unauthorizedConnectionWatcher;

			this.client = new HttpClient();
		}

		public static int AsciiSpace => ASCII_SPACE;

		public static string CharSpace => CHAR_SPACE;
		public ITransportListener Listener => listener;
		public string ListeningAddress { get; private set; }
		public IList<IMutableMessage> Messages => messages;
		public TimeSpan ReconnectionWaitingTime { get => reconnectionWaitingTime; set => reconnectionWaitingTime = value; }
		public CumulocityLongPollingTransportMsgEx Transport => transport;
		public UnauthorizedConnectionWatcher UnauthorizedConnectionWatcher => unauthorizedConnectionWatcher;

		public ConnectionHeartBeatWatcher Watcher => watcher;

		public virtual void AddListener(IMessageExchangeListener listener)
		{
			listeners.Add((MessageExchangeListener)listener);
		}

		public virtual void Cancel()
		{
			Debug.WriteLine(String.Format("canceling {0}", JsonConvert.SerializeObject(messages)));

			listener.onException(new Exception("request cancelled"), ObjectConverter.ToListOfIMessage(messages));
			cts.Cancel();
			onFinish();
		}

		public virtual async Task<string> executeAsync(string url, string content)
		{
			StartWatcher();

			var eof = false;
			var data = Encoding.ASCII.GetBytes(content);

			HttpWebRequest _httpWebRequest = HttpWebRequest.Create(url) as HttpWebRequest;
			_httpWebRequest.Method = "POST";
			_httpWebRequest.Timeout = Timeout.Infinite;
			_httpWebRequest.ReadWriteTimeout = Timeout.Infinite;
			_httpWebRequest.KeepAlive = false;

			_httpWebRequest.ContentType = "application/json;charset=UTF-8";

			//
			//HTTPBasicAuthFilter
			String encoded = System.Convert.ToBase64String(
				System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(
					transport.Parameters.Principal + ":" + transport.Parameters.Password));
			_httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

			//HTTPBasicProxyAuthenticationFilter
			if (isProxyRequired(transport.Parameters))
			{
				IWebProxy proxy = new WebProxy(transport.Parameters.ProxyHost, transport.Parameters.ProxyPort);
				string proxyUsername = transport.Parameters.ProxyUserId;
				string proxyPassword = transport.Parameters.ProxyPassword;
				proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
				_httpWebRequest.Proxy = proxy;
			}

			//addCookieHeader
			if (_httpWebRequest.CookieContainer == null)
				_httpWebRequest.CookieContainer = new CookieContainer();
			_httpWebRequest.CookieContainer.Add(transport.GetCookieCollection());

			//addApplicationKeyHeader
			if (null != transport.Parameters.ApplicationKey)
			{
				_httpWebRequest.Headers.Add(RestConnector_Fields.X_CUMULOCITY_APPLICATION_KEY, transport.Parameters.ApplicationKey);
			}
			//
			try
			{
				using (var stream = _httpWebRequest.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				var response = (HttpWebResponse)_httpWebRequest.GetResponse();
				HttpWebResponseMessage httpWebResponseMessage;

				using (MemoryStream stream = new MemoryStream())
				using (BinaryReader reader = new BinaryReader(response.GetResponseStream()))
				{
					while (true)
					{
						if (true)
						{
							try
							{
								var b = reader.ReadChar();
								stream.Write(new byte[] { Convert.ToByte(b) }, 0, 1);

								if (CHAR_SPACE.Equals(b.ToString()))
								{
									Debug.WriteLine("HeartBeat");

									httpWebResponseMessage = new HttpWebResponseMessage(response.StatusCode, CHAR_SPACE);
									await ConsumeResponse(content, httpWebResponseMessage);
								}
							}
							catch (System.IO.EndOfStreamException ex)
							{
								var exMsg = ex.Message;
								Debug.WriteLine("EndOfStreamException");
								eof = true;
							}

							if (eof)
							{
								var result = Encoding.ASCII.GetString(stream.ToArray());
								Debug.WriteLine(result);
								httpWebResponseMessage = new HttpWebResponseMessage(response.StatusCode, result);

								break;
							}
						}
					}
				}

				await ConsumeResponse(content, httpWebResponseMessage);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(content);
				UnauthorizedConnectionWatcher.resetCounter();
				listener.onConnectException(ex, ObjectConverter.ToListOfIMessage(messages));
				onFinish();
			}
			return String.Empty;
		}

		public void onFinish()
		{
			foreach (var listener in listeners)
			{
				listener.onFinish();
			}

			Debug.WriteLine(String.Format("stopping heartbeat watcher {0}", JsonConvert.SerializeObject(messages)));
			Watcher.stop();
		}

		public virtual void removeListener(IMessageExchangeListener listener)
		{
			//listeners.Remove(listener);
		}

		private static bool isProxyRequired(PlatformParameters platformParameters)
		{
			return !String.IsNullOrEmpty(platformParameters.ProxyHost) && (platformParameters.ProxyPort > 0);
		}

		private async Task ConsumeResponse(string content, HttpWebResponseMessage httpWebResponseMessage)
		{
			try
			{
				ResponseConsumer responseConsumer = new ResponseConsumer(httpWebResponseMessage, this);
				await responseConsumer.RunAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(content);
				UnauthorizedConnectionWatcher.resetCounter();
				listener.onConnectException(ex, ObjectConverter.ToListOfIMessage(messages));
				onFinish();
			}
		}

		private void StartWatcher()
		{
			Debug.WriteLine(String.Format("{0} starting heartbeat watcher {1}", DateTime.Now.ToString("HH:mm:ss.fff"), JsonConvert.SerializeObject(messages)));
			Watcher.Start();
		}
	}

	internal class HttpWebResponseMessage
	{
		public HttpWebResponseMessage(HttpStatusCode StatusCode, String Content)
		{
			this.StatusCode = StatusCode;
			this.Content = Content;
		}

		public String Content { get; }

		public bool IsSuccessStatusCode
		{
			get { return ((int)StatusCode >= 200) && ((int)StatusCode <= 299); }
		}

		public HttpStatusCode StatusCode { get; }
	}

	internal sealed class ResponseConsumer
	{
		private readonly MessageExchange messageExchange;
		private readonly HttpWebResponseMessage response;

		public ResponseConsumer(HttpWebResponseMessage response, MessageExchange messageExchange)
		{
			this.response = response;
			this.messageExchange = messageExchange;
		}

		public async Task RunAsync()
		{
			bool isHeartBeat = false;
			try
			{
				Debug.WriteLine(String.Format("{0}Start response consumer ", DateTime.Now.ToString("HH:mm:ss.fff")));

				isHeartBeat = HeartBeatWatch(response, response.Content);
				if (isHeartBeat)
					return;
				GetMessagesFromResponse(response, response.Content);
			}
			catch (Exception e)
			{
				onConnectionFailed(e);
			}
			finally
			{
				try
				{
					if (!isHeartBeat)
						messageExchange.onFinish();
				}
				finally
				{
					//response.Dispose();
				}
			}
		}

		//ORIGINAL LINE: private void getHeartBeats(final ClientResponse response) throws IOException
		private bool GetHeartBeats(string memoryStream)
		{
			Debug.WriteLine(String.Format(" {0} getting heartbeants  {1}", DateTime.Now.ToString("HH:mm:ss.fff"), JsonConvert.SerializeObject(response)));

			{
				if (isHeartBeat(memoryStream))
				{
					Debug.WriteLine("recived heartbeat");
					messageExchange.Watcher.HeartBeat();
					return true;
				}
				else
				{
					Debug.WriteLine("new messages recived");
					return false;
				}
			}
		}

		private void GetMessagesFromResponse(HttpWebResponseMessage clientResponse, string memoryStream)
		{
			if (isOk(clientResponse))
			{
				string content = memoryStream;
				if (!isNullOrEmpty(content))
				{
					try
					{
						handleContent(content);
					}
					catch (Exception x)
					{
						onException(x);
					}
				}
				else
				{
					onTransportException(204);
				}
			}
			else
			{
				onTransportException((int)clientResponse.StatusCode);
			}
		}

		//ORIGINAL LINE: private void handleContent(String content) throws ParseException
		private void handleContent(string content)
		{
			IList<IMutableMessage> messages = DictionaryMessage.parseMessages(content);
			Debug.WriteLine(String.Format("{0} received messages {1} ", DateTime.Now.ToString("HH:mm:ss.fff"), JsonConvert.SerializeObject(messages)));
			messageExchange.Listener.onMessages(messages);
		}

		//ORIGINAL LINE: private void heartBeatWatch(ClientResponse clientResponse) throws IOException
		private bool HeartBeatWatch(HttpWebResponseMessage clientResponse, string memoryStream)
		{
			if (isOk(clientResponse))
			{
				//if (!isCanGetHeatBeats(clientResponse))
				return GetHeartBeats(memoryStream);
			}
			return false;
		}

		private bool isHeartBeat(string value)
		{
			return value == MessageExchange.CharSpace;
		}

		private bool isNullOrEmpty(string content)
		{
			return String.IsNullOrEmpty(content);
		}

		private bool isOk(HttpWebResponseMessage clientResponse)
		{
			return clientResponse.IsSuccessStatusCode;
		}

		private void onConnectionFailed(Exception e)
		{
			Debug.WriteLine("connection failed");
			messageExchange.UnauthorizedConnectionWatcher.resetCounter();
			messageExchange.Listener.onConnectException(e, ObjectConverter.ToListOfIMessage(messageExchange.Messages));
		}

		private void onException(Exception x)
		{
			Debug.WriteLine(String.Format("request failed {0}", JsonConvert.SerializeObject(x)));
			waitBeforeAnotherReconnect();
			messageExchange.Listener.onException(x, ObjectConverter.ToListOfIMessage(messageExchange.Messages));
		}

		//WARNING: 'final' parameters are not available in .NET:
		private void onException(int code)
		{
			IDictionary<string, object> failure = new Dictionary<string, object>(2);
			failure["httpCode"] = code;
			onException(new TransportException(failure));
		}

		private void onTransportException(int code)
		{
			Debug.WriteLine(String.Format("request failed with code {0}", code));
			if (code == 401)
			{
				messageExchange.UnauthorizedConnectionWatcher.unauthorizedAccess();
				if (messageExchange.UnauthorizedConnectionWatcher.shouldRetry())
				{
					onException(code);
				}
			}
			else
			{
				onException(code);
			}
		}

		private void waitBeforeAnotherReconnect()
		{
			try
			{
				Thread.Sleep(messageExchange.ReconnectionWaitingTime);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(String.Format("Problem occurred while waiting for another bayeux reconnect {0}", ex.Message));
			}
		}
	}
}