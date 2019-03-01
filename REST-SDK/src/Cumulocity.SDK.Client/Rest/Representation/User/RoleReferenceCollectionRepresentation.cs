using Newtonsoft.Json;
using System.Collections.Generic;

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

		public override IEnumerator<RoleReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}
	}
}