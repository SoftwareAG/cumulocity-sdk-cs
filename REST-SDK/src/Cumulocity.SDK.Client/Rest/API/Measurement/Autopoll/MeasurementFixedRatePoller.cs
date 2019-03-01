using Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll;
using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using Cumulocity.SDK.Client.Rest.Utils;
using System;
using Cumulocity.SDK.Client.Logging;

namespace Cumulocity.SDK.Client.Rest.API.Measurement.Autopoll
{
#pragma warning disable CS0168
	/// <summary>
	/// Starts a regular poller that adds measurement operation requests to the
	/// operations queue at a regular interval. Note: this only adds the request -
	/// processing the request must be handled by a concrete OperationProcessor.
	/// </summary>
	public sealed class MeasurementFixedRatePoller : FixedRatePoller
	{
		private static readonly ILog LOG = LogProvider.For<MeasurementFixedRatePoller>();
		public MeasurementFixedRatePoller(long periodInterval) : base(new ScheduledThreadPoolExecutor(1), periodInterval)
		{
			PollingTask = PollingResult;
		}

		private void PollingResult()
		{
			try
			{
				OperationRepresentation newOp = Operations.createNewMeasurementOperation();
				newOp.Id = new GId(OperationsQueueHandler.INTERNAL + ":NewMeasurement-" + DateTimeHelperClass.CurrentUnixTimeMillis().ToString());
				OperationsQueue.Instance.Add(newOp);
			}
			catch (Exception e)
			{
				LOG.Error("Problem polling for operations", e);
			}
		}
	}
}