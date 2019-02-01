using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
	[PackageName("c8y_TemperatureMeasurement")]
	public class TemperatureMeasurement 
	{
		public const string TEMP_UNIT = "C";

		private MeasurementValue t = new MeasurementValue(TEMP_UNIT);

		//ORIGINAL LINE: @JSONProperty("T") public MeasurementValue getT()
		[JsonProperty("T")]
		public virtual MeasurementValue T
		{
			get
			{
				return t;
			}
			set
			{
				this.t = value;
			}
		}


		//ORIGINAL LINE: @JSONProperty(ignore = true) public BigDecimal getTemperature()
		[JsonIgnore]
		public virtual decimal Temperature
		{
			get
			{
				return (decimal) t?.Value;
			}
			set
			{
				t = new MeasurementValue(TEMP_UNIT);
				t.Value = value;
			}
		}


		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj == this)
			{
				return true;
			}
			if (!(obj is TemperatureMeasurement))
			{
				return false;
			}

			TemperatureMeasurement rhs = (TemperatureMeasurement)obj;
			return t == null ? (rhs.t == null) : t.Equals(rhs.t);
		}

		public override int GetHashCode()
		{
			return t == null ? 0 : t.GetHashCode();
		}
	}
}
