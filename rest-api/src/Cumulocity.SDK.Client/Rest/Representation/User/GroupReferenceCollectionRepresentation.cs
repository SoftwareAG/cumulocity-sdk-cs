using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	[JsonObject]
	public class GroupReferenceCollectionRepresentation : BaseCollectionRepresentation<GroupReferenceRepresentation>, IReferenceRepresentation
	{

		private IList<GroupReferenceRepresentation> references;

		public GroupReferenceCollectionRepresentation()
		{
			this.references = new List<GroupReferenceRepresentation>();
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONTypeHint(GroupReferenceRepresentation.class) public List<GroupReferenceRepresentation> getReferences()
		public virtual IList<GroupReferenceRepresentation> References
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


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<GroupReferenceRepresentation> iterator()
		public  IEnumerator<GroupReferenceRepresentation> iterator()
		{
			return references.GetEnumerator();
		}

		public override IEnumerator<GroupReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}
	}
}
