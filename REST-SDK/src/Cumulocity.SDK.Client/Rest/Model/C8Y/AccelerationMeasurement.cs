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
    [PackageName("c8y_AccelerationMeasurement")]
    public class AccelerationMeasurement 
    {
        public const string ACC_UNIT = "m/s2";

        private MeasurementValue acceleration;

        [JsonProperty(PropertyName = "acceleration")]
        public virtual MeasurementValue Acceleration
        {
            get
            {
                return acceleration;
            }
            set
            {
                this.acceleration = value;
            }
        }

        [JsonIgnore]
        public virtual decimal? AccelerationValue
        {
            get
            {
                return acceleration?.Value;
            }
            set
            {
                acceleration = new MeasurementValue(ACC_UNIT);
                acceleration.Value = value;
            }
        }


        public override int GetHashCode()
        {
            return acceleration != null ? acceleration.GetHashCode() : 0;
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
            if (!(obj is AccelerationMeasurement))
            {
                return false;
            }

            AccelerationMeasurement rhs = (AccelerationMeasurement)obj;
            return acceleration == null ? (rhs.acceleration == null) : acceleration.Equals(rhs.acceleration);
        }

        public override string ToString()
        {
            return "AccelerationMeasurement{" + "acceleration=" + acceleration + '}';
        }
    }
}
