using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
	[JsonObject]
	public class ExternalIDCollectionRepresentation : BaseCollectionRepresentation<ExternalIDRepresentation>
    {
        private IList<ExternalIDRepresentation> externalIds = new List<ExternalIDRepresentation>();

        public virtual IList<ExternalIDRepresentation> ExternalIds
        {
            get => externalIds;
            set => externalIds = value;
        }

        public override IEnumerator<ExternalIDRepresentation> GetEnumerator()
        {
            return externalIds.GetEnumerator();
        }
    }
}