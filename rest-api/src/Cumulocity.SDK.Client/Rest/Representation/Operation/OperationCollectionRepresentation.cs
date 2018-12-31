using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
	[JsonObject]
	public class OperationCollectionRepresentation : BaseCollectionRepresentation<OperationRepresentation>
	{
		private IList<OperationRepresentation> operations;

		//ORIGINAL LINE: @JSONTypeHint(OperationRepresentation.class) public List<OperationRepresentation> getOperations()
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<OperationRepresentation> iterator()
		public IEnumerator<OperationRepresentation> iterator()
		{
			return operations.GetEnumerator();
		}

		public override IEnumerator<OperationRepresentation> GetEnumerator()
		{
			return operations.GetEnumerator();
		}
	}
}