using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Inventory
{
    [JsonObject]
    public class
        ManagedObjectReferenceCollectionRepresentation : BaseCollectionRepresentation<
            ManagedObjectReferenceRepresentation>
    {
        private IList<ManagedObjectReferenceRepresentation> references;

        public ManagedObjectReferenceCollectionRepresentation()
        {
            references = new List<ManagedObjectReferenceRepresentation>();
        }

        //ORIGINAL LINE: @JSONTypeHint(ManagedObjectReferenceRepresentation.class) public List<ManagedObjectReferenceRepresentation> getReferences()
        public virtual IList<ManagedObjectReferenceRepresentation> References
        {
            get => references;
            set => references = value;
        }


        //ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<ManagedObjectReferenceRepresentation> iterator()
        public IEnumerator<ManagedObjectReferenceRepresentation> iterator()
        {
            return references.GetEnumerator();
        }

        public override IEnumerator<ManagedObjectReferenceRepresentation> GetEnumerator()
        {
            return this.references.GetEnumerator();
        }
    }
}