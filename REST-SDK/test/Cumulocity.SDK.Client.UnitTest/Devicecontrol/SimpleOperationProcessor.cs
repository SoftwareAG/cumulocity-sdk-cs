using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace Cumulocity.SDK.Client.UnitTest.Devicecontrol
{
	public class SimpleOperationProcessor : IOperationProcessor
	{
		private IList<OperationRepresentation> operations = new List<OperationRepresentation>();

		public bool Process(OperationRepresentation operation)
		{
			operations.Add(operation);
			return true;
		}

		public virtual IList<OperationRepresentation> Operations
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


	}
}
