using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
using Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	public class DeviceCredentialsApiImpl : IDeviceCredentialsApi
	{
		public const string DEVICE_CREDENTIALS_URI = "devicecontrol/deviceCredentials";
		public const string DEVICE_REQUEST_URI = "devicecontrol/newDeviceRequests";

		private readonly RestConnector restConnector;
		private readonly string credentialsUrl;
		private readonly string requestUrl;

		public DeviceCredentialsApiImpl(PlatformParameters platformParameters, RestConnector restConnector)
		{
			this.restConnector = restConnector;
			this.credentialsUrl = platformParameters.Host + DEVICE_CREDENTIALS_URI;
			this.requestUrl = platformParameters.Host + DEVICE_REQUEST_URI;
		}

		public NewDeviceRequestRepresentation register(string id)
		{
			NewDeviceRequestRepresentation representation = new NewDeviceRequestRepresentation();
			representation.Id = id;
			return restConnector.Post(requestUrl, DeviceControlMediaType.NEW_DEVICE_REQUEST, representation);
		}

		public void delete(NewDeviceRequestRepresentation representation)
		{
			restConnector.Delete(representation.Self);
		}

		public DeviceCredentialsRepresentation pollCredentials(string deviceId)
		{
			DeviceCredentialsRepresentation representation = new DeviceCredentialsRepresentation();
			representation.Id = deviceId;
			return restConnector.Post<DeviceCredentialsRepresentation>(credentialsUrl, DeviceControlMediaType.DEVICE_CREDENTIALS, representation);
		}

		public DeviceCredentialsRepresentation pollCredentials(string deviceId, int interval, int timeout)
		{
			PollingStrategy pollingStrategy = new PollingStrategy((long)timeout, TimeUnit.SECONDS, (long)interval);
			return pollCredentials(deviceId, pollingStrategy);
		}

		public DeviceCredentialsRepresentation pollCredentials(string deviceId, PollingStrategy pollingStrategy)
		{
			return aPoller(deviceId, pollingStrategy).startAndPoll();
		}

		private AlteringRateResultPoller<DeviceCredentialsRepresentation> aPoller(string deviceId, PollingStrategy pollingStrategy)
		{
			GetResultTask<DeviceCredentialsRepresentation> pollingTask = new GetResultTask<DeviceCredentialsRepresentation>(this, deviceId);
			return new AlteringRateResultPoller<DeviceCredentialsRepresentation>(pollingTask, pollingStrategy);
		}

		public class GetResultTask<K> : IGetResultTask<K> where K : new()
		{
			private string DeviceId { get; set; }
			private DeviceCredentialsApiImpl DeviceCredentialsApiImpl { get; set; }

			public GetResultTask(DeviceCredentialsApiImpl deviceCredentialsApiImpl, string deviceId)
			{
				this.DeviceId = deviceId;
				this.DeviceCredentialsApiImpl = deviceCredentialsApiImpl;
			}

			public K TryGetResult()
			{
				try
				{
					var readData = DeviceCredentialsApiImpl.pollCredentials(DeviceId);
					if (readData is DeviceCredentialsApiImpl)
					{
						return (K)(object)readData;
					}

					return (K)Convert.ChangeType(readData, typeof(K));
				}
				catch (InvalidCastException)
				{
					return default(K);
				}
			}
		}
	}
}