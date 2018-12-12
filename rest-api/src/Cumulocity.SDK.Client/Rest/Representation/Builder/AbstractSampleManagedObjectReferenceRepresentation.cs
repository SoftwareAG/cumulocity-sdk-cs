using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public abstract class AbstractSampleManagedObjectReferenceRepresentation
    {
        public abstract ManagedObjectReferenceRepresentationBuilder builder();

        public virtual ManagedObjectReferenceRepresentation build()
        {
            return builder().build();
        }
    }
}