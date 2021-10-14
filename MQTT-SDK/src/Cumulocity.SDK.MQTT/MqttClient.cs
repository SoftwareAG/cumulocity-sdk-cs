using System;
using System.Threading.Tasks;
using Cumulocity.SDK.MQTT.Exception;
using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using Cumulocity.SDK.MQTT.Operations;
using Cumulocity.SDK.MQTT.Util;

namespace Cumulocity.SDK.MQTT
{
    public class MqttClient : IMqttClient
    {


        public MqttClient(IConnectionDetails connectionDetails, IOperationsProvider operationsProvider = null)
        {
	        OperationsProvider = operationsProvider ?? new MqttOperationsProvider();
	        ConnectionDetails = connectionDetails;
			OperationsProvider.MessageReceived += OperationsProvider_MessageReceived;
            OperationsProvider.ConnectionFailed += OperationsProvider_ConnectionFailed;
            OperationsProvider.Connected += OperationsProvider_Connected;
        }

        private void OperationsProvider_Connected(object sender, ClientConnectedEventArgs e)
        {
            Connected?.Invoke(this,e);
        }

        private void OperationsProvider_ConnectionFailed(object sender, ProcessFailedEventArgs e)
        {
            ConnectionFailed?.Invoke(this, e );
        }

        private void OperationsProvider_MessageReceived(object sender, IMqttMessageResponse e)
		{
			MessageReceived?.Invoke(this, new MqttMessageResponseBuilder()
				.WithTopicName(e.TopicName)
				.WithMessageContent(e.MessageContent)
				.WithQoS((QoS)e.QoS)
				.Build());
		}

		public IConnectionDetails ConnectionDetails { get; }
		public IOperationsProvider OperationsProvider { get; }

		public event EventHandler<IMqttMessageResponse> MessageReceived;
        public event EventHandler<ProcessFailedEventArgs> ConnectionFailed;
        public event EventHandler<ClientConnectedEventArgs> Connected;

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
                throw new MqttDeviceSDKException($"Invalid topic to publish. Topic name: {message.TopicName}, Client Id: {ConnectionDetails.ClientId}");
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
                throw new MqttDeviceSDKException($"Invalid topic to subscribe. Topic name: {message.TopicName}, Client Id: {ConnectionDetails.ClientId}");
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

        public async Task UnsubscribeAsync(string topic)
        {
            if (!OperationsProvider.ConnectionEstablished)
            {
                throw new MqttDeviceSDKException("Unsubscribe can happen only when client is initialized, " +
                    $"and connection to server established. Topic name: {topic}, Client Id: {ConnectionDetails.ClientId}");
            }

            if (!MqttTopicValidator.IsTopicValidForSubscribe(topic))
            {
                throw new MqttDeviceSDKException($"The topic cannot be subscribed to. Topic name: {topic}, Client Id: {ConnectionDetails.ClientId}");
            }
            try
            {
                await OperationsProvider.UnsubscribeAsync(topic);
            }
            catch (System.Exception ex)
            {
                throw new MqttDeviceSDKException($"Unable to subscribe from topic {topic} for clientId {ConnectionDetails.ClientId} : ", ex);
            }
        }
    }
}
