using Cometd.Bayeux.Client;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface IBayeuxSessionProvider
	{
		IClientSession Get();
	}
}