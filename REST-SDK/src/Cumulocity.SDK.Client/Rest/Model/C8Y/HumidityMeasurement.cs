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
