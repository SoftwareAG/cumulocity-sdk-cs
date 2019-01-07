using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Application
{
	//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
	//ORIGINAL LINE: @Data @NoArgsConstructor @AllArgsConstructor @Builder(builderMethodName = "applicationRepresentation") public class ApplicationRepresentation extends AbstractExtensibleRepresentation
	public class ApplicationRepresentation : AbstractExtensibleRepresentation
	{

		public const string MICROSERVICE = "MICROSERVICE";

		private string id;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Size(max = 128) private String name;
		private string name;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Size(max = 128) private String key;
		private string key;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @IsOneOf({"EXTERNAL","HOSTED", "MICROSERVICE", "FEATURE", "APAMA_CEP_RULE"}) @NotNull(operation = Command.CREATE) @Null(operation = Command.UPDATE) private String type;
		private string type;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Pattern(regexp = "MARKET|PRIVATE") private String availability;
		private string availability;

		private TenantReferenceRepresentation owner;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Size(max = 255) private String contextPath;
		private string contextPath;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Size(max = 255) @Deprecated private String resourcesUrl;
		[Obsolete]
		private string resourcesUrl;

		[Obsolete]
		private string resourcesUsername;

		[Obsolete]
		private string resourcesPassword;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Size(max = 255) private String externalUrl;
		private string externalUrl;

		private ManifestRepresentation manifest;

		private string activeVersionId;

		/// <summary>
		/// Roles that are required for microservce in order to make requests to platform instance.
		/// </summary>
		private IList<string> requiredRoles;

		/// <summary>
		/// Roles that are required for users in order to make requests to microservice instance;
		/// </summary>
		private IList<string> roles;

		private string url;

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


		public virtual string Key
		{
			get
			{
				return key;
			}
			set
			{
				this.key = value;
			}
		}


		public virtual string Type
		{
			get
			{
				return type;
			}
			set
			{
				this.type = value;
			}
		}


		public virtual string Availability
		{
			get
			{
				return availability;
			}
			set
			{
				this.availability = value;
			}
		}


		public virtual TenantReferenceRepresentation Owner
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


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getContextPath()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ContextPath
		{
			get
			{
				return contextPath;
			}
			set
			{
				this.contextPath = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Deprecated @JSONProperty(ignoreIfNull = true) public String getResourcesUrl()
		[Obsolete]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ResourcesUrl
		{
			get
			{
				return resourcesUrl;
			}
			set
			{
				this.resourcesUrl = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Deprecated @JSONProperty(ignoreIfNull = true) public String getResourcesUsername()
		[Obsolete]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ResourcesUsername
		{
			get
			{
				return resourcesUsername;
			}
			set
			{
				this.resourcesUsername = value;
			}
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Deprecated @JSONProperty(ignoreIfNull = true) public String getResourcesPassword()
		[Obsolete]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ResourcesPassword
		{
			get
			{
				return resourcesPassword;
			}
			set
			{
				this.resourcesPassword = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getExternalUrl()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ExternalUrl
		{
			get
			{
				return externalUrl;
			}
			set
			{
				this.externalUrl = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ManifestRepresentation getManifest()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ManifestRepresentation Manifest
		{
			get
			{
				return manifest;
			}
			set
			{
				this.manifest = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getActiveVersionId()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ActiveVersionId
		{
			get
			{
				return activeVersionId;
			}
			set
			{
				this.activeVersionId = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public List<String> getRequiredRoles()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<string> RequiredRoles
		{
			get
			{
				return requiredRoles;
			}
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getUrl()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Url
		{
			get
			{
				return url;
			}
		}

		public virtual ApplicationReferenceRepresentation toReference()
		{
			//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
			//ORIGINAL LINE: final ApplicationReferenceRepresentation ref = new ApplicationReferenceRepresentation();
			ApplicationReferenceRepresentation @ref = new ApplicationReferenceRepresentation();
			//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
			//ORIGINAL LINE: final ApplicationRepresentation application = new ApplicationRepresentation();
			ApplicationRepresentation application = new ApplicationRepresentation();
			application.Id = Id;
			@ref.Application = application;
			return @ref;
		}
	}

}
