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
        private static readonly string MediaTypePart = "application/vnd.com.nsn.cumulocity.";
        private static readonly string DataTypeCharsetAndVersion = "+json;charset=UTF-8;ver=0.9";
        public static readonly NotificationSubscriptionMediaType NOTIFICATION_SUBSCRIPTION = new NotificationSubscriptionMediaType("subscription");
        public static readonly string NOTIFICATION_SUBSCRIPTION_TYPE = $"{MediaTypePart}subscription{DataTypeCharsetAndVersion}";
        public static readonly NotificationSubscriptionMediaType NOTIFICATION_SUBSCRIPTION_COLLECTION = new NotificationSubscriptionMediaType("subscriptionCollection");
        public static readonly string NOTIFICATION_SUBSCRIPTION_COLLECTION_TYPE = $"{MediaTypePart}subscriptionCollection{DataTypeCharsetAndVersion}";
        public static readonly NotificationSubscriptionMediaType NOTIFICATION_SUBSCRIPTION_API = new NotificationSubscriptionMediaType("subscriptionApi");
        public static readonly string NOTIFICATION_SUBSCRIPTION_API_TYPE = $"{MediaTypePart}subscriptionApi{DataTypeCharsetAndVersion}";
    }
}
