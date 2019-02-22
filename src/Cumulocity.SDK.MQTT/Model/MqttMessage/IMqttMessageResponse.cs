using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.MqttMessage
{
    public interface IMqttMessageResponse
    {
        string TopicName { get; }
        string ClientId { get; }
        int QoS { get; }
        string MessageContent { get; }
    }

}
