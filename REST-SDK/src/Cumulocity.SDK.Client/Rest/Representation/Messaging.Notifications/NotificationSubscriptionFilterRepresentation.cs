using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications
{
    public class NotificationSubscriptionFilterRepresentation : BaseResourceRepresentation
    {
        // Filter out Measurements, Events, Alarms etc mentioned in the list. Wildcards also allowed. 
        private List<string> apis;
        private string typeFilter;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Apis
        {
            get => apis;
            set => apis = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string TypeFilter
        {
            get => typeFilter;
            set => typeFilter = value;
        }

        public override string ToString()
        {
            return $"NotificationSubscriptionFilterRepresentation(apis={this.Apis}, typeFilter={this.TypeFilter})";
        }
    }
}
