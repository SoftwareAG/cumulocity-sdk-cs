using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Application
{
	public class ManifestRepresentation : AbstractExtensibleRepresentation
	{
		private long? id;
		private long? applicationId;
		private IList<string> imports;
		private string contextPath;

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

		public virtual IList<string> UniqueImports
		{
			get
			{
				return imports;
			}
		}

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