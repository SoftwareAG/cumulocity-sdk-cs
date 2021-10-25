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

        public override bool Equals(object obj)
        {
            return obj is NotificationSubscriptionFilterRepresentation representation &&
                   EqualityComparer<List<string>>.Default.Equals(apis, representation.apis) &&
                   typeFilter == representation.typeFilter;
        }

        public override int GetHashCode()
        {
            int result = 1;
            object apis = this.Apis;
            result = result * 59 + (apis == null ? 43 : apis.GetHashCode());
            object typeFilter = this.TypeFilter;
            result = result * 59 + (typeFilter == null ? 43 : typeFilter.GetHashCode());
            return result;
        }

        public override string ToString()
        {
            return $"NotificationSubscriptionFilterRepresentation(apis={this.Apis}, typeFilter={this.TypeFilter})";
        }
    }
}
