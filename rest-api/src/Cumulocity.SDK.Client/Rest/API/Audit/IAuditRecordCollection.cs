using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Audit;

namespace Cumulocity.SDK.Client.Rest.API.Audit
{
	public interface IAuditRecordCollection : 
		IPagedCollectionResource<AuditRecordRepresentation, PagedAuditCollectionRepresentation<AuditRecordCollectionRepresentation>>
	{

	}
}
