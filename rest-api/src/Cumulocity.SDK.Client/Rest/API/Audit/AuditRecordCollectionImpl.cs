using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Audit;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Audit
{
	public class AuditRecordCollectionImpl : PagedCollectionResourceImpl<AuditRecordRepresentation, AuditRecordCollectionRepresentation, PagedAuditCollectionRepresentation<AuditRecordCollectionRepresentation>>
	{
		public AuditRecordCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType
		{
			get
			{
				return AuditMediaType.AUDIT_RECORD_COLLECTION;
			}
		}

		protected internal override Type ResponseClassProp => typeof(AuditRecordCollectionRepresentation);

		protected internal override PagedAuditCollectionRepresentation<AuditRecordCollectionRepresentation> wrap(AuditRecordCollectionRepresentation collection)
		{
			return new PagedAuditCollectionRepresentation<AuditRecordCollectionRepresentation>(collection, this);
		}
	}
}