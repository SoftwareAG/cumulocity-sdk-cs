using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	[JsonObject]
	public class AvailabilityStatCollectionRepresentation : BaseCollectionRepresentation<AvailabilityStatRepresentation>
	{
		private IList<AvailabilityStatRepresentation> availabilityStats;

		public AvailabilityStatCollectionRepresentation()
		{
		}

		public AvailabilityStatCollectionRepresentation(IList<AvailabilityStatRepresentation> availabilityStats)
		{
			this.availabilityStats = availabilityStats;
		}

		public virtual IList<AvailabilityStatRepresentation> AvailabilityStats
		{
			get
			{
				return availabilityStats;
			}
			set
			{
				this.availabilityStats = value;
			}
		}

		public override IEnumerator<AvailabilityStatRepresentation> GetEnumerator()
		{
			return availabilityStats.GetEnumerator();
		}
	}
}