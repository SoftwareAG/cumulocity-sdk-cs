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
    [PackageName("c8y_BarometerMeasurement")]
    public class BarometerMeasurement
    {
            public const string PRESS_UNIT = "mbar";
            public const string ALT_UNIT = "m";

            private MeasurementValue p;
            private MeasurementValue alt;

            public virtual MeasurementValue P
            {
                get
                {
                    return p;
                }
                set
                {
                    this.p = value;
                }
            }


            public virtual MeasurementValue Alt
            {
                get
                {
                    return alt;
                }
                set
                {
                    this.alt = value;
                }
            }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "pressure")]
        public virtual decimal? Pressure
            {
                get
                {
                    return p?.Value;
                }
                set
                {
                    p = new MeasurementValue(PRESS_UNIT);
                    p.Value = value;
                }
            }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "altitude")]
        public virtual decimal? Altitude
            {
                get
                {
                    return alt?.Value;
                }
                set
                {
                    alt = new MeasurementValue(ALT_UNIT);
                    alt.Value = value;
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
                if (!(obj is BarometerMeasurement))
                {
                    return false;
                }

                BarometerMeasurement rhs = (BarometerMeasurement)obj;
                bool result = (p == null) ? (rhs.p == null) : p.Equals(rhs.p);
                result = result && ((alt == null) ? (rhs.alt == null) : alt.Equals(rhs.alt));
                return result;
            }

            public override int GetHashCode()
            {
                int result = p == null ? 0 : p.GetHashCode();
                return result * 31 + (alt == null ? 0 : alt.GetHashCode());
            }
        }

    
}
