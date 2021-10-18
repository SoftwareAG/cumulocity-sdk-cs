using AutoFixture;
using AutoFixture.AutoMoq;
using Cumulocity.SDK.MQTT.Model;
using Moq;
using System;
using Xunit;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using Cumulocity.SDK.MQTT.Operations;
using MQTTnet.Extensions.ManagedClient;
using Cumulocity.SDK.MQTT.Exception;
using System.Threading.Tasks;

namespace Cumulocity.SDK.MQTT.Tests
{
	public class MqttClientTest
	{
		private IMqttClient client;
		private IConnectionDetails cDetails;
		private Mock<IManagedMqttClient> managedMqttClient;
		private Mock<IOperationsProvider> operationsProvider;

		public MqttClientTest()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMoqCustomization());

			 cDetails = new ConnectionDetailsBuilder()
							   .WithClientId("123456789")
							   .WithHost("test.c8y.io")
							   .WithCredentials("tenant/user", "password")
							   .WithCleanSession(true)
							   .Build();

			 var connectionDetails = fixture.Freeze<Mock<IConnectionDetails>>();
			 managedMqttClient = fixture.Freeze<Mock<IManagedMqttClient>>();
			operationsProvider = fixture.Freeze<Mock<IOperationsProvider>>();
			operationsProvider.Setup(x => x.ConnectionEstablished).Returns(true);
			managedMqttClient.Setup(x => x.IsStarted).Returns(true);

			client = fixture.Create<MqttClient>();
		}


		[Fact]
		public async Task PublishToTopicAsync()
		{
			// Given
			string topic = "s/us";
			string payload = "100, My MQTT device, c8y_MQTTDevice";
			var message = new MqttMessageRequestBuilder()
											.WithTopicName(topic)
											.WithQoS(QoS.EXACTLY_ONCE)
											.WithMessageContent(payload)
											.Build();

			// When
			await client.PublishAsync(message);

			// Then
			operationsProvider.Verify(x => x.PublishAsync(It.IsAny<IMqttMessageRequest>()), Times.Once);
		}

		[Fact]
		public async Task PublishToWrongTopic()
		{
			// Given
			String topic = "s/usp";
			String payload = "100, My MQTT device, c8y_MQTTDevice";
			var message = new MqttMessageRequestBuilder()
											.WithTopicName(topic)
											.WithQoS(QoS.EXACTLY_ONCE)
											.WithMessageContent(payload).Build();

			// When
			// Then
			await Assert.ThrowsAsync<MqttDeviceSDKException>(() => client.PublishAsync(message));

		}
		[Fact]
		public async Task SubscribeToTopic()
		{

			// Given
			String topic = "s/ds";
			var message = new MqttMessageRequestBuilder()
											.WithTopicName(topic)
											.WithQoS(QoS.EXACTLY_ONCE)
											.Build();

			// When
			await client.SubscribeAsync(message);

			// Then
			operationsProvider.Verify(x => x.SubscribeAsync(It.IsAny<IMqttMessageRequest>()), Times.Once);
		}

		[Fact]
		public async Task UnsubscribeFromTopic()
        {
			// Given
			string topic = "s/ds";

			// When 
			await client.UnsubscribeAsync(topic);

			// Then
			operationsProvider.Verify(x => x.UnsubscribeAsync(It.IsAny<string>()), Times.Once);
        }

		[Fact]
		public async Task SubscribeToWrongTopic()
		{

			// Given
			String topic = "s/xyz";
			var message = new MqttMessageRequestBuilder().WithTopicName(topic).WithQoS(QoS.AT_LEAST_ONCE).Build();

			// When
			// Then
			await Assert.ThrowsAsync<MqttDeviceSDKException>(() => client.SubscribeAsync(message));

		}

		[Fact]
		public async Task UnsubscribeFromWrongTopic()
        {
			// Given
			string topic = "s/xyz";

			// When
			// Then
			await Assert.ThrowsAsync<MqttDeviceSDKException>(() => client.UnsubscribeAsync(topic));
        }

		[Fact]
		public async Task SubscribeAndUnsubscribeFromTopic()
        {
			// Given
			String topic = "s/ds";
			var message = new MqttMessageRequestBuilder()
											.WithTopicName(topic)
											.WithQoS(QoS.EXACTLY_ONCE)
											.Build();

			// When
			await client.SubscribeAsync(message);

			// Then
			operationsProvider.Verify(x => x.SubscribeAsync(It.IsAny<IMqttMessageRequest>()), Times.Once);

			// When 
			await client.UnsubscribeAsync(topic);

			// Then
			operationsProvider.Verify(x => x.UnsubscribeAsync(It.IsAny<string>()), Times.Once);
		}
	}
}
