using System;

namespace Cumulocity.SDK.MQTT.Exception
{
    [Serializable]
    public class MqttDeviceSDKException : System.Exception
    {

        public MqttDeviceSDKException(string message) : base(message)
        {
        }

        public MqttDeviceSDKException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }

}
