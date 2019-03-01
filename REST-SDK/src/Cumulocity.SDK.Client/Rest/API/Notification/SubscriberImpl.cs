using Cometd.Bayeux;
using Cometd.Bayeux.Client;
using Cumulocity.SDK.Client.Rest.API.Notification.Exceptions;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Notification.Subscriber;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Cumulocity.SDK.Client.Logging;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
#pragma warning disable CS0618
	public class SubscriberImpl<T> : ISubscriber<T, IMessage>, IConnectionListener
	{
		private static readonly ILog LOG = LogProvider.For<SubscriberImpl<T>>();

		private readonly Interfaces.ISubscriptionNameResolver<T> subscriptionNameResolver;
		private readonly Interfaces.IBayeuxSessionProvider bayeuxSessionProvider;
		private readonly ThreadSafeList<SubscriptionRecord<T>> subscriptions = new ThreadSafeList<SubscriptionRecord<T>>();
		private readonly ThreadSafeList<SubscriptionRecord<T>> pendingSubscriptions = new ThreadSafeList<SubscriptionRecord<T>>();
		private readonly object @lock = new object();
		private volatile Cometd.Bayeux.Client.IClientSession session;

		//ORIGINAL LINE: public SubscriberImpl(SubscriptionNameResolver<T> channelNameResolver, BayeuxSessionProvider bayeuxSessionProvider, final UnauthorizedConnectionWatcher unauthorizedConnectionWatcher)
		public SubscriberImpl(ISubscriptionNameResolver<T> channelNameResolver, IBayeuxSessionProvider bayeuxSessionProvider, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher)
		{
			this.subscriptionNameResolver = channelNameResolver;
			this.bayeuxSessionProvider = bayeuxSessionProvider;
			unauthorizedConnectionWatcher.AddListener(this);
		}

		public virtual void start()
		{
			LOG.Trace("starting new subscriber");
			checkState(!Connected, "subscriber already started");
			session = bayeuxSessionProvider.Get();
		}

		public bool isHandshake(IMutableMessage message)
		{
			return Channel_Fields.META_HANDSHAKE.Equals(message.Channel);
		}

		public bool isSuccessfulConnected(IMutableMessage message)
		{
			return Channel_Fields.META_CONNECT.Equals(message.Channel) && message.Successful;
		}
		public bool isSuccessfulHandshake(IMutableMessage message)
		{
			return Channel_Fields.META_HANDSHAKE.Equals(message.Channel) && message.Successful;
		}


		public virtual ISubscription<T> Subscribe(T @object, ISubscriptionListener<T, IMessage> handler)
		{
			return this.Subscribe(@object, new LoggingSubscribeOperationListener(), handler, true);
		}

		public virtual ISubscription<T> Subscribe(T @object, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<T, IMessage> handler, bool autoRetry)
		{
			return subscribe(@object, subscribeOperationListener, handler, autoRetry, 0);
		}

		internal virtual ISubscription<T> subscribe(T @object, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<T, IMessage> handler, bool autoRetry, int retriesCount)
		{
			checkArgument(!Helpers.IsNullOrDefault<T>(@object), "object can't be null");
			checkArgument(handler != null, "handler can't be null");
			checkArgument(subscribeOperationListener != null, "subscribeOperationListener can't be null");

			ensureConnection();

			IClientSessionChannel channel = getChannel(@object);
			LOG.Debug("subscribing to channel {}", channel.Id);

			var listener = new MessageListenerAdapter<T>(handler, channel, @object);

			IClientSessionChannel metaSubscribeChannel = session.getChannel(Channel_Fields.META_SUBSCRIBE);
			var subscriptionRecord = new SubscriptionRecord<T>(@object, handler, subscribeOperationListener, this);
			var subscriptionResultListener = new SubscriptionResultListener<T>(subscriptionRecord, listener, subscribeOperationListener, channel, autoRetry, retriesCount, this);
			metaSubscribeChannel.addListener(subscriptionResultListener);
			channel.subscribe(listener);
			if (autoRetry)
			{
				pendingSubscriptions.Add(subscriptionRecord);
			}
			return listener.Subscription;
		}

		private void ensureConnection()
		{
			lock (@lock)
			{
				if (!Connected)
				{
					start();
					session.addExtension(new ReconnectOnSuccessfulConnected<T>(this));
				}
			}
		}

		private bool Connected
		{
			get
			{
				return session != null;
			}
		}

		private IClientSessionChannel getChannel(T @object)
		{
			string channelId = subscriptionNameResolver.Apply(@object);
			checkState(!string.ReferenceEquals(channelId, null) && channelId.Length > 0, "channelId is null or empty for object : " + @object);
			return session.getChannel(channelId);
		}

		public void Disconnect()
		{
			lock (@lock)
			{
				if (Connected)
				{
					subscriptions.Clear();
					session.disconnect();
					session = null;
				}
			}
		}

		private void checkState(bool state, string message)
		{
			if (!state)
			{
				throw new System.InvalidOperationException(message);
			}
		}

		private void checkArgument(bool state, string message)
		{
			if (!state)
			{
				throw new System.ArgumentException(message);
			}
		}

		public void resubscribe()
		{
			var allSubscriptions = new ThreadSafeList<SubscriptionRecord<T>>();
			allSubscriptions.Concat(subscriptions);
			allSubscriptions.Concat(pendingSubscriptions);

			foreach (var subscribed in allSubscriptions)
			{
				ISubscription<T> subscription = Subscribe(subscribed.Id, subscribed.subscribeOperationListener, subscribed.Listener, true);
				try
				{
					subscribed.Listener.OnError(subscription, new ReconnectedSDKException("bayeux client reconnected clientId: " + session.Id));
				}
				catch (Exception e)
				{
					LOG.Warn("Error when executing OnError of listener: {}, {}", subscribed.Listener, e.Message);
				}
			}
		}
		public void OnDisconnection(int httpCode)
		{
			foreach (var subscription in subscriptions)
			{
				subscription.Listener.OnError(new DummySubscription<T>(subscription), new SDKException(httpCode, "bayeux client disconnected  clientId: " + session.Id));
			}
		}

		public bool removeSubscriptions(SubscriptionRecord<T> item)
		{
			return subscriptions.Remove(item);
		}

		public bool removePendingSubscriptions(SubscriptionRecord<T> item)
		{
			return pendingSubscriptions.Remove(item);
		}

		public void AddSubscription(SubscriptionRecord<T> item)
		{
			this.subscriptions.Add(item);
		}

		public IClientSessionChannel GetUnsubscribeChannel()
		{
			return session.getChannel(Channel_Fields.META_UNSUBSCRIBE);
		}

		public string GetSubscriptionName(T o)
		{
			return subscriptionNameResolver.Apply(o);
		}
	}
}