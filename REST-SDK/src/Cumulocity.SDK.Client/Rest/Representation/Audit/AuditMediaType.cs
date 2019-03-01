namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	public class AuditMediaType : CumulocityMediaType
	{
		public static readonly AuditMediaType AUDIT_API = new AuditMediaType("auditApi");

		public static readonly AuditMediaType AUDIT_RECORD = new AuditMediaType("auditRecord");

		public static readonly AuditMediaType AUDIT_RECORD_COLLECTION = new AuditMediaType("auditRecordCollection");

		public static readonly string AUDIT_API_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "auditApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly string AUDIT_RECORD_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "auditRecord+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly string AUDIT_RECORD_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "auditRecordCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public AuditMediaType(string entity) : base(entity)
		{
		}
	}
}