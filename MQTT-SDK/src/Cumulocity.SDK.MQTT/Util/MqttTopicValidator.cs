using Cumulocity.SDK.MQTT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return IsSmartRestStaticPublishTopic(topic) || IsSmartRestLegacyPublishTopic(topic) || IsSmartRestPublishTopic(topic) || IsSmartRestTemplateCreationPublishTopic(topic) || IsJsonViaMqttTopic(topic) || IsDeviceCredentialsPublishTopic(topic);
        }


        public static bool IsTopicValidForSubscribe(string topic)
        {
            if (string.ReferenceEquals(topic, null) || topic.Length == 0)
            {
                return false;
            }

            return IsSmartRestExceptionsTopic(topic) || IsSmartRestStaticSubscribeTopic(topic) || IsSmartRestLegacySubscribeTopic(topic) || IsSmartRestSubscribeTopic(topic) || IsSmartRestTemplateCreationSubscribeTopic(topic) || IsSmartRestLegacyOperationsTopic(topic) || IsJsonViaMqttTopic(topic) || IsDeviceCredentialsSubscribeTopic(topic);
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

        private static bool IsDeviceCredentialsPublishTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_DEVICE_CREDENTIALS_PUBLISH);
        }

        private static bool IsDeviceCredentialsSubscribeTopic(string topic)
        {
            return topic.Contains(MqttTopics.BASE_DEVICE_CREDENTIALS_SUBSCRIBE);
        }

        private static bool IsJsonViaMqttTopic(string topic)
        {
            return MqttTopics.BASE_JSON_via_MQTT_TEMPLATE_ENDPOINTS.Any(l => topic.Contains(l));
        }
    }
}
