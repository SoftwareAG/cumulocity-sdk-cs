using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.MqttMessage
{
    public interface IMqttMessageRequest
    {
         string TopicName { get; }

         QoS QoS { get; }

         string MessageContent { get; }
    }
}
