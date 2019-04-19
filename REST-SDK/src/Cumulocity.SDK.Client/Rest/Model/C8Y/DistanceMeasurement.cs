using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_DistanceMeasurement")]
    public class DistanceMeasurement 
    {

        private MeasurementValue distance;

        public DistanceMeasurement()
        {
        }

        public DistanceMeasurement(MeasurementValue distance)
        {
            this.distance = distance;
        }

        [JsonProperty(PropertyName = "distance")]
        public virtual MeasurementValue Distance
        {
            get
            {
                return distance;
            }
            set
            {
                this.distance = value;
            }
        }

    }
}
