using Cometd.Bayeux.Client;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System.Diagnostics;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	public class ChannelSubscription<T> : ISubscription<T>
	{
		private readonly IMessageListener listener;

		private readonly IClientSessionChannel channel;

		private T @object;

		internal ChannelSubscription(IMessageListener listener, IClientSessionChannel channel, T @object)
		{
			this.listener = listener;
			this.channel = channel;
			this.@object = @object;
		}

		public void unsubscribe()
		{
			Debug.WriteLine("unsubscribing from channel {}", channel.Id);
			channel.unsubscribe(listener);
		}

		public T Object
		{
			get
			{
				return @object;
			}
		}
	}
}