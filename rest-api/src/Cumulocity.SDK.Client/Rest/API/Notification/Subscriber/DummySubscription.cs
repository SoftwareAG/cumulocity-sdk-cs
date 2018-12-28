using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	sealed class DummySubscription<T> : ISubscription<T>
	{

		private readonly SubscriptionRecord<T> subscription;

		//ORIGINAL LINE: DummySubscription(final SubscriptionRecord subscription)
		internal DummySubscription(SubscriptionRecord<T> subscription)
		{
			this.subscription = subscription;
		}

		public void unsubscribe()
		{
		}

		public T Object
		{
			get
			{
				return subscription.Id;
			}
		}
	}
}
