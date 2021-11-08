using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications
{
    public class NotificationSubscriptionCollectionRepresentation : BaseCollectionRepresentation<NotificationSubscriptionRepresentation>
    {
        public NotificationSubscriptionCollectionRepresentation() { }

        private List<NotificationSubscriptionRepresentation> subscriptions;

        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<NotificationSubscriptionRepresentation> Subscriptions
        {
            get => subscriptions;
            set => subscriptions = value;
        }

        public override IEnumerator<NotificationSubscriptionRepresentation> GetEnumerator()
        {
            return subscriptions.GetEnumerator();
        }
    }
}
