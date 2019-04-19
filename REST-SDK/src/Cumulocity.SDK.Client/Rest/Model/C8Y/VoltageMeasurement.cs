using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_VoltageMeasurement")]
    public class VoltageMeasurement
    {
        public const string VOLTAGE_UNIT = "V";
        private MeasurementValue voltage;

        [JsonProperty(PropertyName = "voltage")]
        public virtual MeasurementValue Voltage
        {
            get
            {
                return voltage;
            }
            set
            {
                this.voltage = value;
            }
        }


        [JsonIgnore]
        public virtual decimal VoltageValue
        {
            get
            {
                return (decimal) (voltage == null ? null : voltage.Value);
            }
            set
            {
                voltage = new MeasurementValue(VOLTAGE_UNIT);
                voltage.Value = value;
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
            if (!(obj is VoltageMeasurement))
            {
                return false;
            }

            VoltageMeasurement vm = (VoltageMeasurement)obj;
            return voltage == null ? vm.voltage == null : voltage.Equals(vm.voltage);
        }

        public override int GetHashCode()
        {
            return voltage == null ? 0 : voltage.GetHashCode();
        }
    }
}
