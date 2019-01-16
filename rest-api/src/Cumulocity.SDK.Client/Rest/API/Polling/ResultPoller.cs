namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	/// <summary>
	/// This interface extends <seealso cref="Poller"/> functionality providing way of accessing polling result when available.
	/// </summary>
	public interface IResultPoller<out K> : IPoller
	{
		K startAndPoll();
	}
}