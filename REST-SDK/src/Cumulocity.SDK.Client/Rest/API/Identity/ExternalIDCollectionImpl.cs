using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Identity
{
	public class ExternalIDCollectionImpl :
		PagedCollectionResourceImpl<ExternalIDRepresentation, ExternalIDCollectionRepresentation,
			PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation>>, IExternalIDCollection
	{
		public ExternalIDCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector,
			url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType => IdentityMediaType.EXTERNAL_ID_COLLECTION;

		protected internal override Type ResponseClassProp => typeof(ExternalIDCollectionRepresentation);

		protected internal override PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation> wrap(
			ExternalIDCollectionRepresentation collection)
		{
			return new PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation>(collection, this);
		}
	}
}