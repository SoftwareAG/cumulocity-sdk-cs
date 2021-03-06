using System;
using System.Collections;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation;

namespace Cumulocity.SDK.Client
{
public class PagedCollectionIterable<T, C> : IEnumerable<T>, IEnumerator<T> where C : BaseCollectionRepresentation<T>
{
	private readonly IPagedCollectionResource<T, C> _collectionResource;

	private C collection;

	private readonly int limit;

	private IEnumerator<T> iterator_Renamed;

	private int counter;

	public PagedCollectionIterable(IPagedCollectionResource<T,C> collectionResource, C collection): this(collectionResource, collection, 0) 
	{
	}

	public PagedCollectionIterable(IPagedCollectionResource<T,C> collectionResource, C collection, int limit) 
	{
		this._collectionResource = collectionResource;
		this.collection = collection;
		this.limit = limit;
		this.iterator_Renamed = collection.GetEnumerator();
	}

	public virtual IEnumerator<T> GetEnumerator()
	{
		return this;
	}

	public virtual bool reachedLimit()
	{
		return limit > 0 && counter >= limit;
	}

	public  void remove()
	{
		throw new System.NotSupportedException("Cannot remove from PagedCollectionIterable!");
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public bool MoveNext()
	{
		if (reachedLimit())
		{
			return false;
		}
		//TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
		if (iterator_Renamed.MoveNext())
		{
			counter++;
			return true;
		}
		if (collection.Next == null)
		{
			return false;
		}
		collection = _collectionResource.GetNextPage(collection);
		iterator_Renamed = collection.GetEnumerator();
		//TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
		return iterator_Renamed.MoveNext();
	}

	public void Reset()
	{
		throw new System.NotImplementedException();

	}

	object IEnumerator.Current => Current;

	public T Current // Implement Current.
	{
		get
		{
			if (counter == -1)
				throw new InvalidOperationException();
//			if (counter >= iterator_Renamed.Length)
//				throw new InvalidOperationException();
			return iterator_Renamed.Current;
		}
	}

	public void Dispose()
	{

	}
}
}