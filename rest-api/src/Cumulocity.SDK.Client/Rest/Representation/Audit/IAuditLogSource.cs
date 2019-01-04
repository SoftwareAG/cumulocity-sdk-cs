using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	public interface IAuditLogSource<ID>
	{
		ID LogSource { get; }
	}

}
