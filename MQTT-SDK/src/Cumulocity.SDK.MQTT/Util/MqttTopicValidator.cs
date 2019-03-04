using Cumulocity.SDK.MQTT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Util
{
    public class MqttTopicValidator
    {

        public static bool IsTopicValidForPublish(string topic)
        {
            if (string.ReferenceEquals(topic, null) || topic.Length == 0)
            {
                return false;
            }

            return IsSmartRestStaticPublishTopic(topic) || IsSmartRestLegacyPublishTopic(topic) || IsSmartRestPublishTopic(topic) || IsSmartRestTemplateCreationPublishTopic(topic);
        }


        public static bool IsTopicValidForSubscribe(string topic)
        {
            if (string.ReferenceEquals(topic, null) || topic.Length == 0)
            {
                return false;
            }

            return IsSmartRestExceptionsTopic(topic) || IsSmartRestStaticSubscribeTopic(topic) || IsSmartRestLegacySubscribeTopic(topic) || IsSmartRestSubscribeTopic(topic) || IsSmartRestTemplateCreationSubscribeTopic(topic) || IsSmartRestLegacyOperationsTopic(topic);
        }


        private static bool IsSmartRestExceptionsTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_SMARTREST_EXCEPTION_SUBSCRIBE);
        }

        private static bool IsSmartRestStaticPublishTopic(string topic)
        {
            return topic.Equals(MqttTopics.BASE_SMARTREST_STATIC_PUBLISH_PERSISTENT) || topic.StartsWith(MqttTopics.BASE_SMARTREST_STATIC_PUBLISH_PERSISTENT + MqttTopics.TOPIC_SEPARATOR, StringComparison.Ordinal);
        }

        private static bool IsSmartRestStaticSubscribeTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_SMARTREST_STATIC_SUBSCRIBE);
        }

        private static bool IsSmartRestLegacyPublishTopic(string topic)
        {
            return topic.Equals(MqttTopics.BASE_SMARTREST_LEGACY_PUBLISH_PERSISTENT) || topic.StartsWith(MqttTopics.BASE_SMARTREST_LEGACY_PUBLISH_PERSISTENT + MqttTopics.TOPIC_SEPARATOR, StringComparison.Ordinal);
        }

        private static bool IsSmartRestLegacySubscribeTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_SMARTREST_LEGACY_SUBSCRIBE);
        }

        private static bool IsSmartRestLegacyOperationsTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_SMARTREST_LEGACY_OPERATIONS_PERSISTENT);
        }

        private static bool IsSmartRestPublishTopic(string topic)
        {
            return topic.StartsWith(MqttTopics.BASE_SMARTREST_CUSTOM_TEMPLATE_PUBLISH_PERSISTENT + MqttTopics.TOPIC_SEPARATOR, StringComparison.Ordinal);
        }

        private static bool IsSmartRestSubscribeTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_SMARTREST_CUSTOM_TEMPLATE_SUBSCRIBE);
        }

        private static bool IsSmartRestTemplateCreationPublishTopic(string topic)
        {
            return topic.StartsWith(MqttTopics.BASE_SMARTREST_TEMPLATE_CREATION_PUBLISH_PERSISTENT + MqttTopics.TOPIC_SEPARATOR, StringComparison.Ordinal);
        }

        private static bool IsSmartRestTemplateCreationSubscribeTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_SMARTREST_TEMPLATE_CREATION_SUBSCRIBE);
        }
    }
}
