using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;

namespace Cumulocity.SDK.MQTT.Operations
{
    public class MqttOperationsProvider : IOperationsProvider, IDisposable
	{
        private IManagedMqttClient mqttClient;
        private const string TCP = "tcp://";
        private const int TCP_MQTT_PORT = 1883;

        public event EventHandler<IMqttMessageResponse> MessageReceived;

		public bool ConnectionEstablished => mqttClient.IsStarted;

        public async Task CreateConnectionAsync(IConnectionDetails connectionDetails)
        {
	        var options = new ManagedMqttClientOptionsBuilder()
		        .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
		        .WithClientOptions(new MqttClientOptionsBuilder()
			        .WithClientId(connectionDetails.ClientId)
			        .WithTcpServer(connectionDetails.Host, TCP_MQTT_PORT)
			        .Build())
		        .Build();
	        mqttClient = new MqttFactory().CreateManagedMqttClient();
	        //
	        mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
	        //
	        await mqttClient.StartAsync(options);
		}

        private void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
	        MessageReceived?.Invoke(this, new MqttMessageResponseBuilder()
		        .WithTopicName(e.ApplicationMessage.Topic)
		        .WithMessageContent(System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload))
		        .WithQoS((QoS)e.ApplicationMessage.QualityOfServiceLevel)
		        .Build());
		}

        public Task Disconnect()
        {
           return mqttClient.StopAsync();
        }

        public async Task PublishAsync(IMqttMessageRequest mqttMessageRequest)
        {
            if (!mqttClient.IsConnected) {
                await mqttClient.StartAsync(mqttClient.Options);
            }

            var msg = MqttApplicationMessageWithQoS(mqttMessageRequest.QoS)
                    .WithTopic(mqttMessageRequest.TopicName)
                    .WithPayload(mqttMessageRequest.MessageContent)
                    .WithRetainFlag()
                    .Build();

            await mqttClient.PublishAsync(msg);
        }

        public async Task SubscribeAsync(IMqttMessageRequest message)
        {
	        await mqttClient.SubscribeAsync(new TopicFilterBuilder()
		        .WithTopic(message.TopicName)
		        .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)((int)message.QoS))
		        .Build());
		}

        private string GetServerURI(ConnectionDetails connectionDetails)
        {
            return TCP + connectionDetails.Host + ":" + TCP_MQTT_PORT;
        }
        private MqttApplicationMessageBuilder MqttApplicationMessageWithQoS(QoS qoS) {
            if (qoS.Equals(QoS.EXACTLY_ONCE)) {
                return new MqttApplicationMessageBuilder()
                          .WithExactlyOnceQoS();
            }
            return new MqttApplicationMessageBuilder()
                   .WithExactlyOnceQoS();
        }
        public void Dispose()
        {
	        mqttClient.ApplicationMessageReceived -= MqttClient_ApplicationMessageReceived;
        }

	}
}
