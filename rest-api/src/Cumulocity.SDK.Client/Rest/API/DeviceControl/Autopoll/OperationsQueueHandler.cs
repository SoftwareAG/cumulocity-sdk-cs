using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;
using System.Threading;
using Thread = System.Threading.Thread;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
	public class OperationsQueueHandler
	{
		public const string INTERNAL = "Internal";

		//private static Logger logger = LoggerFactory.getLogger(typeof(OperationsQueueHandler));

		internal double queuePollTimeOut = 1000;

		internal bool active = false;

		internal IOperationProcessor operationProcessor;

		//internal AtomicBoolean running = new AtomicBoolean(false);
		//0 for false, 1 for true.
		private static long usingResource = 0;

		internal OperationsQueue queue;

		internal IDeviceControlApi deviceControlApi;

		public OperationsQueueHandler(IOperationProcessor operationProcessor, OperationsQueue queue, IDeviceControlApi deviceControlApi)
		{
			this.operationProcessor = operationProcessor;
			this.queue = queue;
			this.deviceControlApi = deviceControlApi;
		}

		/// <summary>
		/// Stops after finishing operation in progress
		/// </summary>
		public virtual void stop()
		{
			lock (this)
			{
				active = false;
			}
		}

		/// <summary>
		/// Starts, if it's not started yet
		/// </summary>
		public virtual void start()
		{
			lock (this)
			{
				if (!active)
				{
					active = true;
					(new Thread(ThreadProc)).Start();
				}
			}
		}

		private bool Active
		{
			get
			{
				lock (this)
				{
					return active;
				}
			}
		}

		internal virtual bool Running => Interlocked.Read(ref usingResource) == 1;

		private void ThreadProc()
		{
			OperationRepresentation op = null;
			Interlocked.Exchange(ref usingResource, 1);
			while (Active)
			{
				try
				{
					queue.TryTake(out op, TimeSpan.FromMilliseconds(queuePollTimeOut));
					if (op != null)
					{
						// TODO - refactor. This isn't pretty
						GId gid = op.Id;
						bool? internalOperation = gid.Value.StartsWith(INTERNAL);

						OperationRepresentation executingOperation = null;
						if (!internalOperation.Value)
						{
							// change status of operation in REST to "executing"
							op.Status = OperationStatus.EXECUTING.ToString();
							executingOperation = deviceControlApi.update(op);
						}

						bool result = operationProcessor.process(op);

						if (!internalOperation.Value)
						{
							// change status of operation in REST according to
							// result
							if (result)
							{
								executingOperation.Status = OperationStatus.SUCCESSFUL.ToString();
							}
							else
							{
								executingOperation.Status = OperationStatus.FAILED.ToString();
							}
							deviceControlApi.update(executingOperation);
						}
					}
				}
				catch (ThreadInterruptedException e)
				{
					//logger.warn("Thread interrupted while processing operationRep", e);
				}
				catch (SDKException e)
				{
					// TODO Auto-generated catch block
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
				}
			}
			Interlocked.Exchange(ref usingResource, 0);
		}
	}
}