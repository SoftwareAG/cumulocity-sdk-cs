using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	public class EventCollectionImpl : PagedCollectionResourceImpl<EventRepresentation, EventCollectionRepresentation, PagedEventCollectionRepresentation<EventCollectionRepresentation>>,
			 IEventCollection
	{
		public EventCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType => EventMediaType.EVENT_COLLECTION;

		protected internal override Type ResponseClassProp => typeof(EventCollectionRepresentation);

		protected internal override PagedEventCollectionRepresentation<EventCollectionRepresentation> wrap(EventCollectionRepresentation collection)
		{
			return new PagedEventCollectionRepresentation<EventCollectionRepresentation>(collection, this);
		}
	}
}