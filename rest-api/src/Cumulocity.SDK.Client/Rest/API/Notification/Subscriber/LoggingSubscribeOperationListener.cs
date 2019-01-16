using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	public class LoggingSubscribeOperationListener : ISubscribeOperationListener
	{
		private static ILoggerFactory loggerFactory = new LoggerFactory()
													.AddConsole();

		private ILogger LOG = loggerFactory.CreateLogger<LoggingSubscribeOperationListener>();

		public void onSubscribingSuccess(string channelId)
		{
			LOG.LogInformation("Successfully subscribed: {}", channelId);
		}

		public void onSubscribingError(string channelId, string message, Exception throwable)
		{
			LOG.LogError("Error when subscribing channel: {}, error: {}", channelId, message, throwable);
		}
	}
}