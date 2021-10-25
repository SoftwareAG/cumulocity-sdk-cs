using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public interface INotificationSubscriptionApi
    {
        /// <summary>
        /// Creates a subscription to a source
        /// </summary>
        /// <param name="representation">
        /// </param>
        /// <exception cref="SDKException"> </exception>
        NotificationSubscriptionRepresentation subscribe(NotificationSubscriptionRepresentation representation);
        
        INotificationSubscriptionCollection getSubscriptions();

        /// <summary>
        /// Gets all the subscriptions matching a filter. If the filter is null, return all subscriptions.
        /// </summary>
        /// <param name="filter">
        /// </param>
        /// <exception cref="SDKException"> </exception>
        INotificationSubscriptionCollection getSubscriptionsByFilter(NotificationSubscriptionFilter filter);

        /// <summary>
        /// Delete by object.
        /// </summary>
        /// <param name="subscription">
        /// </param>
        /// <exception cref="SDKException"> </exception>
        void delete(NotificationSubscriptionRepresentation subscription);

        /// <summary>
        /// Delete by ID.
        /// </summary>
        /// <param name="subscriptionId">
        /// </param>
        /// <exception cref="SDKException"> </exception>
        void deleteById(String subscriptionId);

        /// <summary>
        /// Delete all subscriptions matching a filter.
        /// </summary>
        /// <param name="filter">
        /// </param>
        void deleteByFilter(NotificationSubscriptionFilter filter);

        /// <summary>
        /// Deletes all subscriptions to a source in managed object context.
        /// </summary>
        /// <param name="source">
        /// </param>
        void deleteBySource(string source);

        /// <summary>
        /// Deletes all subscriptions of the current tenant.
        /// </summary>
        void deleteTenantSubscriptions();
    }
}
