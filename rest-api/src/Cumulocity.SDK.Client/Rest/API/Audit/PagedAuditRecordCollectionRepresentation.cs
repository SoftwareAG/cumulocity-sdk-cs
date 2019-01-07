﻿using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Audit;

namespace Cumulocity.SDK.Client.Rest.API.Audit
{
	public class PagedAuditCollectionRepresentation<T> : AuditRecordCollectionRepresentation, IPagedCollectionRepresentation<AuditRecordRepresentation>
	      where T : AuditRecordCollectionRepresentation
	{

		//ORIGINAL LINE: private final PagedCollectionResource<AuditRecordRepresentation, ? extends AuditRecordCollectionRepresentation> collectionResource;
		private readonly IPagedCollectionResource<AuditRecordRepresentation, T> collectionResource;

		public PagedAuditCollectionRepresentation(AuditRecordCollectionRepresentation collection, IPagedCollectionResource<AuditRecordRepresentation, T> collectionResource) 
		{
			AuditRecords = collection.AuditRecords;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<AuditRecordRepresentation> allPages()
		{
			return new PagedCollectionIterable<AuditRecordRepresentation, AuditRecordCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<AuditRecordRepresentation> elements(int limit)
		{
			return new PagedCollectionIterable<AuditRecordRepresentation, AuditRecordCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}
