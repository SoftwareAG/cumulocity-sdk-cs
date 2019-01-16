using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Audit;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Audit
{
	public class AuditRecordApiImpl : IAuditRecordApi
	{
		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private AuditRecordsRepresentation auditRecordsRepresentation;

		private UrlProcessor urlProcessor;

		public AuditRecordApiImpl(RestConnector restConnector, UrlProcessor urlProcessor, AuditRecordsRepresentation auditRecordsRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.urlProcessor = urlProcessor;
			this.auditRecordsRepresentation = auditRecordsRepresentation;
			this.pageSize = pageSize;
		}

		private AuditRecordsRepresentation AuditRecordsRepresentation
		{
			get
			{
				return auditRecordsRepresentation;
			}
		}

		public AuditRecordRepresentation getAuditRecord(GId gid)
		{
			string url = SelfUri + "/" + gid.Value;
			return restConnector.Get<AuditRecordRepresentation>(url, AuditMediaType.AUDIT_RECORD, typeof(AuditRecordRepresentation));
		}

		private string SelfUri
		{
			get
			{
				return AuditRecordsRepresentation.AuditRecords.Self;
			}
		}

		public IAuditRecordCollection AuditRecords
		{
			get
			{
				string url = SelfUri;
				return new AuditRecordCollectionImpl(restConnector, url, pageSize);
			}
		}

		public AuditRecordRepresentation create(AuditRecordRepresentation representation)
		{
			return restConnector.Post<AuditRecordRepresentation>(SelfUri, AuditMediaType.AUDIT_RECORD, representation);
		}

		public IAuditRecordCollection getAuditRecordsByFilter(AuditRecordFilter filter)
		{
			if (filter == null)
			{
				return AuditRecords;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new AuditRecordCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}
	}
}