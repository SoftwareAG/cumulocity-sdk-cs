using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;
using System.Threading;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.DeviceControl
{
	public class DeviceCredentialsIT : IClassFixture<DeviceCredentialsFixture>, IDisposable
	{
		private readonly DeviceCredentialsApiImpl deviceCredentialsResource;
		private readonly RestConnector restConnector;
		private readonly DeviceCredentialsFixture fixture;

		public DeviceCredentialsIT(DeviceCredentialsFixture fixture)
		{
			this.fixture = fixture;
			deviceCredentialsResource = (DeviceCredentialsApiImpl)fixture.bootstrap.DeviceCredentialsApi;
			var responseParser = new ResponseParser();
			restConnector = new RestConnector(fixture.platform, responseParser);
		}

		[Fact]
		public void shouldPollCredentialsUntilDeviceAccepted()
		{
			string deviceId = "3000";
			int pollIntervalInSeconds = 2;
			createNewDeviceRequest(deviceId);

			var timer = new Timer(
				callback: new TimerCallback((_) => { acceptNewDeviceRequest(deviceId); }),
				state: null,
				dueTime: 0,
				period: pollIntervalInSeconds * 2 * 1000);

			DeviceCredentialsRepresentation credentials = deviceCredentialsResource.pollCredentials(deviceId, pollIntervalInSeconds, 100);

			Assert.NotNull(credentials);
			Assert.Equal(credentials.Id, this.fixture.platform.TenantId);
			Assert.Equal(credentials.Username, "device_" + deviceId);
			Assert.NotEmpty(credentials.Password);
			timer.Dispose();
		}

		private void createNewDeviceRequest(String deviceId)
		{
			NewDeviceRequestRepresentation representation = new NewDeviceRequestRepresentation();
			representation.Id = deviceId;
			restConnector.Post(newDeviceRequestsUri(), DeviceControlMediaType.NEW_DEVICE_REQUEST, representation);
		}

		private void acceptNewDeviceRequest(String deviceId)
		{
			NewDeviceRequestRepresentation representation = new NewDeviceRequestRepresentation();
			representation.Status = "ACCEPTED";
			restConnector.PutWithoutId(newDeviceRequestUri(deviceId), DeviceControlMediaType.NEW_DEVICE_REQUEST, representation);
		}

		private String newDeviceRequestsUri()
		{
			return this.fixture.platform.Host + "devicecontrol/newDeviceRequests";
		}

		private String newDeviceRequestUri(String deviceId)
		{
			return newDeviceRequestsUri() + "/" + deviceId;
		}

		public void Dispose()
		{
		}
	}
}