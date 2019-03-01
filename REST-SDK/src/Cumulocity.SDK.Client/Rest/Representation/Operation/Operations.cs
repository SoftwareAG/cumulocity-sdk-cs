using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Operation.OperationsEnhanced;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
	public class Operations
	{

		/// <summary>
		///  Returns operation that triggers a new measurement to be made.<para>
		///  Does not define the target device ID. Clients can interpret this as meaning measurements from all
		///  devices should be read, or they can use this as a base instance from which they can add device IDs.
		/// </para>
		/// </summary>
		public static OperationRepresentation createNewMeasurementOperation()
		{
			OperationRepresentation newOp = new OperationRepresentation();
			newOp.Set(new NewMeasurement());
			return newOp;
		}

		public static OperationRepresentation asOperation(GId id)
		{
			OperationRepresentation operation = new OperationRepresentation
			{
				Id = id
			};
			return operation;
		}

		public static OperationRepresentation asExecutingOperation(GId id)
		{
			return asOperationWithStatus(id, OperationStatus.EXECUTING);
		}

		public static OperationRepresentation asSuccessOperation(GId id)
		{
			return asOperationWithStatus(id, OperationStatus.SUCCESSFUL);
		}

		public static OperationRepresentation asOperationWithStatus(GId id, OperationStatus status)
		{
			OperationRepresentation operation = new OperationRepresentation
			{
				Id = id,
				Status = status.ToString()
			};
			return operation;
		}

		public static OperationRepresentation asFailedOperation(GId id, string failureCause)
		{
			OperationRepresentation operation = new OperationRepresentation
			{
				Id = id,
				Status = OperationStatus.FAILED.ToString(),
				FailureReason = failureCause
			};
			return operation;
		}
	}
}
