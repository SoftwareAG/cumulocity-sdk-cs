using Cometd.Bayeux;
using Cometd.Client.Transport;
using Cometd.Common;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Transport
{
	public class CumulocityLongPollingTransportMsgEx : HttpClientTransport
	{
		private readonly object _object = new object();
		private readonly PlatformParameters parameters;
		private readonly UnauthorizedConnectionWatcher unauthorizedConnectionWatcher;

		private bool _aborted;
		private bool _appendMessageType;

		private List<MessageExchange> _exchanges = new List<MessageExchange>();

		public override bool isSending => throw new NotImplementedException();

		public PlatformParameters Parameters => parameters;

		public CumulocityLongPollingTransportMsgEx(IDictionary<String, Object> options, PlatformParameters platformParameters, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher)
									: base("long-polling", options)
		{
			this.parameters = platformParameters;
			this.unauthorizedConnectionWatcher = unauthorizedConnectionWatcher;
			this.OptionPrefix = "long-polling.json";
		}

		public override bool accept(String bayeuxVersion)
		{
			return true;
		}

		public override void init()
		{
			base.init();
			_aborted = false;
			Regex uriRegex = new Regex("(^https?://(([^:/\\?#]+)(:(\\d+))?))?([^\\?#]*)(.*)?");
			Match uriMatch = uriRegex.Match(getURL());
			if (uriMatch.Success)
			{
				String afterPath = uriMatch.Groups[7].ToString();
				_appendMessageType = afterPath == null || afterPath.Trim().Length == 0;
			}
		}

		public override void abort()
		{
			_aborted = true;
			List<MessageExchange> exchanges = new List<MessageExchange>();

			Boolean _lockTaken = false;

			Monitor.Enter(_object, ref _lockTaken);
			try
			{
				exchanges.AddRange(_exchanges);
				this._exchanges.Clear();
			}
			finally
			{
				if (_lockTaken)
				{
					Monitor.Exit(_object);
				}
			}

			foreach (MessageExchange exchange in exchanges)
				exchange.Cancel();
		}

		public override void reset()
		{
		}

		private System.Threading.SemaphoreSlim msgSemaphoreSlim = new SemaphoreSlim(1);

		public override void send(ITransportListener listener, IList<IMutableMessage> messages)
		{
			Debug.WriteLine(String.Format("{0} sending messages {1} ", DateTime.Now.ToString("HH:mm:ss.fff"), JsonConvert.SerializeObject(messages)));

			var result = CallCreateMessageExchange(listener, messages);
		}

		private async Task<bool> CallCreateMessageExchange(ITransportListener listener, IList<IMutableMessage> messages)
		{
			var result = false;
			String content = JsonConvert.SerializeObject(ObjectConverter.ToListOfDictionary(messages));
			try
			{
				await msgSemaphoreSlim.WaitAsync();
				try
				{
					verifyState();
					createMessageExchangeAsync(listener, content, messages);
					result = true;
				}
				finally
				{
					msgSemaphoreSlim.Release();
				}
			}
			catch (Exception x)
			{
				listener.onException(x, ObjectConverter.ToListOfIMessage(messages));
			}
			return result;
		}

		public CookieCollection GetCookieCollection()
		{
			return base.getCookieCollection();
		}

		//ORIGINAL LINE: private void createMessageExchange(final TransportListener listener, final String content, Message.Mutable... messages)
		private async void createMessageExchangeAsync(ITransportListener listener, string content, IList<IMutableMessage> messages)
		{
			//ORIGINAL LINE: final ConnectionHeartBeatWatcher watcher = new ConnectionHeartBeatWatcher(executorService);
			ConnectionHeartBeatWatcher watcher = new ConnectionHeartBeatWatcher(12, 11);
			//ORIGINAL LINE: final MessageExchange exchange = new MessageExchange(this, httpClient, executorService, listener, watcher, unauthorizedConnectionWatcher, messages);
			MessageExchange exchange = new MessageExchange(this, listener, watcher, unauthorizedConnectionWatcher, messages);
			watcher.AddConnectionListener(new ConnectionIdleListener(() => { msgSemaphoreSlim.Release(); exchange.Cancel(); }));

			exchange.AddListener(new MessageExchangeListener(() =>
			{
				lock (this._exchanges)
				{ this._exchanges.Remove(exchange); }
			}
			));
			_exchanges.Add(exchange);

			String url = getURL();

			if (_appendMessageType && messages.Count == 1 && messages[0].Meta)
			{
				String type = messages[0].Channel.Substring(Channel_Fields.META.Length);
				if (url.EndsWith("/"))
					url = url.Substring(0, url.Length - 1);
				url += type;
			}
			await exchange.executeAsync(url, content);
		}

		private void verifyState()
		{
			if (_aborted)
			{
				throw new System.InvalidOperationException("Aborted");
			}
		}
	}
}