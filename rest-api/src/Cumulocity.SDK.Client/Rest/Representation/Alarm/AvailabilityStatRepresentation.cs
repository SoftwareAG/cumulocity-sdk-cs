using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	public class AvailabilityStatRepresentation : BaseResourceRepresentation
	{

		private string source;

		private long? downtimeDuration;

		public AvailabilityStatRepresentation()
		{
		}

		public AvailabilityStatRepresentation(string source, long? downtimeDuration)
		{
			this.source = source;
			this.downtimeDuration = downtimeDuration;
		}

		[JsonProperty(propertyName: "source", NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Source
		{
			get
			{
				return source;
			}
			set
			{
				this.source = value;
			}
		}

		[JsonProperty(propertyName: "downtimeDuration", NullValueHandling = NullValueHandling.Ignore)]
		public virtual long? DowntimeDuration
		{
			get
			{
				return downtimeDuration;
			}
			set
			{
				this.downtimeDuration = value;
			}
		}


	}

}
