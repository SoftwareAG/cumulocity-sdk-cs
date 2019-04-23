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
    [PackageName("c8y_GpsQuality")]
    public class GpsQuality 
    {

        public const string QUALITY_UNIT = "%";

        private MeasurementValue satellites;
        private MeasurementValue quality;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "satellites")]
        public virtual MeasurementValue Satellites
        {
            get
            {
                return satellites;
            }
            set
            {
                this.satellites = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "quality")]
        public virtual MeasurementValue Quality
        {
            get
            {
                return quality;
            }
            set
            {
                this.quality = value;
            }
        }


        public virtual decimal getSatellitesValue()
        {
            return (decimal) satellites?.Value;
        }

        public virtual void setSatellitesValue(int satellitesValue)
        {
            satellites = new MeasurementValue();
            satellites.Value = new decimal(satellitesValue);
        }

        [JsonIgnore]
        public virtual decimal QualityValue
        {
            get
            {
                return (decimal) (quality == null ? null : quality.Value);
            }
            set
            {
                quality = new MeasurementValue(QUALITY_UNIT);
                quality.Value = value;
            }
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((quality == null) ? 0 : quality.GetHashCode());
            result = prime * result + ((satellites == null) ? 0 : satellites.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            GpsQuality other = (GpsQuality)obj;
            if (quality == null)
            {
                if (other.quality != null)
                {
                    return false;
                }
            }
            else if (!quality.Equals(other.quality))
            {
                return false;
            }
            if (satellites == null)
            {
                if (other.satellites != null)
                {
                    return false;
                }
            }
            else if (!satellites.Equals(other.satellites))
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "GpsQuality [satellites=" + satellites + ", quality=" + quality + "]";
        }



    }
}
