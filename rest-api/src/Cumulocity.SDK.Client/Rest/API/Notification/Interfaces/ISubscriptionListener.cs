using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriptionListener<T, R>
	{
		void OnNotification(ISubscription<T> subscription, R notification);

		void OnError(ISubscription<T> subscription, Exception ex);
	}
}