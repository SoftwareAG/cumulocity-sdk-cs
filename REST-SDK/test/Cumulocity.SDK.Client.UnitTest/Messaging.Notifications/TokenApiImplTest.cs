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
    public class ArgumentCaptor<T>
    {
        public T Capture()
        {
            return It.Is<T>(t => SaveValue(t));
        }

        private bool SaveValue(T t)
        {
            Value = t;
            return true;
        }

        public T Value { get; private set; }
    }
    public class TokenApiImplTest
    {
        private static readonly string HOST = "core-0.platform.default.svc.cluster.local/";
        private static readonly string JWT_TOKEN = "f4k3_jwt_t0k3n";

        private Mock<RestConnector> restConnector;
        private Mock<PlatformParameters> platformParameters;
        private Mock<ResponseParser> parser;

        private ITokenApi tokenApi;
        private Mock<TokenApiImpl> tokenApiMock;

        public TokenApiImplTest()
        {
            platformParameters = new Mock<PlatformParameters>();
            platformParameters.Setup(x => x.Host).Returns(HOST);
            parser = new Mock<ResponseParser>();
            restConnector = new Mock<RestConnector>(platformParameters.Object, parser.Object);
            // No concept of spy in C# moq. Hence use concrete impl. 
            tokenApi = new TokenApiImpl(platformParameters.Object, restConnector.Object);
            // For argumentCaptor verify
            tokenApiMock = new Mock<TokenApiImpl>(platformParameters.Object, restConnector.Object);
        }

        private string getUri(string endpoint)
        {
            return HOST + endpoint;
        }
        [Fact]
        public void shouldCreateToken()
        {
            NotificationTokenRequestRepresentation tokenRequest = new NotificationTokenRequestRepresentation("sub", "sup", 1, false);
            Token token = new Token(JWT_TOKEN);
            restConnector.Setup(x => x.Post<Token, NotificationTokenRequestRepresentation>(getUri(TokenApiImpl.TOKEN_REQUEST_URI), TokenApiImpl.TOKEN_MEDIA_TYPE, TokenApiImpl.TOKEN_MEDIA_TYPE, It.IsAny<NotificationTokenRequestRepresentation>(), It.IsAny<Token>())).Returns(token);
            Assert.Equal(JWT_TOKEN, tokenApi.create(tokenRequest).TokenString);
        }

        [Fact]
        public void shouldBuildCreateTokenUri()
        {
            NotificationTokenRequestRepresentation tokenRequest = new NotificationTokenRequestRepresentation("sub", "sup", 1, false);
            string uri = getUri(TokenApiImpl.TOKEN_REQUEST_URI);
            restConnector.Setup(x => x.Post<Token, NotificationTokenRequestRepresentation>(It.IsAny<string>(), It.IsAny<CumulocityMediaType>(), It.IsAny<CumulocityMediaType>(), It.IsAny<NotificationTokenRequestRepresentation>(), It.IsAny<Token>())).Returns(new Token());
            tokenApi.create(tokenRequest);
            restConnector.Verify(x => x.Post<Token, NotificationTokenRequestRepresentation>(uri, TokenApiImpl.TOKEN_MEDIA_TYPE, TokenApiImpl.TOKEN_MEDIA_TYPE, It.IsAny<NotificationTokenRequestRepresentation>(), It.IsAny<Token>()));
        }

        [Fact]
        public void shouldVerifyToken()
        {
            TokenClaims tokenRequest = new TokenClaims("sub", "topic", "jti", 1L, 1L);
            string uri = getUri($"{TokenApiImpl.TOKEN_REQUEST_URI}?token={JWT_TOKEN}");
            Token tokenToVerify = new Token(JWT_TOKEN);
            restConnector.Setup(x => x.Get<TokenClaims>(uri, TokenApiImpl.TOKEN_MEDIA_TYPE, typeof(TokenClaims))).Returns(tokenRequest);
            TokenClaims verificationResult = tokenApi.verify(tokenToVerify);
            Assert.Equal(tokenRequest, verificationResult);
        }

        [Fact]
        public void shouldBuildVerifyUri()
        {
            string uri = getUri($"{TokenApiImpl.TOKEN_REQUEST_URI}?token={JWT_TOKEN}");
            Token tokenToVerify = new Token(JWT_TOKEN);
            tokenApi.verify(tokenToVerify);
            restConnector.Verify(x => x.Get<TokenClaims>(uri, TokenApiImpl.TOKEN_MEDIA_TYPE, typeof(TokenClaims)));
        }

        [Fact]
        public void shouldRefreshToken()
        {
            string expiredJwtToken = "eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJ0ZXN0c3Vic2NyaWJlciIsInRvcGljIjoibWFuYWdlbWVudC9yZW" +
                "xub3RpZi90ZXN0c3Vic2NyaXB0aW9uIiwianRpIjoiMjYwNjY1ZmQtNDI1ZC00NjVlLWJlZTYtZTgzYzI1ZmMxMzYxIiwiaWF0Ij" +
                "oxNjI1NzY5NzUyLCJleHAiOjE2MjU3Njk4MTJ9.KeFUl0b3EMxnlDsin3i8Y_dxidQJmLsbzNSK2JissnYMBSG9EA-YTDNVRwGqW" +
                "LjR8OMEoSiYLPgMPBvWTKKYJliIyStdQ8XhaINHZiwV4Jd-_Y7ITHuc5-XRPN8p2ik1omFmpAS5FwxNsVMj-Rx_dMUK4gp5sKbYr" +
                "R14R1hzFestBZdMnWIT-T5ORywZHd7MtOE7nsSrCHwp6MKmcGvIM7Bhz2e1QC0DU60prpnt_DUoL6M8dpNBPtl40XssGnCIGNruk" +
                "ukm7QMwhgL8U82AQQ_qefpXFJOLMzyDCYD59fMHTQ8Bdi9svH8f6rswu8yQ326QH0sf_Mrhr5dwCI1EnA";

            ArgumentCaptor<NotificationTokenRequestRepresentation> argumentCaptor = new ArgumentCaptor<NotificationTokenRequestRepresentation>();
            tokenApiMock.Object.refresh(new Token(expiredJwtToken));
            tokenApiMock.Verify(x => x.create(argumentCaptor.Capture()));
            Assert.Equal("testsubscriber", argumentCaptor.Value.Subscriber);
            Assert.Equal("testsubscription", argumentCaptor.Value.Subscription);
            Assert.Equal(1, argumentCaptor.Value.ExpiresInMinutes);
        }
    }
}
