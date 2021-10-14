using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.MQTT.Model.ConnectionOptions;

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

		/// <summary>
		/// Gets or sets a value indicating whether [use TLS].
		/// </summary>
		/// <value>
		///   <c>true</c> if [use TLS]; otherwise, <c>false</c>.
		/// </value>
		public bool UseTls { get; set; } = false;

		/// <summary>
		/// Gets or sets the protocol.
		/// </summary>
		/// <value>
		/// The protocol.
		/// </value>
		public TransportType Protocol { get; set; }

        /// <summary>
        /// The "last will" message that is specified at connection time and that
        /// is executed when the client loses the connection.
        /// </summary>
        public LastWillDetails LastWill { get; set; }

	}
}
