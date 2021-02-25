using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cumulocity.SDK.MQTT.Util;

namespace Cumulocity.SDK.MQTT.Tests
{
    public class MQTTTopicValidatorTest
    {
        [Fact]
        public void IsTopicValidForPublishTest()
        {
            Assert.False(MqttTopicValidator.IsTopicValidForPublish(""));
            Assert.False(MqttTopicValidator.IsTopicValidForPublish(null));
            Assert.False(MqttTopicValidator.IsTopicValidForPublish("UnknownTopic"));
            Assert.False(MqttTopicValidator.IsTopicValidForPublish("s/dt"));
            Assert.False(MqttTopicValidator.IsTopicValidForPublish("s/dcr"));
            Assert.False(MqttTopicValidator.IsTopicValidForPublish("s/ds"));
            Assert.True(MqttTopicValidator.IsTopicValidForPublish("s/us"));
            Assert.True(MqttTopicValidator.IsTopicValidForPublish("s/ul"));
            Assert.True(MqttTopicValidator.IsTopicValidForPublish("s/ucr"));
        }


        [Fact]
        public void IsTopicValidForSubscribeTest()
        {
            Assert.False(MqttTopicValidator.IsTopicValidForSubscribe(""));
            Assert.False(MqttTopicValidator.IsTopicValidForSubscribe(null));
            Assert.False(MqttTopicValidator.IsTopicValidForSubscribe("UnknownTopic"));
            Assert.False(MqttTopicValidator.IsTopicValidForSubscribe("s/ul"));
            Assert.False(MqttTopicValidator.IsTopicValidForSubscribe("s/ucr"));
            Assert.False(MqttTopicValidator.IsTopicValidForSubscribe("s/us"));
            Assert.True(MqttTopicValidator.IsTopicValidForSubscribe("s/dt"));
            Assert.True(MqttTopicValidator.IsTopicValidForSubscribe("s/dcr"));
            Assert.True(MqttTopicValidator.IsTopicValidForSubscribe("s/ds"));
        }

    }
}
