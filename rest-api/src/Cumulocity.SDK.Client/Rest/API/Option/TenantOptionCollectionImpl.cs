using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;

namespace Cumulocity.SDK.Client.Rest.API.Option
{
	public class TenantOptionCollectionImpl : PagedCollectionResourceImpl<OptionRepresentation, OptionCollectionRepresentation, PagedTenantOptionCollectionRepresentation<OptionCollectionRepresentation>>, ITenantOptionCollection
	{

		public TenantOptionCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType => OptionMediaType.OPTION_COLLECTION;

		protected internal override Type ResponseClassProp => typeof(OptionCollectionRepresentation);

		protected internal override PagedTenantOptionCollectionRepresentation<OptionCollectionRepresentation> wrap(OptionCollectionRepresentation collection)
		{
			return new PagedTenantOptionCollectionRepresentation<OptionCollectionRepresentation>(collection, this);
		}
	}
}
