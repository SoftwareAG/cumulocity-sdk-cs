using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public class ManagedObjectCollectionImpl :
		PagedCollectionResourceImpl<ManagedObjectRepresentation, ManagedObjectCollectionRepresentation,
			PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation>>, IManagedObjectCollection
	{
		public ManagedObjectCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType => InventoryMediaType.GetInstance.MANAGED_OBJECT_COLLECTION;

		protected internal override Type ResponseClassProp => typeof(ManagedObjectCollectionRepresentation);

		protected internal override PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation> wrap(ManagedObjectCollectionRepresentation collection)
		{
			return new PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation>(collection, this);
		}
	}
}