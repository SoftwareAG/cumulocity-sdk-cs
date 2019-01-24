using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	public class PagedMeasurementCollectionRepresentation<T> : MeasurementCollectionRepresentation, IPagedCollectionRepresentation<MeasurementRepresentation>
		where T : MeasurementCollectionRepresentation
	{
		private readonly IPagedCollectionResource<MeasurementRepresentation, T> collectionResource; 

		public PagedMeasurementCollectionRepresentation(MeasurementCollectionRepresentation collection, IPagedCollectionResource<MeasurementRepresentation, T> collectionResource)
		{
			Measurements = collection.Measurements;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<MeasurementRepresentation> AllPages()
		{
			return new PagedCollectionIterable<MeasurementRepresentation, MeasurementCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<MeasurementRepresentation> Elements(int limit)
		{
			return new PagedCollectionIterable<MeasurementRepresentation, MeasurementCollectionRepresentation>(collectionResource, this, limit);
		}

	}
}
