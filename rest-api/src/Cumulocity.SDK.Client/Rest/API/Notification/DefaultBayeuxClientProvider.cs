using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cometd.Bayeux.Client;
using Cometd.Client;
using Cometd.Client.Transport;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	internal class DefaultBayeuxClientProvider : IBayeuxSessionProvider
	{
		public IClientSession get()
		{
			throw new NotImplementedException();
		}
	}

}
