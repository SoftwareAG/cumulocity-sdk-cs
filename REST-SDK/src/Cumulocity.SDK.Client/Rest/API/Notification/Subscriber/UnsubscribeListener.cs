using Cometd.Bayeux;
using Cometd.Bayeux.Client;
using Cumulocity.SDK.Client.Logging;
using System.Diagnostics;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	internal class UnsubscribeListener<T> : IMessageListener
	{
		private readonly SubscriptionRecord<T> subscribed;
		private readonly IClientSessionChannel unsubscribeChannel;
		private static readonly ILog LOG = LogProvider.For<UnsubscribeListener<T>>();
		private readonly SubscriberImpl<T> subscriberImpl;

		public UnsubscribeListener(SubscriptionRecord<T> subscribed, IClientSessionChannel unsubscribeChannel, SubscriberImpl<T> subscriberImpl)
		{
			this.subscribed = subscribed;
			this.unsubscribeChannel = unsubscribeChannel;
			this.subscriberImpl = subscriberImpl;
		}

		public void onMessage(IClientSessionChannel channel, IMessage message)
		{
			if (subscriberImpl.GetSubscriptionName(subscribed.Id).Equals(message.ChannelId) && message.Successful)
			{
				try
				{
					LOG.Debug("unsubscribed successfuly from channel {}", channel.Id);
					subscribed.Remove();
				}
				finally
				{
					unsubscribeChannel.removeListener(this);
				}
			}
		}
	}
}