using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Cumulocity.SDK.Client.Logging;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
#pragma warning disable 0618
	public class LoggingSubscribeOperationListener : ISubscribeOperationListener
	{
		private static readonly ILog LOG = LogProvider.For<LoggingSubscribeOperationListener>();


		public void OnSubscribingSuccess(string channelId)
		{
			LOG.Info("Successfully subscribed: {}", channelId);
		}

		public void OnSubscribingError(string channelId, string message, Exception throwable)
		{
			LOG.Error("Error when subscribing channel: {}, error: {}", channelId, message, throwable);
		}
	}
}