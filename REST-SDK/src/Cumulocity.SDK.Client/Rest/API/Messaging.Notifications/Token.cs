using Cumulocity.SDK.Client.Rest.Representation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class Token : BaseResourceRepresentation
    {
        public Token() { }

        private string tokenString;

        public Token(string tokenString)
        {
            this.tokenString = tokenString;
        }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string TokenString
        {
            get => tokenString;
            set => tokenString = value;
        }
    }
}
