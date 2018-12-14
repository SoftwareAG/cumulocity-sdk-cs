using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Identity;

namespace Cumulocity.SDK.Client.Rest.API.Event
{

	public class EventCollectionImpl : PagedCollectionResourceImpl<EventRepresentation, EventCollectionRepresentation, PagedEventCollectionRepresentation<EventCollectionRepresentation>>,
		     IEventCollection
	{
		public EventCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType
		{
			get
			{
				return EventMediaType.EVENT_COLLECTION;
			}
		}
		protected internal override Type ResponseClassProp => typeof(EventCollectionRepresentation);

		protected internal override PagedEventCollectionRepresentation<EventCollectionRepresentation> wrap(EventCollectionRepresentation collection)
		{
			return new PagedEventCollectionRepresentation<EventCollectionRepresentation>(collection, this);
		}
	}
}
