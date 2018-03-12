using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IConfiguration
    {
        string Server { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Port { get; set; }
        string ConnectionType { get; set; }
        string ClientId { get; set; }

    }
}
