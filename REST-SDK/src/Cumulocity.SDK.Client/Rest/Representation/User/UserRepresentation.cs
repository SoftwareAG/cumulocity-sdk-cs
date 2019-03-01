using Cumulocity.SDK.Client.Rest.Representation.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class UserRepresentation : CustomPropertiesMapRepresentation
	{
		private string id;

		private string userName;

		private string owner;

		private string delegatedBy;

		private string password;

		private string firstName;

		private string lastName;

		private string phone;

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

		[JsonProperty("lastPasswordChange", NullValueHandling = NullValueHandling.Ignore)]
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
				return (bool)sendPasswordResetEmail;
			else return false;
		}

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