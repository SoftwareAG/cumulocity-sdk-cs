namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface IConnectionListener
	{
		void OnDisconnection(int httpCode);
	}
}