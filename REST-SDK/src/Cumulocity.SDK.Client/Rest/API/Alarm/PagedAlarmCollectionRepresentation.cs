using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	public class PagedAlarmCollectionRepresentation<T> : AlarmCollectionRepresentation, IPagedCollectionRepresentation<AlarmRepresentation>
		where T : AlarmCollectionRepresentation
	{
		private readonly IPagedCollectionResource<AlarmRepresentation, T> collectionResource;

		public PagedAlarmCollectionRepresentation(AlarmCollectionRepresentation collection, IPagedCollectionResource<AlarmRepresentation, T> collectionResource)
		{
			Alarms = collection.Alarms;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<AlarmRepresentation> AllPages()
		{
			return new PagedCollectionIterable<AlarmRepresentation, AlarmCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<AlarmRepresentation> Elements(int limit)
		{
			return new PagedCollectionIterable<AlarmRepresentation, AlarmCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}