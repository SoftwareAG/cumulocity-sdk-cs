using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.ConnectionOptions
{
    public class LastWillDetails : ILastWillDetails
    {
        /// <summary>
        /// The topic to publish to
        /// </summary>

        public string Topic { get; set; }

        /// <summary>
        /// The quality of service to publish the message at (0, 1 or 2)
        /// </summary>
        public QoS qoS { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Whether or not the message should be retained
        /// </summary>
        public bool Retained { get; set; }
    }
}
