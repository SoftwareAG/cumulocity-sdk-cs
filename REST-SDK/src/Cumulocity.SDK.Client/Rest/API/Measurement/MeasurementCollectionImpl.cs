using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	public class MeasurementCollectionImpl : PagedCollectionResourceImpl<MeasurementRepresentation, MeasurementCollectionRepresentation, PagedMeasurementCollectionRepresentation<MeasurementCollectionRepresentation>>,
		 IMeasurementCollection
	{
		public MeasurementCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType => MeasurementMediaType.MEASUREMENT_COLLECTION;

		protected internal override Type ResponseClassProp => typeof(MeasurementCollectionRepresentation);

		protected internal override PagedMeasurementCollectionRepresentation<MeasurementCollectionRepresentation> wrap(MeasurementCollectionRepresentation collection)
		{
			return new PagedMeasurementCollectionRepresentation<MeasurementCollectionRepresentation>(collection, this);
		}
	}
}