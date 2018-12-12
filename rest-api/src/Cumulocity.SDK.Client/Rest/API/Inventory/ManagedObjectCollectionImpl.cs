using System;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
    public class ManagedObjectCollectionImpl :
        PagedCollectionResourceImpl<ManagedObjectRepresentation, ManagedObjectCollectionRepresentation,
            PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation>>, IManagedObjectCollection
    {
        public ManagedObjectCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
        {
        }

        protected internal override CumulocityMediaType MediaType
        {
            get
            {
                return InventoryMediaType.GetInstance.MANAGED_OBJECT_COLLECTION;
            }
        }

        protected internal override Type ResponseClassProp
        { 
            get { return typeof(ManagedObjectCollectionRepresentation); }
        }


        protected internal override PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation> wrap(ManagedObjectCollectionRepresentation collection)
        {
            return new PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation>(collection, this);
        }
    }
}