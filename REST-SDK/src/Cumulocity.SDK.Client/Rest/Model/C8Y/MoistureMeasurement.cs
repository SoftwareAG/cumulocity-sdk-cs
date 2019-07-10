﻿/*
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
