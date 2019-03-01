using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscribeOperationListener
	{
		void OnSubscribingSuccess(string channelId);

		void OnSubscribingError(string channelId, string error, Exception throwable);
	}
}