using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriber<T, R>
	{
		ISubscription<T> subscribe(T @object, ISubscriptionListener<T, R> handler);

		void disconnect();
	}
}
