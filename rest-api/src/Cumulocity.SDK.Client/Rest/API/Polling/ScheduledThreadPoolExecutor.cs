using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	public class ScheduledThreadPoolExecutor
	{
		public int ThreadCount => threads.Length;
		public EventHandler<Exception> OnException;
		private bool shutdown = false;
		private ManualResetEvent waiter;
		private Thread[] threads;
		private SortedSet<Tuple<DateTime, Action>> queue;

		public ScheduledThreadPoolExecutor(int threadCount)
		{
			waiter = new ManualResetEvent(false);
			queue = new SortedSet<Tuple<DateTime, Action>>();
			OnException += (o, e) => { };
			threads = Enumerable.Range(0, threadCount).Select(i => new Thread(RunLoop)).ToArray();
			foreach (var thread in threads)
			{
				thread.Start();
			}
		}
		public void ScheduleWithFixedDelay(Action action, TimeSpan initialDelay, TimeSpan delay)
		{
			Schedule(() =>
			{
				action();
				ScheduleWithFixedDelay(action, delay, delay);
			}, DateTime.Now + initialDelay);
		}
		public void Schedule(Action action, TimeSpan initialDelay)
		{
			Schedule(action, DateTime.Now + initialDelay);
		}

		private void Schedule(Action action, DateTime time)
		{
			lock (waiter)
			{
				queue.Add(Tuple.Create(time, action));
			}

			waiter.Set();
		}

		public void ScheduleAtFixedRate(Action action, TimeSpan initialDelay, TimeSpan delay)
		{
			DateTime scheduleTime = DateTime.Now + initialDelay;
			Action registerTask = null;

			registerTask = () =>
			{
				Schedule(() =>
				{
					action();
					scheduleTime += delay;
					registerTask();
				}, scheduleTime);
			};

			registerTask();
		}

		private void RunLoop()
		{
			while (true)
			{
				TimeSpan sleepingTime = TimeSpan.FromMilliseconds(Int32.MaxValue);
				bool needToSleep = true;
				Action task = null;

				try
				{
					lock (waiter)
					{
						if (IsShutdown())
							break;
						if (queue.Any())
						{
							if (queue.First().Item1 <= DateTime.Now)
							{
								task = queue.First().Item2;
								queue.Remove(queue.First());
								needToSleep = false;
							}
							else
							{
								sleepingTime = queue.First().Item1 - DateTime.Now;
							}
						}
					}

					if (needToSleep)
					{
						waiter.WaitOne((int)sleepingTime.TotalMilliseconds);
					}
					else
					{
						task();
					}
				}
				catch (Exception e)
				{
					OnException(task, e);
				}
			}
		}

		internal void Shutdown()
		{
			lock (waiter)
			{
				shutdown = true;
			}
		}
		public bool IsShutdown()
		{
			return shutdown;
		}
	}
}
