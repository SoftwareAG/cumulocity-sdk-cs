using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_MoistureMeasurement")]
    public class MoistureMeasurement
    {
        public const string MOISTURE_UNIT = "%";
        private MeasurementValue moisture;

        [JsonProperty(PropertyName = "moisture")]
        public virtual MeasurementValue Moisture
        {
            get
            {
                return moisture;
            }
            set
            {
                this.moisture = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "moistureValue")]
        public virtual decimal MoistureValue
        {
            get
            {
                return (decimal) (moisture == null ? null : moisture.Value);
            }
            set
            {
                moisture = new MeasurementValue(MOISTURE_UNIT);
                moisture.Value = value;
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

            MoistureMeasurement mm = (MoistureMeasurement)obj;
            return moisture == null ? mm.moisture == null : moisture.Equals(mm.moisture);
        }

        public override int GetHashCode()
        {
            return moisture == null ? 0 : moisture.GetHashCode();
        }
    }

}
