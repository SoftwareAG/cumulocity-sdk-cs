using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
	public interface IOperationProcessor
	{
		/// <summary>
		/// Processes operation
		/// </summary>
		/// <param name="operation"> operation to Process </param>
		/// <returns> true - if processing was successful, false - otherwise </returns>
		bool Process(OperationRepresentation operation);
	}
}