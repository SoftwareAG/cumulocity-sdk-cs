using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Identity
{
	public class PagedExternalIDCollectionRepresentation<T> : ExternalIDCollectionRepresentation,
		IPagedCollectionRepresentation<ExternalIDRepresentation> where T : ExternalIDCollectionRepresentation
	{
		private readonly IPagedCollectionResource<ExternalIDRepresentation, T> collectionResource;

		public PagedExternalIDCollectionRepresentation(ExternalIDCollectionRepresentation collection, IPagedCollectionResource<ExternalIDRepresentation, T> collectionResource)
		{
			//ManagedObjects = collection.ManagedObjects;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<ExternalIDRepresentation> allPages()
		{
			return new PagedCollectionIterable<ExternalIDRepresentation, ExternalIDCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<ExternalIDRepresentation> elements(int limit)
		{
			return new PagedCollectionIterable<ExternalIDRepresentation, ExternalIDCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}
