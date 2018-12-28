using Cometd.Bayeux;
using Cometd.Bayeux.Client;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	public sealed class MessageListenerAdapter<T> : IMessageListener
	{
		private readonly ISubscriptionListener<T, IMessage> handler;

		private readonly ISubscription<T> subscription;

		internal MessageListenerAdapter(ISubscriptionListener<T, IMessage> handler, IClientSessionChannel channel, T @object)
		{
			this.handler = handler;
			subscription = createSubscription(channel, @object);
		}

		protected internal ChannelSubscription<T> createSubscription(IClientSessionChannel channel, T @object)
		{
			return new ChannelSubscription<T>(this, channel, @object);
		}

		public void onMessage(IClientSessionChannel channel, IMessage message)
		{
			handler.onNotification(subscription, message);
		}

		public ISubscription<T> Subscription
		{
			get
			{
				return subscription;
			}
		}

	}
}
