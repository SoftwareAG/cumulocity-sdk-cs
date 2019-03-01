using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
	[JsonObject]
	public class OperationCollectionRepresentation : BaseCollectionRepresentation<OperationRepresentation>
	{
		private IList<OperationRepresentation> operations;

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

		public override IEnumerator<OperationRepresentation> GetEnumerator()
		{
			return operations.GetEnumerator();
		}
	}
}