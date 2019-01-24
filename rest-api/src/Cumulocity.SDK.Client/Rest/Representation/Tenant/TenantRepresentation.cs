using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Application;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class TenantRepresentation : CustomPropertiesMapRepresentation
	{
		private string id;

		private string domain;

		private string status;

		private string company;

		private string adminName;

		private string adminPass;

		private string adminEmail;

		private string contactName;

		private string contactPhone;

		private ApplicationReferenceCollectionRepresentation applications;

		private ApplicationReferenceCollectionRepresentation ownedApplications;

		private bool? sendPasswordResetEmail;

		private DateTime dateCreated;

		private SupportUserDetailsRepresentation supportUser;

		private string parent;

		private bool? allowCreateTenants;

		private long? storageLimitPerDevice;

		private GId tenantPolicyId;

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

		[MaxLength(256)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Domain
		{
			get
			{
				return domain;
			}
			set
			{
				this.domain = value;
			}
		}

		[RegularExpression(@"ACTIVE|SUSPENDED",
			ErrorMessage = "Characters are not allowed.")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Status
		{
			get
			{
				return status;
			}
			set
			{
				this.status = value;
			}
		}

		[MaxLength(256)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Company
		{
			get
			{
				return company;
			}
			set
			{
				this.company = value;
			}
		}

		[MaxLength(30)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ContactName
		{
			get
			{
				return contactName;
			}
			set
			{
				this.contactName = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ContactPhone
		{
			get
			{
				return contactPhone;
			}
			set
			{
				this.contactPhone = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ApplicationReferenceCollectionRepresentation Applications
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
		public virtual ApplicationReferenceCollectionRepresentation OwnedApplications
		{
			get
			{
				return ownedApplications;
			}
			set
			{
				this.ownedApplications = value;
			}
		}

		[MaxLength(50)]
		[RegularExpression(@"[^\\s\\\\+\\$/:]+",
			ErrorMessage = "field cannot contain whitespace, slashes nor any of (+$:) characters.")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string AdminName
		{
			get
			{
				return adminName;
			}
			set
			{
				this.adminName = value;
			}
		}

		[MaxLength(32)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string AdminPass
		{
			get
			{
				return adminPass;
			}
			set
			{
				this.adminPass = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string AdminEmail
		{
			get
			{
				return adminEmail;
			}
			set
			{
				this.adminEmail = value;
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
			return sendPasswordResetEmail != null && (sendPasswordResetEmail ?? false);
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual SupportUserDetailsRepresentation SupportUser
		{
			get
			{
				return supportUser;
			}
			set
			{
				this.supportUser = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime CreatedDateTime
		{
			get
			{
				return dateCreated;
			}
			set
			{
				this.dateCreated = value;
			}
		}

		public override string toJSON()
		{
			return JsonConvert.SerializeObject(this);
		}

		[MaxLength(32)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Parent
		{
			get
			{
				return parent;
			}
			set
			{
				this.parent = value;
			}
		}

		public virtual bool? AllowCreateTenants
		{
			get
			{
				return allowCreateTenants;
			}
			set
			{
				this.allowCreateTenants = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual long? StorageLimitPerDevice
		{
			get
			{
				return storageLimitPerDevice;
			}
			set
			{
				this.storageLimitPerDevice = value;
			}
		}

		public virtual DateTime DateCreated
		{
			set
			{
				this.dateCreated = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual GId TenantPolicyId
		{
			get
			{
				return tenantPolicyId;
			}
			set
			{
				this.tenantPolicyId = value;
			}
		}
	}
}