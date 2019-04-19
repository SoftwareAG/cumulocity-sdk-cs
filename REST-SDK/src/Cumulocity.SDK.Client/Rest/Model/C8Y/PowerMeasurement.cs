using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_PowerMeasurement")]
    public class PowerMeasurement 
    {
        public const string POWER_UNIT = "W";
        private MeasurementValue power;

        [JsonProperty(PropertyName = "Power")]
        public virtual MeasurementValue Power
        {
            get
            {
                return power;
            }
            set
            {
                this.power = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "powerValue")]
        public virtual decimal PowerValue
        {
            get
            {
                return (decimal) (power == null ? null : power.Value);
            }
            set
            {
                power = new MeasurementValue(POWER_UNIT);
                power.Value = value;
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
            if (!(obj is PowerMeasurement))
            {
                return false;
            }

            PowerMeasurement pm = (PowerMeasurement)obj;
            return power == null ? pm.power == null : power.Equals(pm.power);
        }

        public override int GetHashCode()
        {
            return power == null ? 0 : power.GetHashCode();
        }
    }
}
