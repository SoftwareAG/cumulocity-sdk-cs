using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
    public class ExternalIDCollectionRepresentation : BaseCollectionRepresentation<ExternalIDRepresentation>
    {
        private IList<ExternalIDRepresentation> externalIds = new List<ExternalIDRepresentation>();

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONTypeHint(ExternalIDRepresentation.class) public List<ExternalIDRepresentation> getExternalIds()
        public virtual IList<ExternalIDRepresentation> ExternalIds
        {
            get => externalIds;
            set => externalIds = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
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