using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_HumidityMeasurement")]

    public class HumidityMeasurement 
    {
        public const string HUM_UNIT = "%RH";

        private MeasurementValue h;

        public virtual MeasurementValue H
        {
            get
            {
                return h;
            }
            set
            {
                this.h = value;
            }
        }


        [JsonIgnore]
        public virtual decimal Humidity
        {
            get
            {
                return (decimal) h.Value;
            }
            set
            {
                h = new MeasurementValue(HUM_UNIT);
                h.Value = value;
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
            if (!(obj is HumidityMeasurement))
            {
                return false;
            }

            HumidityMeasurement rhs = (HumidityMeasurement)obj;
            return h == null ? (rhs.h == null) : h.Equals(rhs.h);
        }

        public override int GetHashCode()
        {
            return h == null ? 0 : h.GetHashCode();
        }
    }
}
