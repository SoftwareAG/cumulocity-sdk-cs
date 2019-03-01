using System;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
    public class ManagedObjectReferenceCollectionImpl<T> :
        PagedCollectionResourceImpl<ManagedObjectReferenceRepresentation, ManagedObjectReferenceCollectionRepresentation
            , PagedManagedObjectReferenceCollectionRepresentation<T>>
        , IManagedObjectReferenceCollection<T> where T : ManagedObjectReferenceCollectionRepresentation
    {
        public ManagedObjectReferenceCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
        {
        }

        protected internal override CumulocityMediaType MediaType
        {
            get
            {
                return InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE_COLLECTION;
            }
        }

        protected internal override Type ResponseClassProp
        { 
            get { return typeof(ManagedObjectReferenceCollectionRepresentation); }
        }


//        protected internal override Type ResponseClass
//        {
//            
//                return typeof(ManagedObjectReferenceCollectionRepresentation);
//            
//        }

        protected internal override PagedManagedObjectReferenceCollectionRepresentation<T> wrap(ManagedObjectReferenceCollectionRepresentation collection)
        {
            return new PagedManagedObjectReferenceCollectionRepresentation<T>(collection, this);
        }
    }
}