using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications
{
    public class NotificationSubscriptionMediaType : CumulocityMediaType
    {
        public NotificationSubscriptionMediaType(string str) : base(str)
        {

        }
        public static readonly NotificationSubscriptionMediaType NOTIFICATION_SUBSCRIPTION = new NotificationSubscriptionMediaType("subscription");
        public static readonly String NOTIFICATION_SUBSCRIPTION_TYPE = "application/vnd.com.nsn.cumulocity.subscription+json;charset=UTF-8;ver=0.9";
        public static readonly NotificationSubscriptionMediaType NOTIFICATION_SUBSCRIPTION_COLLECTION = new NotificationSubscriptionMediaType("subscriptionCollection");
        public static readonly String NOTIFICATION_SUBSCRIPTION_COLLECTION_TYPE = "application/vnd.com.nsn.cumulocity.subscriptionCollection+json;charset=UTF-8;ver=0.9";
        public static readonly NotificationSubscriptionMediaType NOTIFICATION_SUBSCRIPTION_API = new NotificationSubscriptionMediaType("subscriptionApi");
        public static readonly String NOTIFICATION_SUBSCRIPTION_API_TYPE = "application/vnd.com.nsn.cumulocity.subscriptionApi+json;charset=UTF-8;ver=0.9";
    }
}
