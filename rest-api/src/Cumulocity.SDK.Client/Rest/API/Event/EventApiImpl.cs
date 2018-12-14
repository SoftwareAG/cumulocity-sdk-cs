using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Event;

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

		//ORIGINAL LINE: private EventsApiRepresentation getEventApiRepresentation() throws SDKException
		private EventsApiRepresentation EventApiRepresentation
		{
			get
			{
				return eventsApiRepresentation;
			}
		}

		//ORIGINAL LINE: @Override public EventRepresentation getEvent(GId eventId) throws SDKException
		public  EventRepresentation getEvent(GId eventId)
		{
			string url = SelfUri + "/" + eventId.Value;
			return restConnector.Get<EventRepresentation>(url, EventMediaType.EVENT, typeof(EventRepresentation));
		}


		//ORIGINAL LINE: @Override public EventCollection getEvents() throws SDKException
		public  IEventCollection Events
		{
			get
			{
				string url = SelfUri;
				return new EventCollectionImpl(restConnector, url, pageSize);
			}
		}
		//ORIGINAL LINE: @Override public EventRepresentation create(EventRepresentation representation) throws SDKException
		public  EventRepresentation create(EventRepresentation representation)
		{
			return restConnector.Post(SelfUri, EventMediaType.EVENT, representation);
		}

		//ORIGINAL LINE: @Override public Future createAsync(EventRepresentation representation) throws SDKException
		//public override Future createAsync(EventRepresentation representation)
		//{
		//	return restConnector.postAsync(SelfUri, EventMediaType.EVENT, representation);
		//}

		//ORIGINAL LINE: @Override public void delete(EventRepresentation event) throws SDKException
		public void delete(EventRepresentation @event)
		{
			string url = SelfUri + "/" + @event.Id.Value;
			restConnector.Delete(url);
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: @Override public void deleteEventsByFilter(EventFilter filter) throws IllegalArgumentException, SDKException
		public void deleteEventsByFilter(EventFilter filter)
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

		//ORIGINAL LINE: @Override public EventCollection getEventsByFilter(EventFilter filter) throws SDKException
		public  IEventCollection getEventsByFilter(EventFilter filter)
		{
			if (filter == null)
			{
				return Events;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new EventCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		//ORIGINAL LINE: private String getSelfUri() throws SDKException
		private string SelfUri
		{
			get
			{
				return EventApiRepresentation.Events.Self;
			}
		}

		//ORIGINAL LINE: @Override public EventRepresentation update(EventRepresentation eventRepresentation) throws SDKException
		public EventRepresentation update(EventRepresentation eventRepresentation)
		{
			string url = SelfUri + "/" + eventRepresentation.Id.Value;
			return restConnector.PutWithoutId(url, EventMediaType.EVENT, eventRepresentation);
		}
	}

}
