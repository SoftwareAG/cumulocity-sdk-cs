using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;

namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	public class AlteringRateResultPoller<K>
	{

		IGetResultTask<K> getResultTask;
		PollingStrategy pollingStrategy;
		ScheduledThreadPoolExecutor pollingExecutor = new ScheduledThreadPoolExecutor(1);
		CountDownLatch latch;
		Exception lastException;
		K result;
		Action pollingTask;

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
				Console.WriteLine("Poller start requested without pollingTask being set");
				//LOG.error("Poller start requested without pollingTask being set");
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
				pollingExecutor.Schedule(pollingTask, TimeSpan.FromMilliseconds(300));
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
				//LOG.error("Poller start requested without pollingTask being set");
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
