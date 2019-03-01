using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Application
{
	[JsonObject]
	public class ApplicationReferenceCollectionRepresentation : BaseCollectionRepresentation<ApplicationReferenceRepresentation>
	{
		private IList<ApplicationReferenceRepresentation> references;

		public ApplicationReferenceCollectionRepresentation()
		{
			this.references = new List<ApplicationReferenceRepresentation>();
		}

		public virtual IList<ApplicationReferenceRepresentation> References
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

		public override IEnumerator<ApplicationReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}
	}
}