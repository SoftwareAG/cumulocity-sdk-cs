using Cumulocity.SDK.Client.Rest.Representation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class TokenClaims : BaseResourceRepresentation
    {
        private string subscriber;

        private string topic;

        // Token annotations: jti: id, iat: issued at, exp: expiry
        private string jti;

        private long iat;

        private long exp;

        public TokenClaims() { }

        public TokenClaims(string subscriber, string topic, string jti, long iat, long exp)
        {
            this.subscriber = subscriber;
            this.topic = topic;
            this.jti = jti;
            this.iat = iat;
            this.exp = exp;
        }

        [JsonProperty("sub", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Subscriber
        {
            get => subscriber;
            set => subscriber = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Topic
        {
            get => topic;
            set => topic = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Jti
        {
            get => jti;
            set => jti = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual long Iat
        {
            get => iat;
            set => iat = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual long Exp
        {
            get => exp;
            set => exp = value;
        }
    }
}
