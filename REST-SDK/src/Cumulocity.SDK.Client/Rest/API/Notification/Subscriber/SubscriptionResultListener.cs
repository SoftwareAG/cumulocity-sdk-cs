using Cometd.Bayeux;
using Cometd.Bayeux.Client;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{

	public sealed class SubscriptionResultListener<T> : IMessageListener
	{
		private const int RETRIES_ON_SHORT_NETWORK_FAILURES = 5;
		private readonly ISubscribeOperationListener subscribeOperationListener;
		private readonly MessageListenerAdapter<T> listener;
		private readonly IClientSessionChannel channel;
		private readonly SubscriptionRecord<T> subscription;
		private readonly bool autoRetry;
		private readonly int retriesCount;
		private readonly SubscriberImpl<T> subscriberImpl;

		public SubscriptionResultListener(SubscriptionRecord<T> subscribed, MessageListenerAdapter<T> listener, ISubscribeOperationListener subscribeOperationListener, IClientSessionChannel channel, bool autoRetry, int retriesCount, SubscriberImpl<T> subscriberImpl)
		{
			this.subscription = subscribed;
			this.listener = listener;
			this.subscribeOperationListener = subscribeOperationListener;
			this.channel = channel;
			this.autoRetry = autoRetry;
			this.retriesCount = retriesCount;
			this.subscriberImpl = subscriberImpl;
		}

		public void onMessage(IClientSessionChannel metaSubscribeChannel, IMessage message)
		{
			if (!Channel_Fields.META_SUBSCRIBE.Equals(metaSubscribeChannel.Id))
			{
				// Should never be here
				//log.warn("Unexpected message to wrong channel, to SubscriptionSuccessListener: {}, {}", metaSubscribeChannel, message);
				return;
			}
			if (message.Successful && !isSubscriptionToChannel(message))
			{
				return;
			}
			try
			{
				if (message.Successful)
				{
					//log.debug("subscribed successfully to channel {}, {}", this.channel, message);
					if (autoRetry)
					{
						subscriberImpl.removePendingSubscriptions(subscription);
					}
					IClientSessionChannel unsubscribeChannel = subscriberImpl.GetUnsubscribeChannel();
					unsubscribeChannel.addListener(new UnsubscribeListener<T>(subscription, unsubscribeChannel, subscriberImpl));
					subscriberImpl.AddSubscription(subscription);
					subscribeOperationListener.OnSubscribingSuccess(this.channel.Id);
				}
				else
				{
					//log.debug("Error subscribing channel: {}, {}", this.channel.Id, message);
					handleError(message);
				}
			}
			catch (NullReferenceException ex)
			{
				//log.warn("NPE on message {} - {}", message, Channel_Fields.META_SUBSCRIBE);
				throw new Exception(ex.Message);
			}
			finally
			{
				metaSubscribeChannel.removeListener(this);
			}
		}

		private bool isSubscriptionToChannel(IMessage message)
		{
			return String.Equals(channel.Id, message[Message_Fields.SUBSCRIPTION_FIELD]);
		}

		private void handleError(IMessage message)
		{
			if (autoRetry && isShortNetworkFailure(message))
			{
				if (retriesCount > RETRIES_ON_SHORT_NETWORK_FAILURES)
				{
					//log.error("Detected a short network failure, giving up after {} retries. " + "Another retry attempt only happen on another successfully handshake", retriesCount);
				}
				else
				{
					//log.debug("Detected a short network failure, retrying to Subscribe channel: {}", channel.Id);
					channel.unsubscribe(new UnsubscribeListener<T>(subscription, channel, this.subscriberImpl));
				}
			}
			else if (autoRetry)
			{
				//log.debug("Detected an error (either server or long network error), " + "another retry attempt only happen on another successfully handshake");
			}
			notifyListenerOnError(message);
		}

		private void notifyListenerOnError(IMessage message)
		{
			string errorMessage = "Unknow error (unspecified by server)";
			Exception throwable = null;
			object error = message[Message_Fields.ERROR_FIELD];
			if (error == null)
			{
				error = message["failure"];
				if (error != null && error is System.Collections.IDictionary)
				{
					throwable = (Exception)((System.Collections.IDictionary)error)["exception"];
					if (throwable != null)
					{
						errorMessage = throwable.Message;
					}
				}
			}
			else
			{
				errorMessage = (string)error;
			}
			subscribeOperationListener.OnSubscribingError(channel.Id, errorMessage, throwable);
		}

		private bool isShortNetworkFailure(IMessage message)
		{
			object failure = message["failure"];
			return failure != null;
		}
	}
}