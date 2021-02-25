using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public class MqttTopics
    {
        public const string TOPIC_SEPARATOR = "/";

        // smartrest exceptions
        public const string BASE_SMARTREST_EXCEPTION_SUBSCRIBE = "s/e";

        // smartrest downlink static
        public const string BASE_SMARTREST_STATIC_SUBSCRIBE = "s/ds";

        // persistent smartrest uplink static
        public const string BASE_SMARTREST_STATIC_PUBLISH_PERSISTENT = "s/us";

        // persistent smartrest uplink legacy
        public const string BASE_SMARTREST_LEGACY_PUBLISH_PERSISTENT = "s/ul";

        // smartrest downlink legacy
        public const string BASE_SMARTREST_LEGACY_SUBSCRIBE = "s/dl";

        // persistent smartrest operations legacy
        public const string BASE_SMARTREST_LEGACY_OPERATIONS_PERSISTENT = "s/ol";

        // persistent smartrest uplink custom template
        public const string BASE_SMARTREST_CUSTOM_TEMPLATE_PUBLISH_PERSISTENT = "s/uc";

        // smartrest downlink custom template
        public const string BASE_SMARTREST_CUSTOM_TEMPLATE_SUBSCRIBE = "s/dc";

        // persistent smartrest uplink template creation
        public const string BASE_SMARTREST_TEMPLATE_CREATION_PUBLISH_PERSISTENT = "s/ut";

        // smartrest downlink template creation
        public const string BASE_SMARTREST_TEMPLATE_CREATION_SUBSCRIBE = "s/dt";

        // uplink device credentials
        public const string BASE_DEVICE_CREDENTIALS_PUBLISH = "s/ucr";

        // downlink device credentials
        public const string BASE_DEVICE_CREDENTIALS_SUBSCRIBE = "s/dcr";

        // JSON via MQTT
        public static readonly List<string> BASE_JSON_via_MQTT_TEMPLATE_ENDPOINTS = new List<string>() { "event/events", "alarm/alarms", "measurement/measurements", "inventory/managedObjects", "error" };
    }
}
