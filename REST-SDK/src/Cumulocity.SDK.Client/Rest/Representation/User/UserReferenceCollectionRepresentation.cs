using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	[JsonObject]
	public class UserReferenceCollectionRepresentation : BaseCollectionRepresentation<UserReferenceRepresentation>, IReferenceRepresentation
	{

		private IList<UserReferenceRepresentation> references;

		public UserReferenceCollectionRepresentation()
		{
			this.references = new List<UserReferenceRepresentation>();
		}

		public virtual IList<UserReferenceRepresentation> References
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

		public override IEnumerator<UserReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}

	}
}
