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
    [PackageName("c8y_TemperatureMeasurement")]
    public class TemperatureMeasurement
    {
        public const string TEMP_UNIT = "C";

        private MeasurementValue t = new MeasurementValue(TEMP_UNIT);

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

        [JsonIgnore]
        public virtual decimal Temperature
        {
            get
            {
                return (decimal)t?.Value;
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
