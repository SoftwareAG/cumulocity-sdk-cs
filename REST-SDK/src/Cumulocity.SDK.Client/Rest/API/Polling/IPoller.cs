namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	/// <summary>
	/// This interface provides methods for controlling polling tasks. It can be used for any task that needs
	/// to be run according to some schedule.
	/// </summary>
	public interface IPoller
	{
		/// <summary>
		/// Starts poller
		/// </summary>
		/// <returns> true if the poller was successfully started </returns>
		bool Start();

		/// <summary>
		/// Stops poller after finishing all started jobs
		/// </summary>
		void Stop();
	}
}