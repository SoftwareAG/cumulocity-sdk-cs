using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cumulocity.SDK.Client.Rest.Representation.Cep
{
	//ORIGINAL LINE: @SuppressWarnings("rawtypes") @NoArgsConstructor public class CepModuleRepresentation extends AbstractExtensibleRepresentation
	public class CepModuleRepresentation : AbstractExtensibleRepresentation
	{
		//ORIGINAL LINE: @Null(operation = { CREATE }) private String id;
		private string id;

		//ORIGINAL LINE: @NotNull(operation = { CREATE }) private String name;
		private string name;

		//ORIGINAL LINE: @Null(operation = { CREATE, UPDATE }) private DateTime lastModified;
		private DateTime lastModified;

		//ORIGINAL LINE: @Null(operation = { CREATE }) private String status;
		private string status;

		private IList statements;

		private string fileRepresentation;

		//ORIGINAL LINE: @Builder(builderMethodName = "cepModuleRepresentation")
		//public CepModuleRepresentation(String id, String name, DateTime lastModified, String status, List statements, String fileRepresentation)
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

		//ORIGINAL LINE: @JSONProperty(value = "deprecated_LastModified", ignore = true) @Deprecated public Date getLastModified()
		//[Obsolete]
		//public virtual DateTime LastModified
		//{
		//	get
		//	{
		//		return lastModified == null ? null : lastModified.toDate();
		//	}
		//	set
		//	{
		//		this.lastModified = value == null ? null : newUTC(value);
		//	}
		//}

		//ORIGINAL LINE: @JSONProperty(value = "lastModified", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getLastModifiedDateTime()
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


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
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


		//ORIGINAL LINE: @JSONProperty(ignore = true) public List getStatements()
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


		//ORIGINAL LINE: @JSONProperty(ignore = true) public String getFileRepresentation()
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
