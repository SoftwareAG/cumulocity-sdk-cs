using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll;
using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using Cumulocity.SDK.Client.Rest.Utils;
using Microsoft.Extensions.Logging;

namespace Cumulocity.SDK.Client.Rest.API.Measurement.Autopoll
{
	/// <summary>
	/// Starts a regular poller that adds measurement operation requests to the
	/// operations queue at a regular interval. Note: this only adds the request -
	/// processing the request must be handled by a concrete OperationProcessor.
	/// </summary>
	public sealed class MeasurementFixedRatePoller : FixedRatePoller
	{

		public MeasurementFixedRatePoller(long periodInterval) : base(new ScheduledThreadPoolExecutor(1), periodInterval)
		{
			PollingTask = pollingResult;
		}
		private void pollingResult()
		{
			try
			{
				OperationRepresentation newOp = Operations.createNewMeasurementOperation();
				newOp.Id = new GId(OperationsQueueHandler.INTERNAL + ":NewMeasurement-" + DateTimeHelperClass.CurrentUnixTimeMillis().ToString());
				OperationsQueue.Instance.Add(newOp);
			}
			catch (Exception e)
			{
				//logger.error("Problem polling for operations", e);
			}
		}
	}
}
