using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using System;
using Cumulocity.SDK.Client.Logging;
using Cumulocity.SDK.Client.Rest.API.Notification.Subscriber;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
#pragma warning disable CS0168
	public class OperationsByAgentAndStatusPollerImpl : FixedRatePoller
	{
		private static long PERIOD_INTERVAL = 10000;

		private OperationsQueueHandler queueHandler;
		private IDeviceControlApi deviceControlApi;
		private OperationFilter filter;
		private static readonly ILog LOG = LogProvider.For<OperationsByAgentAndStatusPollerImpl>();

		public OperationsByAgentAndStatusPollerImpl(IDeviceControlApi deviceControlApi, String agentId,
			OperationStatus status, IOperationProcessor operationProcessor)
			: base(new ScheduledThreadPoolExecutor(1), PERIOD_INTERVAL)
		{
			this.deviceControlApi = deviceControlApi;
			this.queueHandler = new OperationsQueueHandler(operationProcessor, OperationsQueue.Instance, deviceControlApi);
			this.filter = new OperationFilter().ByAgent(agentId).ByStatus(status);

			base.PollingTask = PollingResult;
		}

		private void PollingResult()
		{
			try
			{
				// polls for new operationRep representations and queue them
				var collectionReps = deviceControlApi.GetOperationsByFilter(filter).GetFirstPage();
				OperationsQueue.Instance.addAll(collectionReps.Operations);
			}
			catch (Exception e)
			{
				LOG.Error("Problem polling for operations", e);
			}
		}

		public override bool Start()
		{
			if (!base.Start())
			{
				return false;
			}

			//Start operationRep handler, which is getting operations from blocking queue
			queueHandler.Start();
			return true;
		}

		public override void Stop()
		{
			base.Stop();

			//Stop operationRep handler, which is getting operations from blocking queue
			queueHandler.Stop();
		}
	}
}