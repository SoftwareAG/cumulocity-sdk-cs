using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	/// <summary>
	/// We follow here convention from <seealso cref="MediaType"/> class, where we have both <seealso cref="MediaType"/>
	/// instances, and string representations (with '_TYPE' suffix in name).
	/// </summary>
	public class TenantMediaType : CumulocityMediaType
	{

		public static readonly TenantMediaType TENANT = new TenantMediaType("tenant");

		public static readonly TenantMediaType TENANT_REFERENCE = new TenantMediaType("tenantReference");

		public static readonly string TENANT_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "tenant+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly string TENANT_API_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "tenantApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly string TENANT_REFERENCE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "tenantReference+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly TenantMediaType TENANT_COLLECTION = new TenantMediaType("tenantCollection");

		public static readonly string TENANT_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "tenantCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public TenantMediaType(string entity) : base(entity)
		{
		}
	}
}
