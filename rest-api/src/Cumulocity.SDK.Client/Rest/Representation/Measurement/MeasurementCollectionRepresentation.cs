using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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


		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<MeasurementRepresentation> iterator()
		public  IEnumerator<MeasurementRepresentation> iterator()
		{
			return measurements.GetEnumerator();
		}

		public override IEnumerator<MeasurementRepresentation> GetEnumerator()
		{
			return measurements.GetEnumerator();
		}
	}
}
