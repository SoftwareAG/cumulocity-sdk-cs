using Cumulocity.SDK.Client.Rest.Representation.Measurement;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	public interface IMeasurementCollection : IPagedCollectionResource<MeasurementRepresentation, PagedMeasurementCollectionRepresentation<MeasurementCollectionRepresentation>>
	{
	}
}