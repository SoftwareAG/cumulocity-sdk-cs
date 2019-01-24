using System;

namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	/// <summary>
	/// A poller that triggers tasks to execute at a fixed rate.<br>
	/// This class will trigger the defined pollingTask to run at the configured period.
	/// </summary>
	public abstract class FixedRatePoller : IPoller
	{
		//private static readonly Logger LOG = LoggerFactory.getLogger(typeof(FixedRatePoller));

		private ScheduledThreadPoolExecutor pollingExecutor;

		private Action pollingTask;

		private long periodInterval;

		/// <summary>
		/// Create a fixed rate poller using the given thread pool and with the given fixed rate period.
		/// </summary>
		/// <param name="periodInterval"> polling interval in milliseconds </param>
		public FixedRatePoller(ScheduledThreadPoolExecutor pollingExecutor, long periodInterval) : base()
		{
			this.pollingExecutor = pollingExecutor;
			this.periodInterval = periodInterval;
		}

		public virtual bool Start()
		{
			if (pollingTask == null)
			{
				//LOG.error("Poller Start requested without pollingTask being set");
				return false;
			}

			//Start scheduled periodic polling for new operations (only one task in scheduler at any given time)

			pollingExecutor.ScheduleAtFixedRate(pollingTask, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(periodInterval));

			return true;
		}

		public virtual void Stop()
		{
			//shutdown operationsPollingExecutor if it's running or if it's no shutting down just now
			pollingExecutor.Shutdown();
		}

		protected internal virtual Action PollingTask
		{
			set
			{
				this.pollingTask = value;
			}
			get
			{
				return pollingTask;
			}
		}

		/// <returns> the fixed rate polling interval in milliseconds </returns>
		protected internal virtual long Period
		{
			get
			{
				return periodInterval;
			}
		}
	}
}