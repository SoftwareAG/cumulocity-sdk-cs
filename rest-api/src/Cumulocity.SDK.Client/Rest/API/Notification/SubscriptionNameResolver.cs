using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	public sealed class SubscriptionNameResolver<T> : ISubscriptionNameResolver<T>
	{
		public  string Apply(T input)
		{
			return input.ToString();
		}
	}
}
