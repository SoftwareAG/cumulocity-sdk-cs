using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Event
{
	public class EventsApiRepresentation : AbstractExtensibleRepresentation
	{
		private EventCollectionRepresentation events;

		private string eventsForType;

		private string eventsForSource;

		private string eventsForSourceAndType;

		private string eventsForTime;

		private string eventsForSourceAndTime;

		private string eventsForTimeAndType;

		private string eventsForSourceAndTimeAndType;

		private string eventsForFragmentType;

		private string eventsForSourceAndFragmentType;

		private string eventsForDateAndFragmentType;

		private string eventsForFragmentTypeAndType;

		private string eventsForSourceAndDateAndFragmentType;

		private string eventsForSourceAndFragmentTypeAndType;

		private string eventsForDateAndFragmentTypeAndType;

		private string eventsForSourceAndDateAndFragmentTypeAndType;

		public virtual EventCollectionRepresentation Events
		{
			get
			{
				return events;
			}
			set
			{
				this.events = value;
			}
		}

		public virtual string EventsForType
		{
			get
			{
				return eventsForType;
			}
			set
			{
				this.eventsForType = value;
			}
		}

		public virtual string EventsForSource
		{
			get
			{
				return eventsForSource;
			}
			set
			{
				this.eventsForSource = value;
			}
		}

		public virtual string EventsForSourceAndType
		{
			get
			{
				return eventsForSourceAndType;
			}
			set
			{
				this.eventsForSourceAndType = value;
			}
		}

		public virtual string EventsForTime
		{
			get
			{
				return eventsForTime;
			}
			set
			{
				this.eventsForTime = value;
			}
		}

		public virtual string EventsForSourceAndTime
		{
			get
			{
				return eventsForSourceAndTime;
			}
			set
			{
				this.eventsForSourceAndTime = value;
			}
		}

		public virtual string EventsForTimeAndType
		{
			get
			{
				return eventsForTimeAndType;
			}
			set
			{
				this.eventsForTimeAndType = value;
			}
		}

		public virtual string EventsForSourceAndTimeAndType
		{
			get
			{
				return eventsForSourceAndTimeAndType;
			}
			set
			{
				this.eventsForSourceAndTimeAndType = value;
			}
		}

		public virtual string EventsForFragmentType
		{
			get
			{
				return eventsForFragmentType;
			}
			set
			{
				this.eventsForFragmentType = value;
			}
		}

		public virtual string EventsForSourceAndFragmentType
		{
			get
			{
				return eventsForSourceAndFragmentType;
			}
			set
			{
				this.eventsForSourceAndFragmentType = value;
			}
		}

		public virtual string EventsForDateAndFragmentType
		{
			get
			{
				return eventsForDateAndFragmentType;
			}
			set
			{
				this.eventsForDateAndFragmentType = value;
			}
		}

		public virtual string EventsForFragmentTypeAndType
		{
			get
			{
				return eventsForFragmentTypeAndType;
			}
			set
			{
				this.eventsForFragmentTypeAndType = value;
			}
		}

		public virtual string EventsForSourceAndDateAndFragmentType
		{
			get
			{
				return eventsForSourceAndDateAndFragmentType;
			}
			set
			{
				this.eventsForSourceAndDateAndFragmentType = value;
			}
		}

		public virtual string EventsForSourceAndFragmentTypeAndType
		{
			get
			{
				return eventsForSourceAndFragmentTypeAndType;
			}
			set
			{
				this.eventsForSourceAndFragmentTypeAndType = value;
			}
		}

		public virtual string EventsForDateAndFragmentTypeAndType
		{
			get
			{
				return eventsForDateAndFragmentTypeAndType;
			}
			set
			{
				this.eventsForDateAndFragmentTypeAndType = value;
			}
		}

		public virtual string EventsForSourceAndDateAndFragmentTypeAndType
		{
			get
			{
				return eventsForSourceAndDateAndFragmentTypeAndType;
			}
			set
			{
				this.eventsForSourceAndDateAndFragmentTypeAndType = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<string> URITemplates
		{
			get
			{
				IList<string> uriTemplates = new List<string>();
				uriTemplates.Add(this.EventsForDateAndFragmentType);
				uriTemplates.Add(this.EventsForDateAndFragmentTypeAndType);
				uriTemplates.Add(this.EventsForFragmentType);
				uriTemplates.Add(this.EventsForFragmentTypeAndType);
				uriTemplates.Add(this.EventsForSource);
				uriTemplates.Add(this.EventsForSourceAndDateAndFragmentType);
				uriTemplates.Add(this.EventsForSourceAndDateAndFragmentTypeAndType);
				uriTemplates.Add(this.EventsForSourceAndFragmentType);
				uriTemplates.Add(this.EventsForSourceAndFragmentTypeAndType);
				uriTemplates.Add(this.EventsForSourceAndTime);
				uriTemplates.Add(this.EventsForSourceAndTimeAndType);
				uriTemplates.Add(this.EventsForSourceAndType);
				uriTemplates.Add(this.EventsForTime);
				uriTemplates.Add(this.EventsForTimeAndType);
				uriTemplates.Add(this.EventsForType);
				return uriTemplates;
			}
		}
	}
}