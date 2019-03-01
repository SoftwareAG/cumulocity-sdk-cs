namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	public class AuditRecordsRepresentation : AbstractExtensibleRepresentation
	{
		private string auditRecordsForType;

		private string auditRecordsForUser;

		private string auditRecordsForApplication;

		private string auditRecordsForUserAndType;

		private string auditRecordsForUserAndApplication;

		private string auditRecordsForTypeAndApplication;

		private string auditRecordsForTypeAndUserAndApplication;

		private AuditRecordCollectionRepresentation auditRecords;

		public virtual string AuditRecordsForType
		{
			get
			{
				return auditRecordsForType;
			}
			set
			{
				this.auditRecordsForType = value;
			}
		}

		public virtual string AuditRecordsForUser
		{
			get
			{
				return auditRecordsForUser;
			}
			set
			{
				this.auditRecordsForUser = value;
			}
		}

		public virtual string AuditRecordsForApplication
		{
			get
			{
				return auditRecordsForApplication;
			}
			set
			{
				this.auditRecordsForApplication = value;
			}
		}

		public virtual AuditRecordCollectionRepresentation AuditRecords
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

		public virtual string AuditRecordsForUserAndType
		{
			get
			{
				return auditRecordsForUserAndType;
			}
			set
			{
				this.auditRecordsForUserAndType = value;
			}
		}

		public virtual string AuditRecordsForUserAndApplication
		{
			get
			{
				return auditRecordsForUserAndApplication;
			}
			set
			{
				this.auditRecordsForUserAndApplication = value;
			}
		}

		public virtual string AuditRecordsForTypeAndApplication
		{
			get
			{
				return auditRecordsForTypeAndApplication;
			}
			set
			{
				this.auditRecordsForTypeAndApplication = value;
			}
		}

		public virtual string AuditRecordsForTypeAndUserAndApplication
		{
			get
			{
				return auditRecordsForTypeAndUserAndApplication;
			}
			set
			{
				this.auditRecordsForTypeAndUserAndApplication = value;
			}
		}
	}
}