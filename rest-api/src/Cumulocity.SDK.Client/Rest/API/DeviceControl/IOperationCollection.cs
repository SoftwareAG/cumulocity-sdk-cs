using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	public interface IOperationCollection : IPagedCollectionResource<OperationRepresentation, PagedOperationCollectionRepresentation<OperationCollectionRepresentation>>
	{

	}
}
