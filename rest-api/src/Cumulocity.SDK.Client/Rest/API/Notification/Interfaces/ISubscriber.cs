namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriber<T, R>
	{
		ISubscription<T> subscribe(T @object, ISubscriptionListener<T, R> handler);

		ISubscription<T> subscribe(T @object, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<T, R> handler, bool autoRetry);

		void disconnect();
	}
}