using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Interfaces
{
	public interface ISubscriptionNameResolver<T>
	{
		string apply(T o);
	}
}
