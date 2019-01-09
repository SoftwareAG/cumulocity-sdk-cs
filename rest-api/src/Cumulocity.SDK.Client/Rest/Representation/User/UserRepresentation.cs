using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Application;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class UserRepresentation : CustomPropertiesMapRepresentation
	{

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Null(operation = Command.CREATE) private String id;
		private string id;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Size(max = 1000) @Pattern(regexp = "[^\\s\\\\+\\$/:]+", message = "field cannot contain whitespace, slashes nor any of (+$:) characters.") @Null(operation = Command.UPDATE) @NotNull(operation = Command.CREATE) private String userName;
		private string userName;

		private string owner;

		private string delegatedBy;

		private string password;

		private string firstName;

		private string lastName;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Size(max = 256, message = "maximum length is 256 characters") private String phone;
		private string phone;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Size(max = 256, message = "maximum length is 256 characters") private String email;
		private string email;

		private string passwordStrength;

		private bool? shouldResetPassword;

		private bool? supportUserEnabled;

		private DateTime lastPasswordChange;

		private bool? enabled;

		private IDictionary<string, IList<string>> devicePermissions;

		private GroupReferenceCollectionRepresentation groups;

		private RoleReferenceCollectionRepresentation roles;

		private IList<ApplicationRepresentation> applications;

		private bool? sendPasswordResetEmail;

		private bool? twoFactorAuthenticationEnabled;

		private bool? newsletter;

		private int? subusersCount;

		private string displayName;

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getUserName()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				this.userName = value;
			}
		}


		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getOwner()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Owner
		{
			get
			{
				return owner;
			}
			set
			{
				this.owner = value;
			}
		}


		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getDelegatedBy()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string DelegatedBy
		{
			get
			{
				return delegatedBy;
			}
			set
			{
				this.delegatedBy = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getPassword()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Password
		{
			get
			{
				return password;
			}
			set
			{
				this.password = value;
			}
		}


		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getFirstName()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getLastName()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string LastName
		{
			get
			{
				return lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getPhone()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Phone
		{
			get
			{
				return phone;
			}
			set
			{
				this.phone = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getEmail()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Email
		{
			get
			{
				return email;
			}
			set
			{
				this.email = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getPasswordStrength()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string PasswordStrength
		{
			get
			{
				return passwordStrength;
			}
			set
			{
				this.passwordStrength = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getShouldResetPassword()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? ShouldResetPassword
		{
			get
			{
				return shouldResetPassword;
			}
			set
			{
				this.shouldResetPassword = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getSupportUserEnabled()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? SupportUserEnabled
		{
			get
			{
				return supportUserEnabled;
			}
			set
			{
				this.supportUserEnabled = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getEnabled()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				this.enabled = value;
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public GroupReferenceCollectionRepresentation getGroups()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual GroupReferenceCollectionRepresentation Groups
		{
			get
			{
				return groups;
			}
			set
			{
				this.groups = value;
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


		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getId()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}


		//ORIGINAL LINE: @JSONProperty(value = "deprecated_LastPasswordChange", ignore = true) @Deprecated public Date getLastPasswordChange()
		//[Obsolete]
		//public virtual DateTime LastPasswordChange
		//{
		//	get
		//	{
		//		return lastPasswordChange == null ? null : lastPasswordChange.toDate();
		//	}
		//	set
		//	{
		//		this.lastPasswordChange = value == null ? null : newUTC(value);
		//	}
		//}

		//ORIGINAL LINE: @JSONProperty(value = "lastPasswordChange", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getLastPasswordChangeDateTime()
		[JsonProperty("lastPasswordChange",NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime LastPasswordChangeDateTime
		{
			get
			{
				return lastPasswordChange;
			}
			set
			{
				this.lastPasswordChange = value;
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


		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getSendPasswordResetEmail()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? SendPasswordResetEmail
		{
			get
			{
				return sendPasswordResetEmail;
			}
			set
			{
				this.sendPasswordResetEmail = value;
			}
		}


		public virtual bool shouldSendPasswordResetEmail()
		{

			if (sendPasswordResetEmail != null)
				return (bool) sendPasswordResetEmail;
			else return false;

		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getTwoFactorAuthenticationEnabled()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? TwoFactorAuthenticationEnabled
		{
			get
			{
				return twoFactorAuthenticationEnabled;
			}
			set
			{
				this.twoFactorAuthenticationEnabled = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getNewsletter()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? Newsletter
		{
			get
			{
				return newsletter;
			}
			set
			{
				this.newsletter = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<int> getSubusersCount()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual int? SubusersCount
		{
			get
			{
				return subusersCount;
			}
			set
			{
				this.subusersCount = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getDisplayName()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string DisplayName
		{
			get
			{
				return displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

	}
}
