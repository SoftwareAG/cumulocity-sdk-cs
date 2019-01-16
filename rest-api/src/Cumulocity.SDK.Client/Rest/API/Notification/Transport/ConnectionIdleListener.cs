using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Transport
{
	public class ConnectionIdleListener : IConnectionIdleListener
	{
		private readonly Action action;

		public ConnectionIdleListener(Action action)
		{
			this.action = action;
		}

		public void onConnectionIdle()
		{
			this.action();
		}
	}
}