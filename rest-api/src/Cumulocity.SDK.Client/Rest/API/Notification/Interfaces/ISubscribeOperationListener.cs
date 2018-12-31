﻿using System;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscribeOperationListener
	{
		void onSubscribingSuccess(string channelId);

		void onSubscribingError(string channelId, string error, Exception throwable);
	}
}