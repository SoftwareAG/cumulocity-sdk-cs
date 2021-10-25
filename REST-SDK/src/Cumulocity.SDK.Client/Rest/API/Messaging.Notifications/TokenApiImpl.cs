using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class TokenApiImpl : ITokenApi
    {
        public static readonly CumulocityMediaType TOKEN_MEDIA_TYPE = new CumulocityMediaType("application", "json");

        public static readonly string TOKEN_REQUEST_URI = "notification2/token";
        private static readonly string JWT_TOKEN_SPLIT = ".";
        private static readonly string TOPIC_SPLIT = "/";

        private readonly PlatformParameters platformParameters;
        private readonly RestConnector restConnector;

        public TokenApiImpl(PlatformParameters platformParameters, RestConnector restConnector)
        {
            this.platformParameters = platformParameters;
            this.restConnector = restConnector;
        }

        private string getTokenRequestUri()
        {
            return $"{platformParameters.Host}{TOKEN_REQUEST_URI}";
        }

        public virtual Token create(NotificationTokenRequestRepresentation tokenRequest)
        {
            if(tokenRequest == null)
            {
                throw new ArgumentNullException("Token claim is null");
            }
            return restConnector.Post<Token, NotificationTokenRequestRepresentation>(getTokenRequestUri(), TOKEN_MEDIA_TYPE, TOKEN_MEDIA_TYPE, tokenRequest, (Token)Activator.CreateInstance(typeof(Token)));
        }

        public Token refresh(Token expiredToken)
        {
            if(expiredToken == null || string.IsNullOrEmpty(expiredToken.TokenString))
            {
                throw new ArgumentException("Expired token is null");
            }
            string claimsString = null;
            try
            {
                string[] tokenParts = expiredToken.TokenString.Split(new string[] { JWT_TOKEN_SPLIT }, StringSplitOptions.None);
                byte[] decodedArray = Convert.FromBase64String(tokenParts[1]);
                claimsString = Encoding.UTF8.GetString(decodedArray);
            }
            catch(ArgumentException argumentException)
            {
                throw new ArgumentException("Not a valid token");
            }
            TokenClaims parsedToken = JsonConvert.DeserializeObject<TokenClaims>(claimsString);
            string subscription = null;
            try
            {
                subscription = parsedToken.Topic.Split(new string[] { TOPIC_SPLIT }, StringSplitOptions.None)[2];
            } 
            catch (IndexOutOfRangeException ie)
            {
                throw new ArgumentException("Not a Valid Topic");
            }
            long expiry = parsedToken.Exp - parsedToken.Iat;
            long validityPeriodMinutes = expiry / 60;
            return create(new NotificationTokenRequestRepresentation(parsedToken.Subscriber, subscription, validityPeriodMinutes, false));
        }

        public TokenClaims verify(Token token)
        {
            return restConnector.Get<TokenClaims>($"{getTokenRequestUri()}?token={token.TokenString}", TOKEN_MEDIA_TYPE, typeof(TokenClaims));
        }
    }
}
