using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	public class AlteringRateResultPoller<K>
	{
		private readonly IGetResultTask<K> getResultTask;
		private readonly PollingStrategy pollingStrategy;
		private readonly ScheduledThreadPoolExecutor pollingExecutor = new ScheduledThreadPoolExecutor(1);
		private readonly Action pollingTask;
		private CountDownLatch latch;
		private Exception lastException;
		private K result;

		public AlteringRateResultPoller(IGetResultTask<K> getResultTask, PollingStrategy strategy)
		{
			this.getResultTask = getResultTask;
			this.pollingStrategy = strategy;
			this.pollingTask = pollingAppendResult;
		}

		public bool Start()
		{
			if (pollingTask == null)
			{
				Console.WriteLine("Poller Start requested without pollingTask being set");
				//LOG.error("Poller Start requested without pollingTask being set");
				return false;
			}

			scheduleNextTaskExecution();
			return true;
		}

		private void scheduleNextTaskExecution()
		{
			if (pollingStrategy != null)
			{
				//pollingExecutor.Schedule(pollingTask, TimeSpan.FromMilliseconds(pollingStrategy.popNext()??1));
				pollingExecutor.Schedule(pollingTask, TimeSpan.FromMilliseconds(2000));
			}
		}

		private void pollingAppendResult()
		{
			var task = this.getResultTask;

			if (result != null)
			{
				return;
			}
			try
			{
				result = task.TryGetResult();
				if (result == null)
				{
					scheduleNextTaskExecution();
				}
				else
				{
					latch.Signal();
				}
			}
			catch (Exception ex)
			{
				lastException = ex;
				//LOG.info("Polling with wrong result: " + digest(ex.getMessage()));
				if (pollingStrategy != null)
				{
					//LOG.info("Try again in " + pollingStrategy.peakNext() / 1000 + " seconds.");
				}
				scheduleNextTaskExecution();
			}
		}

		public K startAndPoll()
		{
			latch = new CountDownLatch(1);
			start();
			try
			{
				waitForResult();
				if (result == null && lastException != null)
				{
					//LOG.error("Timeout occured, last exception: " + lastException);
				}
				return result;
			}
			catch (Exception e)
			{
				throw new SDKException("Error polling data", e);
			}
			finally
			{
				stop();
			}
		}

		public bool start()
		{
			if (pollingTask == null)
			{
				//LOG.error("Poller Start requested without pollingTask being set");
				return false;
			}

			scheduleNextTaskExecution();
			return true;
		}

		public void stop()
		{
			//shutdown operationsPollingExecutor if it's running or if it's no shutting down just now
			pollingExecutor.Shutdown();
		}

		private void waitForResult()
		{
			if (pollingStrategy.Timeout == null)
			{
				latch.Wait();
			}
			else
			{
				latch.Wait((int)pollingStrategy.Timeout);
			}
		}
	}
}