using System;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace HelloMqttNet
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			// Setup and start a managed MQTT client.
			var options = new ManagedMqttClientOptionsBuilder()
				.WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
				.WithClientOptions(new MqttClientOptionsBuilder()
					.WithClientId("Client1")
					.WithTcpServer("piotr.staging.c8y.io")
					.WithCredentials("piotr/admin", "test1234")
					.WithCleanSession(true)
					.Build())
				.Build();

			var mqttClient = new MqttFactory().CreateManagedMqttClient();
			await mqttClient.StartAsync(options);
			mqttClient.ApplicationMessageProcessed += MqttClient_ApplicationMessageProcessed;

			await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("s/ds").Build());
			await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("s/e").Build());

			string topic1 = "s/us";
			string payload1 = "100, My MQTT device, c8y_MQTTDevice";
			var message = new MqttApplicationMessageBuilder()
				.WithTopic(topic1)
				.WithPayload(payload1)
				.WithExactlyOnceQoS()
				.WithRetainFlag()
				.Build();

			await mqttClient.PublishAsync(message);
			string topic2 = "s/us";
			string payload2 = "114,c8y_Restart";

			var message2 = new MqttApplicationMessageBuilder()
				.WithTopic(topic2)
				.WithPayload(payload2)
				.WithExactlyOnceQoS()
				.WithRetainFlag()
				.Build();

			await mqttClient.PublishAsync(message2);

			// StartAsync returns immediately, as it starts a new thread using Task.Run, 
			// and so the calling thread needs to wait.
			Console.ReadLine();
		}

		private static void MqttClient_ApplicationMessageProcessed(object sender, ApplicationMessageProcessedEventArgs e)
		{
			Console.WriteLine("Message Processed");
			Console.WriteLine(e.ApplicationMessage.ApplicationMessage.Topic);
			Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.ApplicationMessage.Payload)}");
		}
	}
}
