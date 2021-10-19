using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.ConnectionOptions;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using System;
using System.Threading.Tasks;
using Cumulocity.SDK.MQTT.Exception;
using System.Text;

namespace Cumulocity.SDK.MQTT.Operations
{
	public class MqttOperationsProvider : IOperationsProvider, IDisposable
	{
		private IManagedMqttClient mqttClient;
		private const string TCP = "tcp://";
		private const int TCP_MQTT_PORT = 1883;

		public event EventHandler<IMqttMessageResponse> MessageReceived;
        public event EventHandler<ProcessFailedEventArgs> ConnectionFailed;
        public event EventHandler<ClientConnectedEventArgs> Connected;

        public bool ConnectionEstablished => mqttClient.IsStarted;

		public async Task CreateConnectionAsync(IConnectionDetails connectionDetails)
		{
			var clientOptions = new MqttClientOptionsBuilder()
				.WithClientId(connectionDetails.ClientId)
				.WithCredentials(connectionDetails.UserName, connectionDetails.Password);

			if (connectionDetails.Protocol.Equals(TransportType.Tcp))
				clientOptions.WithTcpServer(connectionDetails.Host, TCP_MQTT_PORT);
			else
				clientOptions.WithWebSocketServer(connectionDetails.Host);

			if (connectionDetails.UseTls)
				clientOptions.WithTls();

			var managedMqttClient = new ManagedMqttClientOptionsBuilder()
				.WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
				.WithClientOptions(clientOptions.Build())
				.Build();

			if(connectionDetails.LastWill != null)
            {
				LastWillDetails lastWillDetails = connectionDetails.LastWill;
				// Mimics Java getBytes(). 
				byte[] payload = Encoding.Default.GetBytes(lastWillDetails.Message);
				var mqttApplicationMessage = new MqttApplicationMessageBuilder().WithTopic(lastWillDetails.Topic)
					.WithPayload(payload).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)Convert.ToInt32(lastWillDetails.qoS))
					.WithRetainFlag(lastWillDetails.Retained).Build();
				clientOptions.WithWillMessage(mqttApplicationMessage);
            }

			mqttClient = new MqttFactory().CreateManagedMqttClient();
			//
			mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
            mqttClient.Connected += MqttClient_Connected;
            mqttClient.ConnectingFailed += MqttClient_ConnectingFailed;
            //
           await mqttClient.StartAsync(managedMqttClient);

            

        }

        private void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            Connected?.Invoke(this,new ClientConnectedEventArgs(e.IsSessionPresent));
        }

        private void MqttClient_ConnectingFailed(object sender, MqttManagedProcessFailedEventArgs e)
        {
            ConnectionFailed?.Invoke(this, new ProcessFailedEventArgs(e.Exception));
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
			if (!mqttClient.IsStarted)
			{
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

		public async Task UnsubscribeAsync(string topic)
        {
			await mqttClient.UnsubscribeAsync(topic);
        }

		private string GetServerURI(ConnectionDetails connectionDetails)
		{
			return TCP + connectionDetails.Host + ":" + TCP_MQTT_PORT;
		}

		private MqttApplicationMessageBuilder MqttApplicationMessageWithQoS(QoS qoS)
		{
			if (qoS.Equals(QoS.EXACTLY_ONCE))
			{
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