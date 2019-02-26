using System;
using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using System.Threading.Tasks;
using Cumulocity.SDK.MQTT.Exception;

namespace Cumulocity.SDK.MQTT
{
    public interface IMqttClient
    {
        /// <summary>
        /// Connects the client to the broker
        /// </summary>
        /// <exception cref="MqttDeviceSDKException"> </exception>
        Task EstablishConnectionAsync();

        /// <summary>
        /// Publishes a given message/payload with qos level 0 / 1 / 2
        /// to a particular topic
        /// </summary>
        /// <param name="message">
        /// </param>
        /// <exception cref="MqttDeviceSDKException"> </exception>
        Task PublishAsync(IMqttMessageRequest message);

        /// <summary>
        /// Subscribe to a particular topic
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="messageListener">
        /// </param>
        /// <exception cref="MqttDeviceSDKException"> </exception>
        Task SubscribeAsync(IMqttMessageRequest message);

        /// <summary>
        /// Disconnects the client from the broker
        /// </summary>
        /// <exception cref="MqttDeviceSDKException"> </exception>
        Task Disconnect();

        /// <summary>
        /// Message received from the broker
        /// </summary>
        event EventHandler<IMqttMessageResponse> MessageReceived;
	}
}
