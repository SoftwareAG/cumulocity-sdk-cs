using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	public class PagedAlarmCollectionRepresentation<T> : AlarmCollectionRepresentation, IPagedCollectionRepresentation<AlarmRepresentation>
		where T : AlarmCollectionRepresentation
	{

		//ORIGINAL LINE: private final PagedCollectionResource<AlarmRepresentation, ? extends AlarmCollectionRepresentation> collectionResource;
		private readonly IPagedCollectionResource<AlarmRepresentation, T> collectionResource;

		public PagedAlarmCollectionRepresentation(AlarmCollectionRepresentation collection, IPagedCollectionResource<AlarmRepresentation,T> collectionResource)
		{
			Alarms = collection.Alarms;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public  IEnumerable<AlarmRepresentation> allPages()
		{
			return new PagedCollectionIterable<AlarmRepresentation, AlarmCollectionRepresentation>(collectionResource, this);
		}

		public  IEnumerable<AlarmRepresentation> elements(int limit)
		{
			return new PagedCollectionIterable<AlarmRepresentation, AlarmCollectionRepresentation>(collectionResource, this, limit);
		}
	}

}
