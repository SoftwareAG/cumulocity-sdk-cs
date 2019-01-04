using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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

		//ORIGINAL LINE: @JSONTypeHint(ApplicationReferenceRepresentation.class) public List<ApplicationReferenceRepresentation> getReferences()
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<ApplicationReferenceRepresentation> iterator()
		public IEnumerator<ApplicationReferenceRepresentation> iterator()
		{
			return references.GetEnumerator();
		}

		public override IEnumerator<ApplicationReferenceRepresentation> GetEnumerator()
		{
			return references.GetEnumerator();
		}
	}
}
