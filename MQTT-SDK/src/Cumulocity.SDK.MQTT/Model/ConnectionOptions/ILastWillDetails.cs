using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public interface ILastWillDetails
    {
        /// <summary>
        /// The topic to publish to
        /// </summary>

        string Topic { get; }

        /// <summary>
        /// The quality of service to publish the message at (0, 1 or 2)
        /// </summary>
        QoS qoS { get; }

        /// <summary>
        /// Message content
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Whether or not the message should be retained
        /// </summary>
        bool Retained { get; }
    }
}
