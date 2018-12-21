using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.API.Polling.Threads
{
	using System;

	public interface Future<T>
	{
		bool Cancel(bool mayInterruptIfRunning);
		T Get();
	}
}
