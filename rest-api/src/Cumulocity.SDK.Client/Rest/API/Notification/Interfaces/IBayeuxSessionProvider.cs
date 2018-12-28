using System;
using System.Collections.Generic;
using System.Text;
using Cometd.Bayeux.Client;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface IBayeuxSessionProvider
	{
		IClientSession get();
	}
}
