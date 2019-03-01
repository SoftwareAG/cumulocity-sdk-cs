using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Inventory
{
	[JsonObject]
	public class
		ManagedObjectReferenceCollectionRepresentation : BaseCollectionRepresentation<
			ManagedObjectReferenceRepresentation>
	{
		private IList<ManagedObjectReferenceRepresentation> references;

		public ManagedObjectReferenceCollectionRepresentation()
		{
			references = new List<ManagedObjectReferenceRepresentation>();
		}

		public virtual IList<ManagedObjectReferenceRepresentation> References
		{
			get => references;
			set => references = value;
		}

		public override IEnumerator<ManagedObjectReferenceRepresentation> GetEnumerator()
		{
			return this.references.GetEnumerator();
		}
	}
}