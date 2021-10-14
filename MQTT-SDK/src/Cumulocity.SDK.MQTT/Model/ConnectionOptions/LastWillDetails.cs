using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.ConnectionOptions
{
    public class LastWillDetails : ILastWillDetails
    {
        private string _topic;
        /// <summary>
        /// The topic to publish to
        /// </summary>
        public string Topic { get => _topic; }

        internal void SetTopic(string topic) { _topic = topic; }

        private QoS _qos;
        /// <summary>
        /// The quality of service to publish the message at (0, 1 or 2)
        /// </summary>
        public QoS qoS { get => _qos; }

        internal void SetQoS(QoS qos) { _qos = qos; }

        private string _message;
        /// <summary>
        /// Message content
        /// </summary>
        public string Message { get => _message; }

        internal void SetMessage(string message) { _message = message; }

        private bool _retained;
        /// <summary>
        /// Whether or not the message should be retained
        /// </summary>
        public bool Retained { get => _retained; }

        internal void SetRetained(bool retained) { _retained = retained; }
    }
}
