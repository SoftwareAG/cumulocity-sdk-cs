using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_MeasurementRequestOperation")]
    [Serializable]
    public class MeasurementRequestOperation
    { 
        private const long serialVersionUID = -2731997499381254447L;

        private string requestName;

        public MeasurementRequestOperation()
        {
        }

        public MeasurementRequestOperation(string requestName)
        {
            this.requestName = requestName;
        }
        [JsonProperty(PropertyName = "RequestName")]
        public virtual string RequestName
        {
            get
            {
                return requestName;
            }
            set
            {
                this.requestName = value;
            }
        }


        public override string ToString()
        {
            return string.Format("MeasurementRequestOperation [requestName={0}]", requestName);
        }
    }
}
