using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.MqttMessage
{
    public class MqttMessageResponse : IMqttMessageResponse
    {
        public string TopicName { get; set; }

        public string ClientId { get; set; }

        public int QoS { get; set; }

        public string MessageContent { get; set; }

    }
}
