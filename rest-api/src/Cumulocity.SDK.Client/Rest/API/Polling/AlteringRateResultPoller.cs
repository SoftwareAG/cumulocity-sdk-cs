using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;

namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	public class AlteringRateResultPoller<K> : IResultPoller<K>
	{

		//private static readonly Logger LOG = LoggerFactory.getLogger(typeof(AlteringRateResultPoller));

		public interface GetResultTask<K>
		{
			K tryGetResult();
		}

		private readonly PollingStrategy pollingStrategy;
		private readonly ScheduledThreadPoolExecutor pollingExecutor;
		private readonly Runnable pollingTask;
		private CountDownLatch latch;
		private  K result;
		private Exception lastException;

		public AlteringRateResultPoller(GetResultTask<K> task, PollingStrategy strategy)
		{
			this.pollingStrategy = strategy;
			this.pollingExecutor = new ScheduledThreadPoolExecutor(1, new ThreadFactory());
			this.pollingTask = wrapAsRunnable(task);
		}

		public  bool start()
		{
			if (pollingTask == null)
			{
				//LOG.error("Poller start requested without pollingTask being set");
				return false;
			}

			scheduleNextTaskExecution();
			return true;
		}

		private void scheduleNextTaskExecution()
		{
			if (!pollingStrategy.Empty)
			{
				pollingExecutor.Schedule(pollingTask, pollingStrategy.popNext()??0, TimeUnit.MILLISECONDS);
			}

		}

		public  void stop()
		{
			//shutdown operationsPollingExecutor if it's running or if it's no shutting down just now
			pollingExecutor.Shutdown();
		}

		public  K startAndPoll()
		{
			latch = new CountDownLatch(1);
			start();
			try
			{
				waitForResult();
				//if (result == default(K) && lastException != null)
				//{
				//	//LOG.error("Timeout occured, last exception: " + lastException);
				//}
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

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: private void waitForResult() throws InterruptedException
		private void waitForResult()
		{
			if (pollingStrategy.Timeout == null)
			{
				//latch.await();
			}
			else
			{
				//latch.await(pollingStrategy.Timeout, TimeUnit.MILLISECONDS);
			}
		}

		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		//ORIGINAL LINE: private Runnable wrapAsRunnable(final GetResultTask<K> task)
		private Runnable wrapAsRunnable(GetResultTask<K> task)
		{
			//Todo: Runnable
			return null;
		}

		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		//ORIGINAL LINE: private void appendResult(final GetResultTask<K> task)
		private void appendResult(GetResultTask<K> task)
		{
			//if (result != default(K))
			//{
			//	return;
			//}
			try
			{
				//result = task.tryGetResult();
				//if (result == default(K))
				//{
				//	scheduleNextTaskExecution();
				//}
				//else
				//{
				//	latch.CountDown();
				//}
			}
			catch (Exception ex)
			{
				lastException = ex;
				//LOG.info("Polling with wrong result: " + digest(ex.Message));
				if (!pollingStrategy.Empty)
				{
					//LOG.info("Try again in " + pollingStrategy.peakNext() / 1000 + " seconds.");
				}
				scheduleNextTaskExecution();
			}
		}

		private static string digest(string message)
		{
			if (string.ReferenceEquals(message, null) || message.Length < 200)
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
