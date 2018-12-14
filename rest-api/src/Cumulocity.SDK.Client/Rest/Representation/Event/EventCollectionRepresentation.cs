﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.Event
{
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<EventRepresentation> iterator()
		public  IEnumerator<EventRepresentation> iterator()
		{
			return events.GetEnumerator();
		}
	}
}
