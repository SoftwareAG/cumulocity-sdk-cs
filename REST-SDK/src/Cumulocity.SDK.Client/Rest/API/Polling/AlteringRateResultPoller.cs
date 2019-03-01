using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
using System;
using Cumulocity.SDK.Client.Logging;

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
		private static readonly ILog LOG = LogProvider.For<AlteringRateResultPoller<K>>();
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
				Console.WriteLine("Poller Start requested without pollingTask being Set");
				LOG.Error("Poller Start requested without pollingTask being Set");
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
				LOG.Info("Polling with wrong result: " + Digest(ex.Message));
				if (pollingStrategy != null)
				{
					LOG.Info("Try again in " + pollingStrategy.peakNext() / 1000 + " seconds.");
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
					LOG.Error("Timeout occured, last exception: " + lastException);
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
				LOG.Error("Poller Start requested without pollingTask being Set");
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

		private static String Digest(String message)
		{
			if (message == null || message.Length < 200)
			{
				return message;
			}
			else
			{
				return message.Substring(0, Math.Min(message.Length - 1, 200));
			}
		}
	}
}