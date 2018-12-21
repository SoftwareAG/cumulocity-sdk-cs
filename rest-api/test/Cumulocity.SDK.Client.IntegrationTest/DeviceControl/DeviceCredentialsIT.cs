using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.DeviceControl
{
	public class DeviceCredentialsIT : IClassFixture<DeviceCredentialsFixture>, IDisposable
	{
		private DeviceCredentialsApiImpl deviceCredentialsResource;
		private ResponseParser responseParser;
		private RestConnector restConnector;
		private DeviceCredentialsFixture fixture;

		public DeviceCredentialsIT(DeviceCredentialsFixture fixture)
		{
			this.fixture = fixture;
			deviceCredentialsResource = (DeviceCredentialsApiImpl)fixture.platform.DeviceCredentialsApi;
			responseParser = new ResponseParser();
			restConnector = new RestConnector(fixture.platform, responseParser);
		}

		[Fact]
		public void shouldPollCredentialsUntilDeviceAccepted()
		{
			string deviceId = "3000";
			int pollIntervalInSeconds = 2;
			createNewDeviceRequest(deviceId);

			//Timer timer = new Timer();
			//timer.schedule(new TimerTask()
			//{
			//	@Override

			//	public void run()
			//	{
			//		acceptNewDeviceRequest(deviceId);
			//	}
			//}, pollIntervalInSeconds* 2 * 1000);

			DeviceCredentialsRepresentation credentials = deviceCredentialsResource.pollCredentials(deviceId, pollIntervalInSeconds, 100);
			//assertThat(credentials).isNotNull();
			//assertThat(credentials.getTenantId()).isEqualTo(platform.getTenantId());
			//assertThat(credentials.getUsername()).isEqualTo("device_" + deviceId);
			//assertThat(credentials.getPassword()).isNotEmpty();
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