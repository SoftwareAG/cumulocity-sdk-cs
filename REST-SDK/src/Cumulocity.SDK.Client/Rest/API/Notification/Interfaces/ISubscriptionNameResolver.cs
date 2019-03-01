namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriptionNameResolver<T>
	{
		string Apply(T o);
	}
}