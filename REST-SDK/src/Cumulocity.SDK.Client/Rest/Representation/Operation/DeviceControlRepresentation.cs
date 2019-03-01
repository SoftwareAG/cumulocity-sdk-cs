namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
	public class DeviceControlRepresentation : AbstractExtensibleRepresentation
	{
		private OperationCollectionRepresentation operations;

		private string operationsByStatus;
		private string operationsByAgentId;
		private string operationsByAgentIdAndStatus;
		private string operationsByDeviceId;
		private string operationsByDeviceIdAndStatus;

		public virtual OperationCollectionRepresentation Operations
		{
			get
			{
				return operations;
			}
			set
			{
				this.operations = value;
			}
		}

		public virtual string OperationsByStatus
		{
			get
			{
				return operationsByStatus;
			}
			set
			{
				this.operationsByStatus = value;
			}
		}

		public virtual string OperationsByAgentId
		{
			get
			{
				return operationsByAgentId;
			}
			set
			{
				this.operationsByAgentId = value;
			}
		}

		public virtual string OperationsByAgentIdAndStatus
		{
			get
			{
				return operationsByAgentIdAndStatus;
			}
			set
			{
				this.operationsByAgentIdAndStatus = value;
			}
		}

		public virtual string OperationsByDeviceId
		{
			get
			{
				return operationsByDeviceId;
			}
			set
			{
				this.operationsByDeviceId = value;
			}
		}

		public virtual string OperationsByDeviceIdAndStatus
		{
			get
			{
				return operationsByDeviceIdAndStatus;
			}
			set
			{
				this.operationsByDeviceIdAndStatus = value;
			}
		}
	}
}