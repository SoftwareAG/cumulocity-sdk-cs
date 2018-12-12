using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public class ManagedObjectReferenceRepresentationBuilder
    {

        private ManagedObjectRepresentation mo;

        private string self;

        public virtual ManagedObjectReferenceRepresentationBuilder withMo(ManagedObjectRepresentation mo)
        {
            this.mo = mo;
            return this;
        }

        public virtual ManagedObjectReferenceRepresentationBuilder withSelf(string self)
        {
            this.self = self;
            return this;
        }

        public virtual ManagedObjectReferenceRepresentation build()
        {
            ManagedObjectReferenceRepresentation @ref = new ManagedObjectReferenceRepresentation();
            @ref.ManagedObject = mo;
            @ref.Self = self;
            return @ref;
        }
    }
}