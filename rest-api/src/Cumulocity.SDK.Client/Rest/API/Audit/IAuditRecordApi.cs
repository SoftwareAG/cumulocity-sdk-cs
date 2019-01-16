﻿using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Audit;

namespace Cumulocity.SDK.Client.Rest.API.Audit
{
	/// <summary>
	/// API for creating and retrieving audit records from the platform
	/// </summary>
	public interface IAuditRecordApi
	{
		/// <summary>
		/// Gets an audit record by id
		/// </summary>
		/// <param name="gid"> id of the audit record to search for </param>
		/// <returns> the audit record with the given id </returns>
		/// <exception cref="SDKException"> if the audit record is not found </exception>
		AuditRecordRepresentation getAuditRecord(GId gid);

		/// <summary>
		/// Creates an audit record in the platform. The id of the audit record must not be set, since it will be generated by the platform
		/// </summary>
		/// <param name="auditRecord"> the audit record to be created </param>
		/// <returns> the created audit record with the generated id </returns>
		/// <exception cref="SDKException"> if the audit record could not be generated </exception>
		AuditRecordRepresentation create(AuditRecordRepresentation auditRecord);

		/// <summary>
		/// Gets all audit records from the platform
		/// </summary>
		/// <returns> collection of audit records with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IAuditRecordCollection AuditRecords { get; }

		/// <summary>
		/// Gets audit records from the platform based on the specified filter
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <returns> collection of audit records matched by the filter with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IAuditRecordCollection getAuditRecordsByFilter(AuditRecordFilter filter);
	}
}