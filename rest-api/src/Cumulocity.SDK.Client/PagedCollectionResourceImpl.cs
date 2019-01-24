using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Representation;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client
{
	public abstract class PagedCollectionResourceImpl<T, C, I> : IPagedCollectionResource<T, I>
		where C : BaseCollectionRepresentation<T> where I : C
	{
		protected internal readonly RestConnector restConnector;
		private readonly string url;

		protected internal int pageSize = 5;

		//private static readonly Logger LOG = LoggerFactory.getLogger(typeof(PagedCollectionResourceImpl));
		private readonly UrlProcessor urlProcessor = new UrlProcessor();

		public PagedCollectionResourceImpl(RestConnector restConnector, string url, int pageSize)
		{
			this.restConnector = restConnector;
			this.url = url;
			this.pageSize = pageSize;
		}

		protected internal abstract CumulocityMediaType MediaType { get; }

		protected internal abstract Type ResponseClassProp { get; }

		public virtual I getPage(BaseCollectionRepresentation<T> collectionRepresentation, int pageNumber)
		{
			if (collectionRepresentation == null)
				throw new SDKException("Unable to determin the Resource URL. ");
			return getPage(collectionRepresentation, pageNumber,
				collectionRepresentation.PageStatistics == null ? 5 : collectionRepresentation.PageStatistics.PageSize);
		}

		public virtual I getPage(BaseCollectionRepresentation<T> collectionRepresentation, int pageNumber, int pageSize)
		{
			var pageUrl = getPageUrl(collectionRepresentation, pageNumber, pageSize);
			return getCollection(pageUrl);
		}

		public virtual I getNextPage(BaseCollectionRepresentation<T> collectionRepresentation)
		{
			if (collectionRepresentation == null)
				throw new SDKException("Unable to determin the Resource URL. ");
			return collectionRepresentation.Next == null ? default(I) : getCollection(collectionRepresentation.Next);
		}

		public virtual I getPreviousPage(BaseCollectionRepresentation<T> collectionRepresentation)
		{
			if (collectionRepresentation == null)
				throw new SDKException("Unable to determin the Resource URL. ");
			return collectionRepresentation.Prev == null ? default(I) : getCollection(collectionRepresentation.Prev);
		}

		public virtual I get(params QueryParam[] queryParams)
		{
			return get(pageSize, queryParams);
		}

		public virtual I get(int pageSize, params QueryParam[] queryParams)
		{
			var @params = prepareGetParams(pageSize);
			var arr = queryParams;
			var len = queryParams.Length;

			for (var i = 0; i < len; ++i)
			{
				var param = arr[i];
				@params[param.Key.Name] = param.Value;
			}

			return get(@params);
		}

		internal virtual string getPageUrl(BaseCollectionRepresentation<T> collectionRepresentation, int pageNumber,
			int pageSize)
		{
			if (collectionRepresentation != null && collectionRepresentation.Self != null)
			{
				IDictionary<string, string> @params = new Dictionary<string, string>();
				@params["pageSize"] = (pageSize < 1 ? 5 : pageSize).ToString();
				@params["currentPage"] = pageNumber.ToString();
				var url = urlProcessor.replaceOrAddQueryParam(collectionRepresentation.Self, @params);
				url = urlProcessor.removeQueryParam(url, new List<string> { "startkey", "startkey_docid" });
				//LOG.debug(" URL : " + url);
				return url;
			}

			throw new SDKException("Unable to determin the Resource URL. ");
		}

		protected internal virtual I getCollection(string url)
		{
			return wrap(restConnector.Get<C>(url, MediaType, ResponseClassProp));
		}

		protected internal abstract I wrap(C var1);

		protected internal virtual I get(IDictionary<string, string> @params)
		{
			var urlToCall = urlProcessor.replaceOrAddQueryParam(url, @params);
			return wrap(restConnector.Get<C>(urlToCall, MediaType, ResponseClassProp));
		}

		protected internal virtual IDictionary<string, string> prepareGetParams(int pageSize)
		{
			var result = new Dictionary<string, string>();
			result["pageSize"] = (pageSize < 1 ? 5 : pageSize).ToString();
			return result;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PagedCollectionResourceImpl<T, C, I>))
				return false;
			try
			{
				var another = (PagedCollectionResourceImpl<T, C, I>)obj;
				return MediaType.Equals(another.MediaType) && ResponseClassProp.Equals(another.ResponseClassProp) &&
					   pageSize == another.pageSize && url.Equals(another.url);
			}
			catch (NullReferenceException)
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return MediaType.GetHashCode() ^ ResponseClassProp.GetHashCode() ^ url.GetHashCode() ^ pageSize;
		}
	}
}