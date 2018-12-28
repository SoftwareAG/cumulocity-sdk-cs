using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
	public interface IOperationProcessor
	{
		/// <summary>
		/// Processes operation
		/// </summary>
		/// <param name="operation"> operation to process </param>
		/// <returns> true - if processing was successful, false - otherwise </returns>
		bool process(OperationRepresentation operation);

	}
}
