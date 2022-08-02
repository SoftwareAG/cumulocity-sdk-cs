using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;
using System.Threading;
using Cumulocity.SDK.Client.Logging;
using Thread = System.Threading.Thread;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
#pragma warning disable CS0618
#pragma warning disable CS0168
	public class OperationsQueueHandler
	{
		public const string INTERNAL = "Internal";
		private static readonly ILog LOG = LogProvider.For<OperationsQueueHandler>();

		internal double queuePollTimeOut = 1000;

		internal bool active = false;

		internal IOperationProcessor operationProcessor;

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
		public virtual void Stop()
		{
			lock (this)
			{
				active = false;
			}
		}

		/// <summary>
		/// Starts, if it's not started yet
		/// </summary>
		public virtual void Start()
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
							executingOperation = deviceControlApi.Update(op);
						}

						bool result = operationProcessor.Process(op);

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
							deviceControlApi.Update(executingOperation);
						}
					}
				}
				catch (ThreadInterruptedException e)
				{
					LOG.Warn("Thread interrupted while processing operationRep", e);
				}
				catch (SDKException e)
				{
					// TODO Auto-generated catch block
					LOG.Debug(e.ToString());
					LOG.Debug(e.StackTrace);
				}
			}
			Interlocked.Exchange(ref usingResource, 0);
		}
	}
}