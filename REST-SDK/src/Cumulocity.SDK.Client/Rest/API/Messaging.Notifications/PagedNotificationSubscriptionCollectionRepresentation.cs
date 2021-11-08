using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class PagedNotificationSubscriptionCollectionRepresentation : NotificationSubscriptionCollectionRepresentation, IPagedCollectionRepresentation<NotificationSubscriptionRepresentation>
    {
        private readonly IPagedCollectionResource<NotificationSubscriptionRepresentation, NotificationSubscriptionCollectionRepresentation> collectionResource;

        public PagedNotificationSubscriptionCollectionRepresentation(NotificationSubscriptionCollectionRepresentation collection, IPagedCollectionResource<NotificationSubscriptionRepresentation, NotificationSubscriptionCollectionRepresentation> collectionResource) 
        {
            this.Subscriptions = collection.Subscriptions;
            this.PageStatistics = collection.PageStatistics;
            this.Self = collection.Self;
            this.Next = collection.Next;
            this.Prev = collection.Prev;
            this.collectionResource = collectionResource;
        }

        public IEnumerable<NotificationSubscriptionRepresentation> AllPages()
        {
            return new PagedCollectionIterable<NotificationSubscriptionRepresentation, NotificationSubscriptionCollectionRepresentation>(collectionResource, this);
        }

        public IEnumerable<NotificationSubscriptionRepresentation> Elements(int limit)
        {
            return new PagedCollectionIterable<NotificationSubscriptionRepresentation, NotificationSubscriptionCollectionRepresentation>(collectionResource, this, limit);
        }
    }
}
