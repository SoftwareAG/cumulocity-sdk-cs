using Cumulocity.SDK.Client.Rest.Representation.Event;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	public class PagedEventCollectionRepresentation<T> : EventCollectionRepresentation, IPagedCollectionRepresentation<EventRepresentation>
		where T : EventCollectionRepresentation
	{
		private readonly IPagedCollectionResource<EventRepresentation, T> collectionResource; //EventCollectionRepresentation

		public PagedEventCollectionRepresentation(EventCollectionRepresentation collection, IPagedCollectionResource<EventRepresentation, T> collectionResource)
		{
			Events = collection.Events;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<EventRepresentation> AllPages()
		{
			return new PagedCollectionIterable<EventRepresentation, EventCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<EventRepresentation> Elements(int limit)
		{
			return new PagedCollectionIterable<EventRepresentation, EventCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}