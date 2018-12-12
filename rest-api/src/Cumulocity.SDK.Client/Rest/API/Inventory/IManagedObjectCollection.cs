using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
    public interface
        IManagedObjectCollection : IPagedCollectionResource<ManagedObjectRepresentation,
            PagedManagedObjectCollectionRepresentation<ManagedObjectCollectionRepresentation>>
    {

    }
}