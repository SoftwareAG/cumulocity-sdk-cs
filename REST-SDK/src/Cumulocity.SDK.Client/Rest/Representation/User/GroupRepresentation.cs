using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Application;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class GroupRepresentation : CustomPropertiesMapRepresentation
	{

		private long? id;

		private string name;

		private string description;

		private UserReferenceCollectionRepresentation users;

		private RoleReferenceCollectionRepresentation roles;

		private IDictionary<string, IList<string>> devicePermissions;

		private IList<ApplicationRepresentation> applications = new List<ApplicationRepresentation>();

		public virtual long? Id
		{
			set
			{
				this.id = value;
			}
			get
			{
				return id;
			}
		}


		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getName()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				this.name = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getDescription()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Description
		{
			get
			{
				return description;
			}
			set
			{
				this.description = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public UserReferenceCollectionRepresentation getUsers()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual UserReferenceCollectionRepresentation Users
		{
			get
			{
				return users;
			}
			set
			{
				this.users = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public RoleReferenceCollectionRepresentation getRoles()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual RoleReferenceCollectionRepresentation Roles
		{
			get
			{
				return roles;
			}
			set
			{
				this.roles = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public Map<String, List<String>> getDevicePermissions()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IDictionary<string, IList<string>> DevicePermissions
		{
			get
			{
				return devicePermissions;
			}
			set
			{
				this.devicePermissions = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public List<ApplicationRepresentation> getApplications()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<ApplicationRepresentation> Applications
		{
			get
			{
				return applications;
			}
			set
			{
				this.applications = value;
			}
		}

	}
}
