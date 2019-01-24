using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	public class EventApiImpl : IEventApi
	{
		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private EventsApiRepresentation eventsApiRepresentation;

		private UrlProcessor urlProcessor;

		public EventApiImpl(RestConnector restConnector, UrlProcessor urlProcessor, EventsApiRepresentation eventsApiRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.urlProcessor = urlProcessor;
			this.eventsApiRepresentation = eventsApiRepresentation;
			this.pageSize = pageSize;
		}

		private EventsApiRepresentation EventApiRepresentation
		{
			get
			{
				return eventsApiRepresentation;
			}
		}

		public EventRepresentation GetEvent(GId eventId)
		{
			string url = $"{SelfUri}/{eventId.Value}";
			return restConnector.Get<EventRepresentation>(url, EventMediaType.EVENT, typeof(EventRepresentation));
		}

		public IEventCollection Events
		{
			get
			{
				string url = SelfUri;
				return new EventCollectionImpl(restConnector, url, pageSize);
			}
		}

		public EventRepresentation Create(EventRepresentation representation)
		{
			return restConnector.Post(SelfUri, EventMediaType.EVENT, representation);
		}

		public Task<EventRepresentation> CreateAsync(EventRepresentation representation)
		{
			return restConnector.PostAsync(SelfUri, EventMediaType.EVENT, representation);
		}

		public void Delete(EventRepresentation @event)
		{
			string url = $"{SelfUri}/{@event.Id.Value}";
			restConnector.Delete(url);
		}

		public void DeleteEventsByFilter(EventFilter filter)
		{
			if (filter == null)
			{
				throw new System.ArgumentException("Event filter is null");
			}
			else
			{
				IDictionary<string, string> @params = filter.QueryParams;
				restConnector.Delete(urlProcessor.replaceOrAddQueryParam(SelfUri, @params));
			}
		}

		public IEventCollection getEvents()
		{
			string url = SelfUri;
			return new EventCollectionImpl(restConnector, url, pageSize);
		}

		public IEventCollection GetEventsByFilter(EventFilter filter)
		{
			if (filter == null)
			{
				return Events;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new EventCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		private string SelfUri
		{
			get
			{
				return EventApiRepresentation.Events.Self;
			}
		}

		public EventRepresentation Update(EventRepresentation eventRepresentation)
		{
			string url = $"{SelfUri}/{eventRepresentation.Id.Value}";
			return restConnector.PutWithoutId(url, EventMediaType.EVENT, eventRepresentation);
		}
	}
}