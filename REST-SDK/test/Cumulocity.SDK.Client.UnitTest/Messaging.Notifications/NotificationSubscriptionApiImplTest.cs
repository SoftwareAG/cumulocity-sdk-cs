using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Messaging.Notifications;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using Cumulocity.SDK.Client.Rest.Utils;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cumulocity.SDK.Client.UnitTest.Messaging.Notifications
{
    public class NotificationSubscriptionApiImplTest
    {
        private Mock<RestConnector> restConnector;
        private Mock<PlatformParameters> platformParameters;
        private Mock<ResponseParser> parser;
        private Mock<UrlProcessor> urlProcessor;
        private INotificationSubscriptionApi api;

        private static readonly int DEFAULT_PAGE_SIZE = 3;
        private static readonly string DEFAULT_HOST = "http://localhost/";
        private static readonly string INVALID_DEFAULT_HOST = "host/";
        private static readonly string DEFAULT_GID_VALUE = "value";
        private static readonly string SUBSCRIPTION_REQUEST_URI = "notification2/subscriptions";
        public static readonly CumulocityMediaType MEDIA_TYPE = new CumulocityMediaType("application", "json");

        public NotificationSubscriptionApiImplTest()
        {
            platformParameters = new Mock<PlatformParameters>();
            platformParameters.Setup(x => x.Host).Returns(DEFAULT_HOST);
            parser = new Mock<ResponseParser>();
            restConnector = new Mock<RestConnector>(platformParameters.Object, parser.Object);
            restConnector.Setup(x => x.PlatformParameters).Returns(platformParameters.Object);
            // Use the base virtual method not overloaded method created by moq. Delete and Post need to be virtual as they vant be used in Verify and setup.
            restConnector.Setup(x => x.Delete(It.IsAny<string>())).CallBase();
            restConnector.Setup(x => x.Post<NotificationSubscriptionRepresentation>(It.IsAny<string>(), It.IsAny<CumulocityMediaType>(), It.IsAny<NotificationSubscriptionRepresentation>())).CallBase();
            urlProcessor = new Mock<UrlProcessor>();
            urlProcessor.Setup(x => x.replaceOrAddQueryParam(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>())).CallBase();
            api = new NotificationSubscriptionApiImpl(restConnector.Object, DEFAULT_PAGE_SIZE, urlProcessor.Object);
        }

        [Fact]
        public void restConnectorCannotBeNullInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new NotificationSubscriptionApiImpl(null, DEFAULT_PAGE_SIZE, urlProcessor.Object));
        }

        [Fact]
        public void urlProcessorCannotBeNullInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new NotificationSubscriptionApiImpl(restConnector.Object, DEFAULT_PAGE_SIZE, null));
        }

        [Fact]
        public void representationCannotBeNullInDelete()
        {
            Assert.Throws<ArgumentNullException>(() => api.delete(null));
        }

        [Fact]
        public void representationCannotBeNullInSubscribe()
        {
            Assert.Throws<ArgumentNullException>(() => api.subscribe(null));
        }

        [Fact]
        public void subscriptionFilterCannotBeNullInDelete()
        {
            Assert.Throws<ArgumentNullException>(() => api.deleteByFilter(null));
        }

        [Fact]
        public void subscriptionIdCannotBeNullInDelete()
        {
            Assert.Throws<ArgumentNullException>(() => api.deleteById(null));
        }

        [Fact]
        public void subscriptionSourceCannotBeNullInDelete()
        {
            Assert.Throws<ArgumentNullException>(() => api.deleteBySource(null));
        }

        [Fact]
        public void testDelete()
        {
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            subscription.Id = new Rest.Model.Idtype.GId(DEFAULT_GID_VALUE);
            // Since for unit test we dont need to use valid tenant host and are not using authentication etc, we can swallow the exception of Unreachable host and just verify if delete was called. 
            try
            {
                api.delete(subscription);
            }
            catch (Exception ex) { }
            restConnector.Verify(x => x.Delete($"{DEFAULT_HOST}{SUBSCRIPTION_REQUEST_URI}/{DEFAULT_GID_VALUE}"));
        }

        [Fact]
        public void testDeleteTenantSubscriptions()
        {
            // Since for unit test we dont need to use valid tenant host and are not using authentication etc, we can swallow the exception of Unreachable host and just verify if delete was called. 
            try
            {
                api.deleteTenantSubscriptions();
            }
            catch (Exception ex) { }
            restConnector.Verify(x => x.Delete($"{DEFAULT_HOST}{SUBSCRIPTION_REQUEST_URI}?=tenant"));
        }

        [Fact]
        public void testDeleteBySource()
        {
            string source = "sampleSource";
            try
            {
                api.deleteBySource(source);
            }
            catch (Exception ex) { }
            restConnector.Verify(x => x.Delete($"{DEFAULT_HOST}{SUBSCRIPTION_REQUEST_URI}?={source}"));
        }

        [Fact]
        public void testDeleteWithInvalidHost()
        {
            // Create new mock for invalid host name not starting with http or https
            var platformParametersInvalidHost = new Mock<PlatformParameters>();
            platformParametersInvalidHost.Setup(x => x.Host).Returns(INVALID_DEFAULT_HOST);
            var restConnectorInvalidHost = new Mock<RestConnector>(platformParametersInvalidHost.Object, parser.Object);
            restConnectorInvalidHost.Setup(x => x.PlatformParameters).Returns(platformParametersInvalidHost.Object);
            restConnectorInvalidHost.Setup(x => x.Delete(It.IsAny<string>())).CallBase();
            api = new NotificationSubscriptionApiImpl(restConnectorInvalidHost.Object, DEFAULT_PAGE_SIZE, urlProcessor.Object);
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            subscription.Id = new Rest.Model.Idtype.GId(DEFAULT_GID_VALUE);
            try
            {
                api.delete(subscription);
            }
            catch(UriFormatException ex) {
                Assert.Throws<UriFormatException>(() => restConnector.Object.Delete($"{INVALID_DEFAULT_HOST}{SUBSCRIPTION_REQUEST_URI}/{DEFAULT_GID_VALUE}"));
            }
        }

        [Fact]
        public void testSubscribe()
        {
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            try
            {
                api.subscribe(subscription);
            }
            catch (Exception ex) { }
            restConnector.Verify(x => x.Post<NotificationSubscriptionRepresentation>(It.IsAny<string>(), It.IsAny<CumulocityMediaType>(), It.IsAny<NotificationSubscriptionRepresentation>()));
        }

        [Fact]
        public void testSubscribeWithInvalidHost()
        {
            // Refer delete method for explanation
            var platformParametersInvalidHost = new Mock<PlatformParameters>();
            platformParametersInvalidHost.Setup(x => x.Host).Returns(INVALID_DEFAULT_HOST);
            var restConnectorInvalidHost = new Mock<RestConnector>(platformParametersInvalidHost.Object, parser.Object);
            restConnectorInvalidHost.Setup(x => x.PlatformParameters).Returns(platformParametersInvalidHost.Object);
            restConnectorInvalidHost.Setup(x => x.Delete(It.IsAny<string>())).CallBase();
            api = new NotificationSubscriptionApiImpl(restConnectorInvalidHost.Object, DEFAULT_PAGE_SIZE, urlProcessor.Object);
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            try
            {
                api.subscribe(subscription);
            }
            catch (UriFormatException ex) {
                Assert.Throws<UriFormatException>(() => restConnector.Object.Post<NotificationSubscriptionRepresentation>(It.IsAny<string>(), It.IsAny<CumulocityMediaType>(), It.IsAny<NotificationSubscriptionRepresentation>()));
            }
        }

        [Fact]
        public void testGetSubscriptionsWithoutFilter()
        {
            Assert.NotNull(api.getSubscriptionsByFilter(null));
        }

        [Fact]
        public void testGetSubscriptionsWithFilter()
        {
            NotificationSubscriptionFilter filter = new NotificationSubscriptionFilter().byContext(SubscriptionContext.TENANT.ToDescriptionString());
            Assert.Equal("tenant", filter.getContext());
            Assert.Null(filter.getSource());
            Assert.NotNull(api.getSubscriptionsByFilter(filter));
        }

        [Fact]
        public void testNotificationSubscriptionCollectionRepresentationJson()
        {
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            NotificationSubscriptionRepresentation subscription2 = new NotificationSubscriptionRepresentation();
            subscription2.Context = "sampleContext";
            subscription2.Subscription = "tenantSub";
            subscription2.FragmentsToCopy = new List<string>() { "c8y_Temparature", "c8y_humidity" };
            NotificationSubscriptionCollectionRepresentation notificationSubscriptionCollectionRepresentations = new NotificationSubscriptionCollectionRepresentation();
            notificationSubscriptionCollectionRepresentations.Subscriptions = new List<NotificationSubscriptionRepresentation>() { subscription, subscription2 };
            string jsonRepresentation = @"[{},{""Context"":""sampleContext"",""Subscription"":""tenantSub"",""FragmentsToCopy"":[""c8y_Temparature"",""c8y_humidity""]}]";
            Assert.Equal(jsonRepresentation, JsonConvert.SerializeObject(notificationSubscriptionCollectionRepresentations));
            List<NotificationSubscriptionRepresentation> deserializedRepresentation = JsonConvert.DeserializeObject<List<NotificationSubscriptionRepresentation>>(jsonRepresentation);
            Assert.Equal(notificationSubscriptionCollectionRepresentations.Subscriptions.Count, deserializedRepresentation.Count);
            Assert.Equal(notificationSubscriptionCollectionRepresentations.Subscriptions[1].Context, deserializedRepresentation[1].Context);
            Assert.Equal(notificationSubscriptionCollectionRepresentations.Subscriptions[1].Subscription, deserializedRepresentation[1].Subscription);
        }

        [Fact]
        public void testNotificationSubscriptionFilterRepresentationJson()
        {
            string filterRepresentationJson = @"{""apis"":[""inventoryApi"", ""alarmApi""], ""typeFilter"":""mo""}";
            NotificationSubscriptionFilterRepresentation notificationSubscriptionFilterRepresentation = JsonConvert.DeserializeObject<NotificationSubscriptionFilterRepresentation>(filterRepresentationJson);
            List<string> apis = new List<string>() { "inventoryApi", "alarmApi" };
            Assert.Equal(apis, notificationSubscriptionFilterRepresentation.Apis);
            Assert.Equal("mo", notificationSubscriptionFilterRepresentation.TypeFilter);
            Assert.Equal("NotificationSubscriptionFilterRepresentation(apis=System.Collections.Generic.List`1[System.String], typeFilter=mo)", notificationSubscriptionFilterRepresentation.ToString());
        }

        [Fact]
        public void testNotificationSubscriptionRepresentationJson()
        {
            string jsonRepresentation = @"{
                    ""context"": ""tenant"",
                    ""subscription"": ""sampleSubscription"",
                    ""subscriptionFilter"": {
                        ""apis"" : [""inventoryApi"", ""alarmApi""],
                        ""typeFilter"" : ""mo""
                    },
                    ""source"": {
                        ""type"": ""c8y_Thermometer"",
                        ""name"": ""loRa Device"",
                        ""owner"": ""sampleOwner""
                    }
                }";
            NotificationSubscriptionRepresentation notificationSubscriptionRepresentation = JsonConvert.DeserializeObject<NotificationSubscriptionRepresentation>(jsonRepresentation);
            Assert.Equal("tenant", notificationSubscriptionRepresentation.Context);
            Assert.Equal("mo", notificationSubscriptionRepresentation.SubscriptionFilter.TypeFilter);
            Assert.Equal("c8y_Thermometer", notificationSubscriptionRepresentation.Source.Type);
            NotificationSubscriptionRepresentation clonedObject = (NotificationSubscriptionRepresentation)notificationSubscriptionRepresentation.Clone();
            Assert.Equal(notificationSubscriptionRepresentation.Context, clonedObject.Context);
            Assert.Equal(notificationSubscriptionRepresentation.SubscriptionFilter, clonedObject.SubscriptionFilter);
        }
    }
}
