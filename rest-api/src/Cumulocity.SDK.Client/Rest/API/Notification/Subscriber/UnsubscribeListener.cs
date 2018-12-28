using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Cometd.Bayeux;
using Cometd.Bayeux.Client;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	internal class UnsubscribeListener<T> : IMessageListener
	{
		private readonly SubscriptionRecord<T> subscribed;
		private readonly IClientSessionChannel unsubscribeChannel;
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
					Debug.WriteLine("unsubscribed successfuly from channel {}", channel.Id);
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
