using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
    public interface
        IManagedObjectReferenceCollection<T> : IPagedCollectionResource<ManagedObjectReferenceRepresentation,
            PagedManagedObjectReferenceCollectionRepresentation<T>>
        where T : ManagedObjectReferenceCollectionRepresentation
    {

    }

}