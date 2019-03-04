using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.MqttMessage
{
    public class MqttMessageRequest: IMqttMessageRequest
    {
        public string TopicName { get; set; }

        public QoS QoS { get; set; }

        public string MessageContent { get; set; }
    }
}
