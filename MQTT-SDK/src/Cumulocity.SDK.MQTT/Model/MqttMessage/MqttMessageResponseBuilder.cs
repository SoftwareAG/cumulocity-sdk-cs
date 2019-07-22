using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.MqttMessage
{
    public class MqttMessageResponseBuilder
    {
        private readonly MqttMessageResponse _mqttMessageResponse = new MqttMessageResponse();

        public MqttMessageResponseBuilder WithTopicName(string topicName)
        {
            _mqttMessageResponse.TopicName = topicName;
            return this;
        }

        public MqttMessageResponseBuilder WithQoS(QoS qos)
        {
            _mqttMessageResponse.QoS = (int)qos;
            return this;
        }

        public MqttMessageResponseBuilder WithMessageContent(string messageContent)
        {
            _mqttMessageResponse.MessageContent = messageContent;
            return this;
        }

        public MqttMessageResponseBuilder WithClientId(string clientId)
        {
            _mqttMessageResponse.ClientId = clientId;
            return this;
        }

        public IMqttMessageResponse Build()
        {
            return _mqttMessageResponse;
        }
    }
}
