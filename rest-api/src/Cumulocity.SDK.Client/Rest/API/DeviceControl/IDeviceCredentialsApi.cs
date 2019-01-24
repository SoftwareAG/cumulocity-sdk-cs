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
		NewDeviceRequestRepresentation Register(string id);

		/// <summary>
		/// Remove a device registration.
		/// </summary>
		void Delete(NewDeviceRequestRepresentation representation);

		/// <summary>
		/// Schedule polling credentials task, invoking it at the specified execution time and subsequently with the given interval
		///
		/// Execution will end after timeout
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <param name="interval"> - how often request is sent in seconds </param>
		/// <param name="timeout"> - after how many seconds polling Process will expire
		///  </param>
		DeviceCredentialsRepresentation PollCredentials(string deviceId, int interval, int timeout);

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
		DeviceCredentialsRepresentation PollCredentials(string deviceId);
	}
}