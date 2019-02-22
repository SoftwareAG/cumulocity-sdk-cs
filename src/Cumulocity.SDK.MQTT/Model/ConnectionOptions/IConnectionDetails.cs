using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{

    public interface IConnectionDetails
    {

        string Host { get;}

        /// <summary>
        /// The unique clientId/deviceId to connect with
        /// </summary>
        string ClientId { get;  }

        /// <summary>
        /// The username to connect with
        /// </summary>
        string UserName { get;  }

        /// <summary>
        /// The password for the user
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Clear state at end of connection or not (durable or non-durable subscriptions),
        /// by default value being false.
        /// </summary>
        bool CleanSession { get;  }
    }

}
