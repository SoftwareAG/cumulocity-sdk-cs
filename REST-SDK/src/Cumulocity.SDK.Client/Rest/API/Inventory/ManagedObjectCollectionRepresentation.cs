using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	[JsonObject]
	public class ManagedObjectCollectionRepresentation : BaseCollectionRepresentation<ManagedObjectRepresentation>//, IEnumerable<ManagedObjectRepresentation>
	{
		private IList<ManagedObjectRepresentation> managedObjects = new List<ManagedObjectRepresentation>();

		public ManagedObjectCollectionRepresentation()
		{
		}

		public virtual IList<ManagedObjectRepresentation> ManagedObjects
		{
			get
			{
				return this.managedObjects;
			}
			set
			{
				this.managedObjects = value;
			}
		}

		#region Implementation of IEnumerable

		public override IEnumerator<ManagedObjectRepresentation> GetEnumerator()
		{
			return this.managedObjects.GetEnumerator();
		}

		#endregion Implementation of IEnumerable
	}
}