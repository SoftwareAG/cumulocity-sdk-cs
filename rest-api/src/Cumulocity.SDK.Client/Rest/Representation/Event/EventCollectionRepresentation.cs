using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Event
{
	[JsonObject]
	public class EventCollectionRepresentation : BaseCollectionRepresentation<EventRepresentation>
	{

		private IList<EventRepresentation> events;

		public virtual IList<EventRepresentation> Events
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

		public override IEnumerator<EventRepresentation> GetEnumerator()
		{
			return Events.GetEnumerator();
		}
	}
}
