using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Text;

namespace HelloMqttNet
{
    internal class Program
    {
        private static IManagedMqttClient mqttClient;

        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Write all trace messages to the console window.
            MqttNetGlobalLogger.LogMessagePublished += (s, e) =>
            {
                var trace = $">> [{e.TraceMessage.Timestamp:O}] [{e.TraceMessage.ThreadId}] [{e.TraceMessage.Source}] [{e.TraceMessage.Level}]: {e.TraceMessage.Message}";
                if (e.TraceMessage.Exception != null)
                {
                    trace += Environment.NewLine + e.TraceMessage.Exception.ToString();
                }

                Console.WriteLine(trace);
            };
            var clientID = Guid.NewGuid().ToString();
            // Setup and start a managed MQTT client.
            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(clientID)
                    .WithTcpServer("c8y.io")
                    .WithCredentials("username", "pass")
                    .WithCleanSession(true)
                    .Build())
                .Build();

            mqttClient = new MqttFactory().CreateManagedMqttClient();
            await mqttClient.StartAsync(options);
            mqttClient.ApplicationMessageProcessed += MqttClient_ApplicationMessageProcessed;
            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
            mqttClient.Connected += MqttClient_Connected;

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

        private static void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("s/e").Build());
        }

        private static void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Application Message Received");
            Console.WriteLine(e.ApplicationMessage.Topic);
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
        }

        private static void MqttClient_ApplicationMessageProcessed(object sender, ApplicationMessageProcessedEventArgs e)
        {
            Console.WriteLine("Message Processed");
            Console.WriteLine(e.ApplicationMessage.ApplicationMessage.Topic);
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.ApplicationMessage.Payload)}");
        }
    }
}