using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Application
{
#pragma warning disable CS0169
#pragma warning disable 0649
	public class ApplicationRepresentation : AbstractExtensibleRepresentation
	{

		public const string MICROSERVICE = "MICROSERVICE";

		private string id;

		private string name;

		private string key;

		private string type;

		private string availability;

		private TenantReferenceRepresentation owner;

		private string contextPath;

		[Obsolete]
		private string resourcesUrl;

		[Obsolete]
		private string resourcesUsername;

		[Obsolete]
		private string resourcesPassword;

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

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<string> RequiredRoles
		{
			get
			{
				return requiredRoles;
			}
		}

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
			ApplicationReferenceRepresentation @ref = new ApplicationReferenceRepresentation();
			ApplicationRepresentation application = new ApplicationRepresentation();
			application.Id = Id;
			@ref.Application = application;
			return @ref;
		}
	}

}
