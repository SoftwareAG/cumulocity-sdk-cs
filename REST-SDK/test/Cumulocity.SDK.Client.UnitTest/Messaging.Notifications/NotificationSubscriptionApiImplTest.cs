using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Messaging.Notifications;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using Moq;
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
        private static readonly string DEFAULT_HOST = "host/";
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
            restConnector.Setup(x => x.Post<NotificationSubscriptionRepresentation>(It.IsAny<string>(), It.IsAny<CumulocityMediaType>(), It.IsAny<NotificationSubscriptionRepresentation>())).Verifiable();
            urlProcessor = new Mock<UrlProcessor>();
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
        public void testDelete()
        {
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            subscription.Id = new Rest.Model.Idtype.GId(DEFAULT_GID_VALUE);
            api.delete(subscription);
            restConnector.Verify(x => x.Delete($"{DEFAULT_HOST}{SUBSCRIPTION_REQUEST_URI}/{DEFAULT_GID_VALUE}"));
        }

        [Fact]
        public void testSubscribe()
        {
            NotificationSubscriptionRepresentation subscription = new NotificationSubscriptionRepresentation();
            api.subscribe(subscription);
            restConnector.Verify(x => x.Post<NotificationSubscriptionRepresentation>(It.IsAny<string>(), It.IsAny<CumulocityMediaType>(), It.IsAny<NotificationSubscriptionRepresentation>()));
        }
    }
}
