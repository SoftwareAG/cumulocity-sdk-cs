using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
    public sealed class PagedManagedObjectCollectionRepresentation<T> : ManagedObjectCollectionRepresentation,
        IPagedCollectionRepresentation<ManagedObjectRepresentation> where T : ManagedObjectCollectionRepresentation
    {

        private readonly IPagedCollectionResource<ManagedObjectRepresentation, T> collectionResource;

        public PagedManagedObjectCollectionRepresentation(ManagedObjectCollectionRepresentation collection, IPagedCollectionResource<ManagedObjectRepresentation, T> collectionResource) 
        {
            ManagedObjects = collection.ManagedObjects;
            PageStatistics = collection.PageStatistics;
            Self = collection.Self;
            Next = collection.Next;
            Prev = collection.Prev;
            this.collectionResource = collectionResource;
        }

        public IEnumerable<ManagedObjectRepresentation> allPages()
        {
            return new PagedCollectionIterable<ManagedObjectRepresentation, ManagedObjectCollectionRepresentation>(collectionResource, this);
        }

        public IEnumerable<ManagedObjectRepresentation> elements(int limit)
        {
            return new PagedCollectionIterable<ManagedObjectRepresentation, ManagedObjectCollectionRepresentation>(collectionResource, this, limit);
        }
    }
}