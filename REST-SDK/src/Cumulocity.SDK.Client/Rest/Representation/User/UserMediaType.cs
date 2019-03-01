using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	/// <summary>
	/// We follow here convention from <seealso cref="MediaType"/> class, where we have both <seealso cref="MediaType"/>
	/// instances, and string representations (with '_TYPE' suffix in name). 
	/// </summary>
	public class UserMediaType : CumulocityMediaType
	{

		public static readonly UserMediaType USER = new UserMediaType("user");

		public static readonly string USER_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "user+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType CURRENT_USER = new UserMediaType("currentUser");

		public static readonly string CURRENT_USER_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "currentUser+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType CURRENT_TENANT = new UserMediaType("currentTenant");

		public static readonly string CURRENT_TENANT_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "currentTenant+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType USER_COLLECTION = new UserMediaType("userCollection");

		public static readonly string USER_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "userCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType USER_API = new UserMediaType("userApi");

		public static readonly string USER_API_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "userApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType USER_REFERENCE = new UserMediaType("userReference");

		public static readonly string USER_REFERENCE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "userReference+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType USER_REFERENCE_COLLECTION = new UserMediaType("userReferenceCollection");

		public static readonly string USER_REFERENCE_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "userReferenceCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType GROUP = new UserMediaType("group");

		public static readonly string GROUP_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "group+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType GROUP_COLLECTION = new UserMediaType("groupCollection");

		public static readonly string GROUP_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "groupCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType GROUP_REFERENCE = new UserMediaType("groupReference");

		public static readonly string GROUP_REFERENCE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "groupReference+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType GROUP_REFERENCE_COLLECTION = new UserMediaType("groupReferenceCollection");

		public static readonly string GROUP_REFERENCE_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "groupReferenceCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType ROLE = new UserMediaType("role");

		public static readonly string ROLE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "role+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType ROLE_COLLECTION = new UserMediaType("roleCollection");

		public static readonly string ROLE_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "roleCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType ROLE_REFERENCE = new UserMediaType("roleReference");

		public static readonly string ROLE_REFERENCE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "roleReference+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType ROLE_REFERENCE_COLLECTION = new UserMediaType("roleReferenceCollection");

		public static readonly string ROLE_REFERENCE_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "roleReferenceCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType INVENTORY_ASSIGNMENT = new UserMediaType("inventoryAssignment");

		public static readonly string INVENTORY_ASSIGNMENT_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "inventoryAssignment+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType INVENTORY_ASSIGNMENT_COLLECTION = new UserMediaType("inventoryAssignmentCollection");

		public static readonly string INVENTORY_ASSIGNMENT_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "inventoryAssignmentCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType USER_OWNER_REFERENCE = new UserMediaType("userOwnerReference");

		public static readonly string USER_OWNER_REFERENCE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "userOwnerReference+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly UserMediaType USER_DELEGATED_BY_REFERENCE = new UserMediaType("userDelegatedByReference");

		public static readonly string USER_DELEGATED_BY_REFERENCE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "userDelegatedByReference+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;


		public UserMediaType(string entity) : base("application", VND_COM_NSN_CUMULOCITY + entity + "+json;" + VND_COM_NSN_CUMULOCITY_PARAMS)
		{
		}
	}
}
