using Cumulocity.MQTT.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.MQTT.Model
{
    public class Configuration : IConfiguration
    {
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string ConnectionType { get; set; }
        public string ClientId { get; set; }
    }
}
