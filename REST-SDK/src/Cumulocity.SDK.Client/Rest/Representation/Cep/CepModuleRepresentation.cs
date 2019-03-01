using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cumulocity.SDK.Client.Rest.Representation.Cep
{
	public class CepModuleRepresentation : AbstractExtensibleRepresentation
	{
		private string id;

		private string name;

		private DateTime lastModified;

		private string status;

		private IList statements;

		private string fileRepresentation;

		public CepModuleRepresentation(string id, string name, DateTime lastModified, string status, System.Collections.IList statements, string fileRepresentation)
		{
			this.id = id;
			this.name = name;
			this.lastModified = lastModified;
			this.status = status;
			this.statements = statements;
			this.fileRepresentation = fileRepresentation;
		}

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

		[JsonProperty("lastModified",NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime LastModifiedDateTime
		{
			get
			{
				return lastModified;
			}
			set
			{
				this.lastModified = value;
			}
		}

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

		[JsonIgnore]
		public virtual IList Statements
		{
			get
			{
				return statements;
			}
			set
			{
				this.statements = value;
			}
		}

		[JsonIgnore]
		public virtual string FileRepresentation
		{
			get
			{
				return fileRepresentation;
			}
			set
			{
				this.fileRepresentation = value;
			}
		}

	}

}
