using System;
using System.Threading.Tasks;
using Cumulocity.SDK.MQTT.Exception;
using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using Cumulocity.SDK.MQTT.Operations;
using Cumulocity.SDK.MQTT.Util;

namespace Cumulocity.SDK.MQTT
{
    public class MqttClientExt : IMqttClient
    {


        public MqttClientExt(IConnectionDetails connectionDetails, IOperationsProvider operationsProvider = null)
        {
	        OperationsProvider = operationsProvider ?? new MqttOperationsProvider();
	        ConnectionDetails = connectionDetails;
        }

		public IConnectionDetails ConnectionDetails { get; }
		public IOperationsProvider OperationsProvider { get; }

		public event EventHandler<IMqttMessageResponse> MessageReceived;

		public Task Disconnect()
        {
            return OperationsProvider.Disconnect();
        }

        public async Task EstablishConnectionAsync()
        {
            try
            {
                await OperationsProvider.CreateConnectionAsync(ConnectionDetails);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException(($"Unable to construct client for clientId {ConnectionDetails.ClientId} and connect to server {ConnectionDetails.Host} : {ex.Message}" ));
            }
        }

        public async Task PublishAsync(IMqttMessageRequest message)
        {
            if (!OperationsProvider.ConnectionEstablished)
            {
                throw new MqttDeviceSDKException("Publish can happen only when client is initialized and connection to " + "server established.");
            }
            
            if (!MqttTopicValidator.IsTopicValidForPublish(message.TopicName))
            {
                throw new MqttDeviceSDKException("Invalid topic to publish.");
            }

            try
            {
                await OperationsProvider.PublishAsync(message);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException($"Unable to publish message for clientId {ConnectionDetails.ClientId} on topic {message.TopicName} : ", ex);
            }

        }

        public async Task SubscribeAsync(IMqttMessageRequest message)
        {
            if (!OperationsProvider.ConnectionEstablished)
            {
                throw new MqttDeviceSDKException("Subscribe can happen only when client is initialized and " +
                        "connection to server established.");
            }

            if (!MqttTopicValidator.IsTopicValidForSubscribe(message.TopicName))
            {
                throw new MqttDeviceSDKException("Invalid topic to subscribe.");
            }

            try
            {
                await OperationsProvider.SubscribeAsync(message);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException($"Unable to subscribe message for clientId {ConnectionDetails.ClientId} on topic {message.TopicName} : ", ex);
            }
        }

    }
}
