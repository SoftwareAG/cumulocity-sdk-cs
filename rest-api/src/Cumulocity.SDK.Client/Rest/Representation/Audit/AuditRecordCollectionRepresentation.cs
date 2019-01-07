using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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

		//ORIGINAL LINE: @JSONTypeHint(AuditRecordRepresentation.class) public List<AuditRecordRepresentation> getAuditRecords()
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<AuditRecordRepresentation> iterator()
		public IEnumerator<AuditRecordRepresentation> iterator()
		{
			return auditRecords.GetEnumerator();
		}

		public override IEnumerator<AuditRecordRepresentation> GetEnumerator()
		{
			return auditRecords.GetEnumerator();
		}
	}
}
