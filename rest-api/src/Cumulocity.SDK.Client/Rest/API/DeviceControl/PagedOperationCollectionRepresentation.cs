﻿using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

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

		public IEnumerable<OperationRepresentation> allPages()
		{
			return new PagedCollectionIterable<OperationRepresentation, OperationCollectionRepresentation>(collectionResource, this);
		}

		public  IEnumerable<OperationRepresentation> elements(int limit)
		{
			return new PagedCollectionIterable<OperationRepresentation, OperationCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}
