﻿namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface IProvider<T>
	{
		T Get();
	}
}