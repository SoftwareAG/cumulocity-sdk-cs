using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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

		//ORIGINAL LINE: @JSONTypeHint(AvailabilityStatRepresentation.class) public List<AvailabilityStatRepresentation> getAvailabilityStats()
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<AvailabilityStatRepresentation> iterator()
		public  IEnumerator<AvailabilityStatRepresentation> iterator()
		{
			return availabilityStats.GetEnumerator();
		}

		public override IEnumerator<AvailabilityStatRepresentation> GetEnumerator()
		{
			return availabilityStats.GetEnumerator();
		}
	}
}
