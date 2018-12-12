using System;
using System.Net;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation
{
    public class BaseResourceRepresentation : IResourceRepresentation
    {
        private string self;

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public virtual string Self
        {
            get => self;
            set => self = value;
        }


        [JsonIgnore]
        public virtual string SelfDecoded
        {
            get
            {
                if (ReferenceEquals(self, null)) return null;
                try
                {
                    return WebUtility.UrlEncode(self);
                }
                catch (Exception)
                {
                    return self;
                }
            }
        }

        public override string ToString()
        {
            return toJSON();
        }

        public virtual string toJSON()
        {
            //return JSONBase.JSONGenerator.forValue(this);
            return JsonConvert.SerializeObject(this);
        }
    }
}