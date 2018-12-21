using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	/// <summary>
	/// Api for device bootstrap
	/// 
	/// </summary>
	public interface IDeviceCredentialsApi
	{
		/// <summary>
		/// Register a new device.
		/// </summary>
		NewDeviceRequestRepresentation register(string id);

		/// <summary>
		/// Remove a device registration.
		/// </summary>
		void delete(NewDeviceRequestRepresentation representation);

		/// <summary>
		/// Schedule polling credentials task, invoking it at the specified execution time and subsequently with the given interval 
		/// 
		/// Execution will end after timeout
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <param name="interval"> - how often request is sent in seconds </param>
		/// <param name="timeout"> - after how many seconds polling process will expire
		///  </param>
		DeviceCredentialsRepresentation pollCredentials(string deviceId, int interval, int timeout);

		/// <summary>
		/// Device poll credentials
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <param name="strategy"> </param>
		DeviceCredentialsRepresentation pollCredentials(string deviceId, PollingStrategy strategy);

		/// <summary>
		/// Executes single request to credentials endpoint
		/// </summary>
		/// <param name="deviceId">
		/// @return </param>
		DeviceCredentialsRepresentation pollCredentials(string deviceId);
	}
}
