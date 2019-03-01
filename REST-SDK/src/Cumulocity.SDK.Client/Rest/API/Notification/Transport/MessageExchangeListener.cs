using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Transport
{
	public class MessageExchangeListener : IMessageExchangeListener
	{
		private Action action;

		public MessageExchangeListener(Action action)
		{
			this.action = action;
		}

		public void OnFinish()
		{
			this.action();
		}
	}
}