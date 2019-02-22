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
        private MqttOperationsProvider operationsProvider;

        public MqttClientExt(IConnectionDetails connectionDetails)
        {
            ConnectionDetails = connectionDetails;
        }

        public IConnectionDetails ConnectionDetails { get; }

        public Task Disconnect()
        {
            return operationsProvider.Disconnect();
        }

        public async Task EstablishConnectionAsync()
        {
            try
            {
                operationsProvider = new MqttOperationsProvider();
                await operationsProvider.CreateConnectionAsync(ConnectionDetails);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException(($"Unable to construct client for clientId {ConnectionDetails.ClientId} and connect to server {ConnectionDetails.Host} : {ex.Message}" ));
            }
        }

        public async Task PublishAsync(IMqttMessageRequest message)
        {
            if (!operationsProvider.ConnectionEstablished)
            {
                throw new MqttDeviceSDKException("Publish can happen only when client is initialized and connection to " + "server established.");
            }
            
            if (!MqttTopicValidator.IsTopicValidForPublish(message.TopicName))
            {
                throw new MqttDeviceSDKException("Invalid topic to publish.");
            }

            try
            {
                await operationsProvider.PublishAsync(message);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException($"Unable to publish message for clientId {ConnectionDetails.ClientId} on topic {message.TopicName} : ", ex);
            }

        }

        public void PublishAsync(MqttMessageRequest message)
        {
            throw new NotImplementedException();
        }

        public async Task SubscribeAsync(IMqttMessageRequest message)
        {
            if (!operationsProvider.ConnectionEstablished)
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
                await operationsProvider.SubscribeAsync(message);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException($"Unable to subscribe message for clientId {ConnectionDetails.ClientId} on topic {message.TopicName} : ", ex);
            }
        }

    }
}
