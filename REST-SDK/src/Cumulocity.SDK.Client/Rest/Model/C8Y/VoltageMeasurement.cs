/*
 * Copyright (C) 2019 Cumulocity GmbH
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
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
