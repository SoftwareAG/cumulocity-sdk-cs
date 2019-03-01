using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	[JsonObject]
	public class AuditRecordCollectionRepresentation : BaseCollectionRepresentation<AuditRecordRepresentation>
	{
		private IList<AuditRecordRepresentation> auditRecords;

		public AuditRecordCollectionRepresentation()
		{
		}

		public AuditRecordCollectionRepresentation(IList<AuditRecordRepresentation> auditRecords)
		{
			this.auditRecords = auditRecords;
		}

		public virtual IList<AuditRecordRepresentation> AuditRecords
		{
			get
			{
				return auditRecords;
			}
			set
			{
				this.auditRecords = value;
			}
		}

		public override IEnumerator<AuditRecordRepresentation> GetEnumerator()
		{
			return auditRecords.GetEnumerator();
		}
	}
}