using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	public class PagedOperationCollectionRepresentation<T> : OperationCollectionRepresentation, IPagedCollectionRepresentation<OperationRepresentation>
		where T : OperationCollectionRepresentation
	{
		private readonly IPagedCollectionResource<OperationRepresentation, T> collectionResource;

		public PagedOperationCollectionRepresentation(OperationCollectionRepresentation collection, IPagedCollectionResource<OperationRepresentation, T> collectionResource)
		{
			Operations = collection.Operations;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<OperationRepresentation> AllPages()
		{
			return new PagedCollectionIterable<OperationRepresentation, OperationCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<OperationRepresentation> Elements(int limit)
		{
			return new PagedCollectionIterable<OperationRepresentation, OperationCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}