using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.MqttMessage
{
    public class MqttMessageRequestBuilder
    {
        MqttMessageRequest _mqttMessageRequest = new MqttMessageRequest();

        public MqttMessageRequestBuilder WithTopicName(string topicName)
        {
            _mqttMessageRequest.TopicName = topicName;
            return this;
        }

        public MqttMessageRequestBuilder WithMessageContent(string messageContent)
        {
            _mqttMessageRequest.MessageContent = messageContent;
            return this;
        }

        public MqttMessageRequestBuilder WithQoS(QoS qos)
        {
            _mqttMessageRequest.QoS = qos;
            return this;
        }

        public IMqttMessageRequest Build()
        {
            return _mqttMessageRequest;
        }
    }
}
