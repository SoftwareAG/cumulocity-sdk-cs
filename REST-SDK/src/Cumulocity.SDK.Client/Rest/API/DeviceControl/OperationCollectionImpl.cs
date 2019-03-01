using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	public class OperationCollectionImpl : PagedCollectionResourceImpl<OperationRepresentation, OperationCollectionRepresentation, PagedOperationCollectionRepresentation<OperationCollectionRepresentation>>, IOperationCollection
	{
		public OperationCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType
		{
			get
			{
				return DeviceControlMediaType.OPERATION_COLLECTION;
			}
		}

		protected internal override Type ResponseClassProp => typeof(OperationCollectionRepresentation);

		protected internal override PagedOperationCollectionRepresentation<OperationCollectionRepresentation> wrap(OperationCollectionRepresentation collection)
		{
			return new PagedOperationCollectionRepresentation<OperationCollectionRepresentation>(collection, this);
		}
	}
}