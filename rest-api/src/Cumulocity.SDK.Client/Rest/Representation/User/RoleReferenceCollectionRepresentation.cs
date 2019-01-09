using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	[JsonObject]
	public class RoleReferenceCollectionRepresentation : BaseCollectionRepresentation<RoleReferenceRepresentation>, IReferenceRepresentation
	{

		private IList<RoleReferenceRepresentation> references;

		public RoleReferenceCollectionRepresentation()
		{
			this.references = new List<RoleReferenceRepresentation>();
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONTypeHint(RoleReferenceRepresentation.class) public List<RoleReferenceRepresentation> getReferences()
		public virtual IList<RoleReferenceRepresentation> References
		{
			get
			{
				return references;
			}
			set
			{
				this.references = value;
			}
		}


		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<RoleReferenceRepresentation> iterator()
		public  IEnumerator<RoleReferenceRepresentation> iterator()
		{
			return references.GetEnumerator();
		}

		public override IEnumerator<RoleReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}
	}

}
