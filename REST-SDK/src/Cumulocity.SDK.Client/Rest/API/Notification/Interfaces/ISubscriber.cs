namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriber<T, R>
	{
		ISubscription<T> Subscribe(T @object, ISubscriptionListener<T, R> handler);

		ISubscription<T> Subscribe(T @object, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<T, R> handler, bool autoRetry);

		void Disconnect();
	}
}