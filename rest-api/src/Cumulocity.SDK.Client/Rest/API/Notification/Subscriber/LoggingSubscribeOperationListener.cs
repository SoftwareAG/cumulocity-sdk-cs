using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
#pragma warning disable 0618
	public class LoggingSubscribeOperationListener : ISubscribeOperationListener
	{
		private static ILoggerFactory loggerFactory = new LoggerFactory()
													.AddConsole();

		private ILogger LOG = loggerFactory.CreateLogger<LoggingSubscribeOperationListener>();

		public void OnSubscribingSuccess(string channelId)
		{
			LOG.LogInformation("Successfully subscribed: {}", channelId);
		}

		public void OnSubscribingError(string channelId, string message, Exception throwable)
		{
			LOG.LogError("Error when subscribing channel: {}, error: {}", channelId, message, throwable);
		}
	}
}