using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	public interface IMeasurementCollection : IPagedCollectionResource<MeasurementRepresentation, PagedMeasurementCollectionRepresentation<MeasurementCollectionRepresentation>>
	{
	}
}
