using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriptionListener<T, R>
	{
		void onNotification(ISubscription<T> subscription, R notification);

		void onError(ISubscription<T> subscription, Exception ex);
	}
}