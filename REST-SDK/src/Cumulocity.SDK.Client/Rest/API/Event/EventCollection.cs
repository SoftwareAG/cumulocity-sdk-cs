using Cumulocity.SDK.Client.Rest.Representation.Event;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	public interface IEventCollection : IPagedCollectionResource<EventRepresentation, PagedEventCollectionRepresentation<EventCollectionRepresentation>>
	{
	}
}