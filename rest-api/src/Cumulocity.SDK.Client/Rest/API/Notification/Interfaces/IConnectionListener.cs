namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface IConnectionListener
	{
		void onDisconnection(int httpCode);
	}
}