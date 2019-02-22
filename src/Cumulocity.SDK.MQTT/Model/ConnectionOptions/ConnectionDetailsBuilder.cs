using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public class ConnectionDetailsBuilder
    {
        private readonly ConnectionDetails _options = new ConnectionDetails();

        public ConnectionDetailsBuilder WithHost(string host)
        {
            _options.Host = host;
            return this;
        }
        public ConnectionDetailsBuilder WithClientId(string clientId)
        {
            _options.ClientId = clientId;
            return this;
        }

        public ConnectionDetailsBuilder WithCleanSession(bool value = true)
        {
            _options.CleanSession = value;
            return this;
        }

        public ConnectionDetailsBuilder WithCredentials(string username, string password = null)
        {
            _options.UserName = username;
            _options.Password = password;

            return this;
        }

        public IConnectionDetails Build()
        {
            return _options;
        }
    }
}
