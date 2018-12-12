using System.Collections;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	[JsonObject]
	public class ManagedObjectCollectionRepresentation : BaseCollectionRepresentation<ManagedObjectRepresentation>//, IEnumerable<ManagedObjectRepresentation>
	{
	
		private IList<ManagedObjectRepresentation> managedObjects = new List<ManagedObjectRepresentation>();

		public ManagedObjectCollectionRepresentation()
		{
		}

		//ORIGINAL LINE: @JSONTypeHint(ManagedObjectRepresentation.class) public List<ManagedObjectRepresentation> getManagedObjects()
		
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

		//ORIGINAL LINE: @JSONProperty(ignore = true) public Iterator<ManagedObjectRepresentation> iterator()
		
		public virtual IEnumerator<ManagedObjectRepresentation> iterator()
		{
			return this.managedObjects.GetEnumerator();
		}

		#region Implementation of IEnumerable
		public override IEnumerator<ManagedObjectRepresentation> GetEnumerator()
		{
			return this.managedObjects.GetEnumerator();
		}

//		IEnumerator IEnumerable.GetEnumerator()
//		{
//			return GetEnumerator();
//		}
		#endregion
	}
}