using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public class ConnectionDetails : IConnectionDetails
    {
        public string Host { get; set; }

        /// <summary>
        /// The unique clientId/deviceId to connect with
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The username to connect with
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// The password for the user
        /// </summary>

        public string Password { get; set; }

        /// <summary>
        /// Clear state at end of connection or not (durable or non-durable subscriptions),
        /// by default value being false.
        /// </summary>
        public bool CleanSession { get; set; }

    }
}
