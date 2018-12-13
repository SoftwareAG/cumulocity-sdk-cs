
using System;
using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Identity;

public class ExternalIDCollectionImpl : PagedCollectionResourceImpl<ExternalIDRepresentation, ExternalIDCollectionRepresentation, PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation>>, IExternalIDCollection
{

	public ExternalIDCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
	{
	}

	protected internal override CumulocityMediaType MediaType
	{
		get
		{
			return IdentityMediaType.EXTERNAL_ID_COLLECTION;
		}
	}

	//protected internal override Type<ExternalIDCollectionRepresentation> ResponseClass
	//{
	//	get
	//	{
	//		return typeof(ExternalIDCollectionRepresentation);
	//	}
	//}

	protected internal override Type ResponseClassProp
	{
		get { return typeof(ExternalIDCollectionRepresentation); }
	}
	protected internal override PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation> wrap(ExternalIDCollectionRepresentation collection)
	{
		return new PagedExternalIDCollectionRepresentation<ExternalIDCollectionRepresentation>(collection, this);
	}
}
