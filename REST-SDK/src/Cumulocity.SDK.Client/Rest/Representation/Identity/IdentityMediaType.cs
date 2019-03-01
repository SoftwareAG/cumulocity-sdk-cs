using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
	public class IdentityMediaType : CumulocityMediaType
	{

		public static readonly IdentityMediaType EXTERNAL_ID = new IdentityMediaType("externalId");

		public static readonly string EXTERNAL_ID_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "externalId+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly IdentityMediaType EXTERNAL_ID_COLLECTION = new IdentityMediaType("externalIdCollection");

		public static readonly string EXTERNAL_ID_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "externalIdCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly IdentityMediaType IDENTITY_API = new IdentityMediaType("identityApi");

		public static readonly string IDENTITY_API_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "identityApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public IdentityMediaType(string entity) : base("application", VND_COM_NSN_CUMULOCITY + entity + "+json;" + VND_COM_NSN_CUMULOCITY_PARAMS)
		{
		}

	}
}
