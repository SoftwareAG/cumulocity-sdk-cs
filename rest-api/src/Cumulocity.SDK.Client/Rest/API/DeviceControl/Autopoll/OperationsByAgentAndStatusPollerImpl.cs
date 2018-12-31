using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using System;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
	public class OperationsByAgentAndStatusPollerImpl : FixedRatePoller
	{
		private static long PERIOD_INTERVAL = 10000;

		private OperationsQueueHandler queueHandler;
		private IDeviceControlApi deviceControlApi;
		private OperationFilter filter;

		public OperationsByAgentAndStatusPollerImpl(IDeviceControlApi deviceControlApi, String agentId,
			OperationStatus status, IOperationProcessor operationProcessor)
			: base(new ScheduledThreadPoolExecutor(1), PERIOD_INTERVAL)
		{
			this.deviceControlApi = deviceControlApi;
			this.queueHandler = new OperationsQueueHandler(operationProcessor, OperationsQueue.Instance, deviceControlApi);
			this.filter = new OperationFilter().byAgent(agentId).byStatus(status);

			base.PollingTask = pollingResult;
		}

		private void pollingResult()
		{
			try
			{
				// polls for new operationRep representations and queue them
				var collectionReps = deviceControlApi.getOperationsByFilter(filter).get();
				OperationsQueue.Instance.addAll(collectionReps.Operations);
			}
			catch (Exception e)
			{
				//logger.error("Problem polling for operations", e);
			}
		}
	}
}