using Newtonsoft.Json;
using System.Collections.Generic;

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