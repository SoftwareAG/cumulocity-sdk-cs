using Cometd.Bayeux;
using Cometd.Client.Transport;
using Cometd.Common;
using Cumulocity.SDK.Client.Rest.API.Notification.Common;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Transport
{
	public class CumulocityLongPollingTransport : HttpClientTransport
	{
		private readonly PlatformParameters parameters;
		private readonly UnauthorizedConnectionWatcher unauthorizedConnectionWatcher;

		//private bool _aborted;
		private bool _appendMessageType;

		private List<TransportExchange> _exchanges = new List<TransportExchange>();
		private ManualResetEvent ready = new ManualResetEvent(true);

		private HashSet<LongPollingRequest> transmissions = new HashSet<LongPollingRequest>();
		private List<LongPollingRequest> transportQueue = new List<LongPollingRequest>();

		public CumulocityLongPollingTransport(IDictionary<String, Object> options, PlatformParameters platformParameters, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher)
									: base("long-polling", options)
		{
			this.parameters = platformParameters;
			this.unauthorizedConnectionWatcher = unauthorizedConnectionWatcher;
			this.OptionPrefix = "long-polling.json";
		}

		public override bool isSending
		{
			get
			{
				lock (this)
				{
					if (transportQueue.Count > 0)
						return true;

					foreach (var transmission in transmissions)
						if (transmission.exchange.isSending)
							return true;

					return false;
				}
			}
		}

		public override void abort()
		{
			//_aborted = true;
			lock (this)
			{
				foreach (TransportExchange exchange in _exchanges)
					exchange.Abort();

				_exchanges.Clear();
			}
		}

		public override bool accept(String bayeuxVersion)
		{
			return true;
		}

		public void addRequest(LongPollingRequest request)
		{
			lock (this)
			{
				transportQueue.Add(request);
			}

			performNextRequest();
		}

		public override void init()
		{
			base.init();
			//_aborted = false;
			Regex uriRegex = new Regex("(^https?://(([^:/\\?#]+)(:(\\d+))?))?([^\\?#]*)(.*)?");
			Match uriMatch = uriRegex.Match(getURL());
			if (uriMatch.Success)
			{
				String afterPath = uriMatch.Groups[7].ToString();
				_appendMessageType = afterPath == null || afterPath.Trim().Length == 0;
			}
		}

		public void removeRequest(LongPollingRequest request)
		{
			lock (this)
			{
				transmissions.Remove(request);
			}

			performNextRequest();
		}

		public override void reset()
		{
		}

		public override void send(ITransportListener listener, IList<IMutableMessage> messages)
		{
			//Console.WriteLine();
			//Console.WriteLine("send({0} message(s))", messages.Count);
			String url = getURL();

			if (_appendMessageType && messages.Count == 1 && messages[0].Meta)
			{
				String type = messages[0].Channel.Substring(Channel_Fields.META.Length);
				if (url.EndsWith("/"))
					url = url.Substring(0, url.Length - 1);
				url += type;
			}

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.ContentType = "application/json;charset=UTF-8";

			//HTTPBasicAuthFilter
			//"Basic bWFuYWdlbWVudC9hZG1pbjpjOHk="
			String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(this.parameters.Principal + ":" + this.parameters.Password));
			request.Headers.Add("Authorization", "Basic " + encoded);

			//HTTPBasicProxyAuthenticationFilter
			if (isProxyRequired(this.parameters))
			{
				IWebProxy proxy = new WebProxy(this.parameters.ProxyHost, this.parameters.ProxyPort);
				string proxyUsername = this.parameters.ProxyUserId;
				string proxyPassword = this.parameters.ProxyPassword;
				proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
				request.Proxy = proxy;
			}

			//addCookieHeader
			if (request.CookieContainer == null)
				request.CookieContainer = new CookieContainer();
			request.CookieContainer.Add(getCookieCollection());

			//addApplicationKeyHeader
			if (null != this.parameters.ApplicationKey)
			{
				request.Headers.Add(RestConnector_Fields.X_CUMULOCITY_APPLICATION_KEY, this.parameters.ApplicationKey);
			}

			String content = JsonConvert.SerializeObject(ObjectConverter.ToListOfDictionary(messages));

			LongPollingRequest longPollingRequest = new LongPollingRequest(listener, messages, request);

			TransportExchange exchange = new TransportExchange(this, listener, messages, longPollingRequest);
			exchange.content = content;
			exchange.request = request;
			lock (this)
			{
				_exchanges.Add(exchange);
			}

			longPollingRequest.exchange = exchange;
			addRequest(longPollingRequest);
		}

		private static bool isProxyRequired(PlatformParameters platformParameters)
		{
			return !String.IsNullOrEmpty(platformParameters.ProxyHost) && (platformParameters.ProxyPort > 0);
		}

		// From http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetrequeststream.aspx
		private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{
			TransportExchange exchange = (TransportExchange)asynchronousResult.AsyncState;

			try
			{
				// End the operation
				using (Stream postStream = exchange.request.EndGetRequestStream(asynchronousResult))
				{
					// Convert the string into a byte array.
					byte[] byteArray = Encoding.UTF8.GetBytes(exchange.content);
					//System.Diagnostics.Debug.WriteLine("Sending message(s): {0}", exchange.content);

					Debug.WriteLine(String.Format("{0} sending messages {1} ", DateTime.Now.ToString("HH:mm:ss.fff"), JsonConvert.SerializeObject(exchange.content)));

					// Write to the request stream.
					postStream.Write(byteArray, 0, exchange.content.Length);
					postStream.Close();
				}

				// Start the asynchronous operation to GetFirstPage the response
				exchange.listener.onSending(ObjectConverter.ToListOfIMessage(exchange.messages));
				IAsyncResult result = (IAsyncResult)exchange.request.BeginGetResponse(new AsyncCallback(GetResponseCallback), exchange);
				// A new connection is established after every configured timeout milliseconds.
				long timeout = 5400000;
				ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), exchange, timeout, true);

				exchange.isSending = false;
			}
			catch (Exception e)
			{
				if (exchange.request != null) exchange.request.Abort();
				exchange.Dispose();
				exchange.listener.onException(e, ObjectConverter.ToListOfIMessage(exchange.messages));
			}
		}

		private static void GetResponseCallback(IAsyncResult asynchronousResult)
		{
			TransportExchange exchange = (TransportExchange)asynchronousResult.AsyncState;

			try
			{
				// End the operation
				string responseString;
				using (HttpWebResponse response = (HttpWebResponse)exchange.request.EndGetResponse(asynchronousResult))
				{
					using (Stream streamResponse = response.GetResponseStream())
					{
						using (StreamReader streamRead = new StreamReader(streamResponse))
						{
							var peeker = streamRead.Peek();
							if (peeker == 32)
							{
								responseString = " ";
								Console.WriteLine("HeartBeat");
							}
							else
							{
								responseString = streamRead.ReadToEnd();
							}
						}
					}
					//Console.WriteLine("Received message(s): {0}", responseString);

					if (response.Cookies != null)
						foreach (Cookie cookie in response.Cookies)
							exchange.AddCookie(cookie);

					response.Close();
				}

				Console.WriteLine(responseString);
				exchange.messages = DictionaryMessage.parseMessages(responseString);

				exchange.listener.onMessages(exchange.messages);
				exchange.Dispose();
			}
			catch (Exception e)
			{
				exchange.listener.onException(e, ObjectConverter.ToListOfIMessage(exchange.messages));
				exchange.Dispose();
			}
		}

		// From http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetresponse.aspx
		// Abort the request if the timer fires.
		private static void TimeoutCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				Console.WriteLine("Timeout");
				TransportExchange exchange = state as TransportExchange;

				if (exchange.request != null) exchange.request.Abort();
				exchange.Dispose();
			}
		}

		private void performNextRequest()
		{
			bool ok = false;
			LongPollingRequest nextRequest = null;

			lock (this)
			{
				if (transportQueue.Count > 0 && transmissions.Count <= 1)
				{
					ok = true;
					nextRequest = transportQueue[0];
					transportQueue.Remove(nextRequest);
					transmissions.Add(nextRequest);
				}
			}

			if (ok && nextRequest != null)
			{
				nextRequest.send();
			}
		}

		// Fix for not running more than two simultaneous requests:
		public class LongPollingRequest
		{
			public TransportExchange exchange;
			private ITransportListener listener;
			private IList<IMutableMessage> messages;
			private HttpWebRequest request;

			public LongPollingRequest(ITransportListener _listener, IList<IMutableMessage> _messages,
					HttpWebRequest _request)
			{
				listener = _listener;
				messages = _messages;
				request = _request;
			}

			public void send()
			{
				try
				{
					request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), exchange);
				}
				catch (Exception e)
				{
					exchange.Dispose();
					listener.onException(e, ObjectConverter.ToListOfIMessage(messages));
				}
			}
		}

		public class TransportExchange
		{
			public String content;
			public bool isSending;
			public ITransportListener listener;
			public LongPollingRequest lprequest;
			public IList<IMutableMessage> messages;
			public HttpWebRequest request;
			private CumulocityLongPollingTransport parent;

			public TransportExchange(CumulocityLongPollingTransport _parent, ITransportListener _listener, IList<IMutableMessage> _messages,
					LongPollingRequest _lprequest)
			{
				parent = _parent;
				listener = _listener;
				messages = _messages;
				request = null;
				lprequest = _lprequest;
				isSending = true;
			}

			public void Abort()
			{
				if (request != null) request.Abort();
			}

			public void AddCookie(Cookie cookie)
			{
				parent.addCookie(cookie);
			}

			public void Dispose()
			{
				parent.removeRequest(lprequest);
				lock (parent)
					parent._exchanges.Remove(this);
			}
		}
	}
}