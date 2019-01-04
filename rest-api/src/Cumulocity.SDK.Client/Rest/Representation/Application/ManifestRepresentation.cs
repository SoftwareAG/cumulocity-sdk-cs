using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.Application
{
	public class ManifestRepresentation : AbstractExtensibleRepresentation
	{

		private long? id;
		private long? applicationId;
		private IList<string> imports;
		private string contextPath;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getId()
		public virtual long? Id
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


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getApplicationId()
		public virtual long? ApplicationId
		{
			get
			{
				return applicationId;
			}
			set
			{
				this.applicationId = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public List<String> getImports()
		public virtual IList<string> Imports
		{
			get
			{
				return imports;
			}
			set
			{
				this.imports = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignore = true) public List<String> getUniqueImports()
		public virtual IList<string> UniqueImports
		{
			get
			{
				return imports;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getContextPath()
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



	}
}
