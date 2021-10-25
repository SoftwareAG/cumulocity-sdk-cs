using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class NotificationSubscriptionApiImpl : INotificationSubscriptionApi
    {
        public static readonly CumulocityMediaType MEDIA_TYPE = new CumulocityMediaType("application", "json");

        public static readonly String SUBSCRIPTION_REQUEST_URI = "notification2/subscriptions";

        private readonly RestConnector restConnector;

        private readonly int pageSize;

        private readonly UrlProcessor urlProcessor;

        public NotificationSubscriptionApiImpl(RestConnector restConnector, int pageSize, UrlProcessor urlProcessor)
        {
            this.restConnector = restConnector ?? throw new ArgumentNullException(nameof(restConnector));
            this.pageSize = pageSize;
            this.urlProcessor = urlProcessor ?? throw new ArgumentNullException(nameof(urlProcessor));
        }

        private string getSelfUri()
        {
            return $"{restConnector.PlatformParameters.Host}{SUBSCRIPTION_REQUEST_URI}";
        }

        public void delete(NotificationSubscriptionRepresentation subscription)
        {
            if(subscription != null)
            {
                deleteById(subscription.Id.Value);
            } 
            else
            {
                throw new ArgumentNullException("Invalid subscription passed");
            }
        }

        public void deleteByFilter(NotificationSubscriptionFilter filter)
        {
            if(filter != null)
            {
                Dictionary<string, string> parameters = (Dictionary<string, string>)filter.QueryParams;
                restConnector.Delete(urlProcessor.replaceOrAddQueryParam(getSelfUri(), parameters));
            }
        }

        public void deleteById(string subscriptionId)
        {
            if(!string.IsNullOrEmpty(subscriptionId))
            {
                string url = $"{getSelfUri()}/{subscriptionId}";
                restConnector.Delete(url);
            }
        }

        public void deleteBySource(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                NotificationSubscriptionFilter filter = new NotificationSubscriptionFilter().bySource(new GId(source));
                deleteByFilter(filter);
            }
        }

        public void deleteTenantSubscriptions()
        {
            throw new NotImplementedException();
        }

        public INotificationSubscriptionCollection getSubscriptions()
        {
            return new NotificationSubscriptionCollectionImpl(restConnector, getSelfUri(), pageSize);
        }

        public INotificationSubscriptionCollection getSubscriptionsByFilter(NotificationSubscriptionFilter filter)
        {
            if(filter == null)
            {
                return getSubscriptions();
            }
            Dictionary<string, string> parameters = (Dictionary<string, string>)filter.QueryParams;
            return new NotificationSubscriptionCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(getSelfUri(), parameters), pageSize);
        }

        public NotificationSubscriptionRepresentation subscribe(NotificationSubscriptionRepresentation representation)
        {
            NotificationSubscriptionRepresentation result = null;
            if (representation != null)
            {
                result = restConnector.Post<NotificationSubscriptionRepresentation>(getSelfUri(), MEDIA_TYPE, representation);
            }
            else
            {
                throw new ArgumentNullException("Invalid representation passed");
            }
            return result;
        }
    }
}
