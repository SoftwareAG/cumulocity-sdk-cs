using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public abstract class AbstractSampleManagedObjectReferenceRepresentation
    {
        public abstract ManagedObjectReferenceRepresentationBuilder Builder();

        public virtual ManagedObjectReferenceRepresentation Build()
        {
	        return Builder().Build();
        }
    }
}