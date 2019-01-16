using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Option
{
	public class PagedTenantOptionCollectionRepresentation<T> : OptionCollectionRepresentation, IPagedCollectionRepresentation<OptionRepresentation>
		where T : OptionCollectionRepresentation
	{
		private readonly IPagedCollectionResource<OptionRepresentation, T> collectionResource;

		public PagedTenantOptionCollectionRepresentation(OptionCollectionRepresentation collection, IPagedCollectionResource<OptionRepresentation, T> collectionResource)
		{
			Options = collection.Options;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<OptionRepresentation> allPages()
		{
			return new PagedCollectionIterable<OptionRepresentation, OptionCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<OptionRepresentation> elements(int limit)
		{
			return new PagedCollectionIterable<OptionRepresentation, OptionCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}