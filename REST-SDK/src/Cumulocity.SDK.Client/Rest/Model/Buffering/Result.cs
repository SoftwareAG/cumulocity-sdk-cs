using System;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
	public class Result
	{
		public virtual Exception Exception { get; set; }

		public virtual object Response { get; set; }
	}
}