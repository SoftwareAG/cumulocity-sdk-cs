namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscription<T>
	{
		T Object { get; }

		void unsubscribe();
	}
}