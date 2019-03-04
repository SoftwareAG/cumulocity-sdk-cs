using System;
using System.Threading.Tasks;
using Cumulocity.SDK.MQTT;
using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.ConnectionOptions;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using MQTTnet;
using MQTTnet.Client;

namespace HelloJsonExample
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Task.Run(RunClientAsync);
			new System.Threading.AutoResetEvent(false).WaitOne();
		}

		private static async Task RunClientAsync()
		{
			var cDetails = new ConnectionDetailsBuilder()
				.WithClientId("123456789")
				.WithHost("piotr.staging.c8y.io/mqtt")
				.WithCredentials("user/admin","pass")
				.WithCleanSession(true)
				.WithWs()
				.Build();

			string topic = "s/us";
			string payload = "100, My MQTT device, c8y_MQTTDevice";
			var message = new MqttMessageRequestBuilder()
				.WithTopicName(topic)
				.WithQoS(QoS.EXACTLY_ONCE)
				.WithMessageContent(payload)
				.Build();

			MqttClientExt client = new MqttClientExt(cDetails);
			await client.EstablishConnectionAsync();
			await client.PublishAsync(message);

			///////////////////////////////
			///
			// Use WebSocket connection.
			//var options = new MqttClientOptionsBuilder()
			//	.WithWebSocketServer("127.0.0.1:5000/mqtt")
			//	.Build();
			//// Create a new MQTT client.
			//var factory = new MqttFactory();
			//var client = factory.CreateMqttClient();

			//await client.ConnectAsync(options);

			//client.Connected += async (s, e) =>
			//{
			//	Console.WriteLine("### CONNECTED WITH SERVER ###");

			//	// Subscribe to a topic
			//	await client.SubscribeAsync(new TopicFilterBuilder().WithTopic("message").Build());

			//	Console.WriteLine("### SUBSCRIBED ###");
			//};
			//client.ApplicationMessageReceived += Client_ApplicationMessageReceived;
			//var message = new MqttApplicationMessageBuilder()
			//	.WithTopic("MyTopic")
			//	.WithPayload("Hello World")
			//	.WithExactlyOnceQoS()
			//	.WithRetainFlag()
			//	.Build();

			//await client.PublishAsync(message);

		}

		private static void Client_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
		{
			Console.WriteLine("### MSG ###");
		}
	}
}
