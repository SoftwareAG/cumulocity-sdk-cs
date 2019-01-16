using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	internal sealed class DummySubscription<T> : ISubscription<T>
	{
		private readonly SubscriptionRecord<T> subscription;

		internal DummySubscription(SubscriptionRecord<T> subscription)
		{
			this.subscription = subscription;
		}

		public void unsubscribe()
		{
		}

		public T Object => subscription.Id;
	}
}