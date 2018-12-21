using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;

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

		private ThreadStart pollingTask;

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

		public bool start()
		{
			if (pollingTask == null)
			{
				//LOG.error("Poller start requested without pollingTask being set");
				return false;
			}

			////start scheduled periodic polling for new operations (only one task in scheduler at any given time)
			//if (pollingExecutor.TaskCount == 0)
			//{
			//	pollingExecutor.scheduleAtFixedRate(pollingTask, 0, periodInterval, TimeUnit.MILLISECONDS);
			//}

			return true;
		}

		public  void stop()
		{
			//shutdown operationsPollingExecutor if it's running or if it's no shutting down just now
			pollingExecutor.Shutdown();
		}

		protected internal virtual ThreadStart PollingTask
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
