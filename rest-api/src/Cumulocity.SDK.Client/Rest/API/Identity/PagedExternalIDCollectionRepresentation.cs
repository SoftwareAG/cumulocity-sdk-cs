using Cumulocity.SDK.Client.Rest.Representation.Identity;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Identity
{
	public class PagedExternalIDCollectionRepresentation<T> : ExternalIDCollectionRepresentation,
		IPagedCollectionRepresentation<ExternalIDRepresentation> where T : ExternalIDCollectionRepresentation
	{
		private readonly IPagedCollectionResource<ExternalIDRepresentation, T> collectionResource;

		public PagedExternalIDCollectionRepresentation(ExternalIDCollectionRepresentation collection, IPagedCollectionResource<ExternalIDRepresentation, T> collectionResource)
		{
			this.collectionResource = collectionResource;
			ExternalIds = collection.ExternalIds;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
		}

		public IEnumerable<ExternalIDRepresentation> AllPages()
		{
			return new PagedCollectionIterable<ExternalIDRepresentation, ExternalIDCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<ExternalIDRepresentation> Elements(int limit)
		{
			return new PagedCollectionIterable<ExternalIDRepresentation, ExternalIDCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}