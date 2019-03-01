using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Measurement
{
	[JsonObject]
	public class MeasurementCollectionRepresentation : BaseCollectionRepresentation<MeasurementRepresentation>
	{
		private IList<MeasurementRepresentation> measurements;

		public virtual IList<MeasurementRepresentation> Measurements
		{
			get
			{
				return measurements;
			}
			set
			{
				this.measurements = value;
			}
		}

		public override IEnumerator<MeasurementRepresentation> GetEnumerator()
		{
			return measurements.GetEnumerator();
		}
	}
}