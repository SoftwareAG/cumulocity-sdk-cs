using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
	[JsonObject]
	public class ExternalIDCollectionRepresentation : BaseCollectionRepresentation<ExternalIDRepresentation>
    {
        private IList<ExternalIDRepresentation> externalIds = new List<ExternalIDRepresentation>();

       //ORIGINAL LINE: @JSONTypeHint(ExternalIDRepresentation.class) public List<ExternalIDRepresentation> getExternalIds()
        public virtual IList<ExternalIDRepresentation> ExternalIds
        {
            get => externalIds;
            set => externalIds = value;
        }

        //ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<ExternalIDRepresentation> iterator()
        public IEnumerator<ExternalIDRepresentation> iterator()
        {
            return externalIds.GetEnumerator();
        }

        public override IEnumerator<ExternalIDRepresentation> GetEnumerator()
        {
            return externalIds.GetEnumerator();
        }
    }
}