using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications
{
    public class NotificationTokenRequestRepresentation : BaseResourceRepresentation
    {
        public NotificationTokenRequestRepresentation() { }

        private string subscriber;
        private string subscription;
        private long expiresInMinutes;
        private bool shared;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Subscriber
        {
            get => subscriber;
            set => subscriber = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Subscription
        {
            get => subscription;
            set => subscription = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual long ExpiresInMinutes
        {
            get => expiresInMinutes;
            set => expiresInMinutes = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool IsShared
        {
            get => shared;
            set => shared = value;
        }

        public NotificationTokenRequestRepresentation(string subscriber, string subscription, long expiresInMinutes, bool shared)
        {
            this.subscriber = subscriber;
            this.subscription = subscription;
            this.expiresInMinutes = expiresInMinutes;
            this.shared = shared;
        }
    }
}
