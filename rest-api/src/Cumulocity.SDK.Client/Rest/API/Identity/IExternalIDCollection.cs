using Cumulocity.SDK.Client.Rest.Representation.Identity;

namespace Cumulocity.SDK.Client.Rest.API.Identity
{
    public interface IExternalIDCollection :
	    IPagedCollectionResource<ExternalIDRepresentation, PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation>>
    {
    }
}