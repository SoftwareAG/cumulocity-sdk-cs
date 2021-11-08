using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications
{
    public class NotificationSubscriptionRepresentation : AbstractExtensibleRepresentation, ICloneable
    {
        private string context;
        private string subscription;
        private List<string> fragmentsToCopy;
        private GId? id;
        private ManagedObjectRepresentation source;
        private NotificationSubscriptionFilterRepresentation subscriptionFilter;

        public NotificationSubscriptionRepresentation() { }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(GidConverter))]
        public virtual GId Id
        {
            get => id;
            set => id = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Context
        {
            get => context;
            set => context = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Subscription
        {
            get => subscription;
            set => subscription = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual NotificationSubscriptionFilterRepresentation SubscriptionFilter
        {
            get => subscriptionFilter;
            set => subscriptionFilter = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> FragmentsToCopy
        {
            get => fragmentsToCopy;
            set => fragmentsToCopy = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectRepresentation Source
        {
            get => source;
            set => source = value;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
