﻿using Cumulocity.SDK.Client.Rest.Representation.Tenant;

namespace Cumulocity.SDK.Client.Rest.API.Option
{
	public interface ITenantOptionCollection : IPagedCollectionResource<OptionRepresentation, PagedTenantOptionCollectionRepresentation<OptionCollectionRepresentation>>
	{
	}
}