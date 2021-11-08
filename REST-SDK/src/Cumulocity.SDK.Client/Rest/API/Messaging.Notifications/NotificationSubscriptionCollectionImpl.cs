using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class NotificationSubscriptionCollectionImpl : PagedCollectionResourceImpl<NotificationSubscriptionRepresentation, NotificationSubscriptionCollectionRepresentation, PagedNotificationSubscriptionCollectionRepresentation>, INotificationSubscriptionCollection
    {
        public NotificationSubscriptionCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
        {

        }

        protected internal override CumulocityMediaType MediaType => NotificationSubscriptionMediaType.NOTIFICATION_SUBSCRIPTION_COLLECTION;

        protected internal override Type ResponseClassProp => typeof(NotificationSubscriptionCollectionRepresentation);

        protected internal override PagedNotificationSubscriptionCollectionRepresentation wrap(NotificationSubscriptionCollectionRepresentation collection)
        {
            return new PagedNotificationSubscriptionCollectionRepresentation(collection, this);
        }
    }
}
