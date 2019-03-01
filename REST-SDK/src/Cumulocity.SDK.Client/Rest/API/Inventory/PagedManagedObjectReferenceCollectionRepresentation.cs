using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
    public class PagedManagedObjectReferenceCollectionRepresentation<T> : ManagedObjectReferenceCollectionRepresentation,
        IPagedCollectionRepresentation<ManagedObjectReferenceRepresentation>where T : ManagedObjectReferenceCollectionRepresentation
    {
    
        private readonly IPagedCollectionResource<ManagedObjectReferenceRepresentation, T> collectionResource;

        public PagedManagedObjectReferenceCollectionRepresentation(ManagedObjectReferenceCollectionRepresentation collection, 
            dynamic collectionResource) 
        {
            References = collection.References;
            PageStatistics = collection.PageStatistics;
            Self = collection.Self;
            Next = collection.Next;
            Prev = collection.Prev;
            //this.collectionResource = collectionResource;
        }

        public  IEnumerable<ManagedObjectReferenceRepresentation> AllPages()
        {
            return new PagedCollectionIterable<ManagedObjectReferenceRepresentation, ManagedObjectReferenceCollectionRepresentation>(collectionResource, this);
        }

        public  IEnumerable<ManagedObjectReferenceRepresentation> Elements(int limit)
        {
            return new PagedCollectionIterable<ManagedObjectReferenceRepresentation, ManagedObjectReferenceCollectionRepresentation>(collectionResource, this, limit);
        }
    }

}