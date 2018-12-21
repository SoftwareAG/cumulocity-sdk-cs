using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
using Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

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

		public  NewDeviceRequestRepresentation register(string id)
		{
			NewDeviceRequestRepresentation representation = new NewDeviceRequestRepresentation();
			representation.Id = id;
			return restConnector.Post(requestUrl, DeviceControlMediaType.NEW_DEVICE_REQUEST, representation);
		}

		public  void delete(NewDeviceRequestRepresentation representation)
		{
			restConnector.Delete(representation.Self);
		}

		public  DeviceCredentialsRepresentation pollCredentials(string deviceId)
		{
			//ORIGINAL LINE: final DeviceCredentialsRepresentation representation = new DeviceCredentialsRepresentation();
			DeviceCredentialsRepresentation representation = new DeviceCredentialsRepresentation();
			representation.Id = deviceId;
			return restConnector.Post(credentialsUrl, DeviceControlMediaType.DEVICE_CREDENTIALS, representation);
		}

		public  DeviceCredentialsRepresentation pollCredentials(string deviceId, int interval, int timeout)
		{
			PollingStrategy pollingStrategy = new PollingStrategy((long)timeout, TimeUnit.SECONDS, (long)interval);
			return pollCredentials(deviceId, pollingStrategy);
		}

		public DeviceCredentialsRepresentation pollCredentials(string deviceId, PollingStrategy pollingStrategy)
		{
			return null;
			//return aPoller(deviceId, pollingStrategy).startAndPoll();
		}

		//ORIGINAL LINE: private AlteringRateResultPoller<DeviceCredentialsRepresentation> aPoller(final String deviceId, PollingStrategy pollingStrategy)
		//private AlteringRateResultPoller<DeviceCredentialsRepresentation> aPoller(string deviceId, PollingStrategy pollingStrategy)
		//{
		//	GetResultTask<DeviceCredentialsRepresentation> pollingTask = new GetResultTaskAnonymousInnerClass(this, deviceId);
		//	return new AlteringRateResultPoller<DeviceCredentialsRepresentation>(pollingTask, pollingStrategy);
		//}

		private class GetResultTaskAnonymousInnerClass : GetResultTask<DeviceCredentialsRepresentation>
		{
			private readonly DeviceCredentialsApiImpl outerInstance;

			private string deviceId;

			public GetResultTaskAnonymousInnerClass(DeviceCredentialsApiImpl outerInstance, string deviceId)
			{
				this.outerInstance = outerInstance;
				this.deviceId = deviceId;
			}


			public  DeviceCredentialsRepresentation tryGetResult()
			{
				return outerInstance.pollCredentials(deviceId);
			}

		}
	}
}
