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

		public override IEnumerator<GroupReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}
	}
}
