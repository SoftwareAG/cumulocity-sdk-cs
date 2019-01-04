using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Cumulocity.SDK.Client.Rest.Representation.Application;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class TenantRepresentation : CustomPropertiesMapRepresentation
	{
		private string id;

		//ORIGINAL LINE: @Size(max = 256) @NotNull(operation = Command.CREATE) @Null(operation = Command.UPDATE) private String domain;
		private string domain;

		//ORIGINAL LINE: @Pattern(regexp = "ACTIVE|SUSPENDED") private String status;
		private string status;

		//ORIGINAL LINE: @Size(max = 256) @NotNull(operation = Command.CREATE) private String company;
		private string company;

		//ORIGINAL LINE: @Size(max = 50) @Pattern(regexp = "[^\\s\\\\+\\$/:]+", message = "field cannot contain whitespace, slashes nor any of (+$:) characters.") private String adminName;
		private string adminName;

		//ORIGINAL LINE: @Size(max = 32) private String adminPass;
		private string adminPass;

		private string adminEmail;

		//ORIGINAL LINE: @Size(max = 30) private String contactName;
		private string contactName;

		private string contactPhone;

		private ApplicationReferenceCollectionRepresentation applications;

		private ApplicationReferenceCollectionRepresentation ownedApplications;

		private bool? sendPasswordResetEmail;

		private DateTime dateCreated;

		private SupportUserDetailsRepresentation supportUser;

		//ORIGINAL LINE: @Size(max = 32) @Null(operation = { Command.CREATE, Command.UPDATE }) private String parent;
		private string parent;

		//ORIGINAL LINE: @Null(operation = { Command.CREATE }) private System.Nullable<bool> allowCreateTenants;
		private bool? allowCreateTenants;

		private long? storageLimitPerDevice;

		//ORIGINAL LINE: @Null(operation = { Command.UPDATE }) private GId tenantPolicyId;
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getDomain()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getStatus()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getCompany()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getContactName()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getContactPhone()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ApplicationReferenceCollectionRepresentation getApplications()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ApplicationReferenceCollectionRepresentation getOwnedApplications()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getAdminName()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getAdminPass()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getAdminEmail()
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

		//ORIGINAL LINE: @JSONProperty(ignore = true) public boolean shouldSendPasswordResetEmail()
		public virtual bool shouldSendPasswordResetEmail()
		{

			return sendPasswordResetEmail != null && (sendPasswordResetEmail??false);

		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public SupportUserDetailsRepresentation getSupportUser()
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

		//ORIGINAL LINE: @JSONProperty(value = "deprecated_DateCreated", ignore = true) @Deprecated public Date getDateCreated()
		//[Obsolete]
		//[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		//public virtual DateTime DateCreated
		//{
		//	get
		//	{
		//		return dateCreated == null ? null : dateCreated.toDate();
		//	}
		//	set
		//	{
		//		this.dateCreated = value == null ? null : newUTC(value);
		//	}
		//}

		//ORIGINAL LINE: @JSONProperty(value = "creationTime", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getCreatedDateTime()
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

	//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getParent()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getAllowCreateTenants()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getStorageLimitPerDevice()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) @JSONConverter(type = IDTypeConverter.class) public GId getTenantPolicyId()
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