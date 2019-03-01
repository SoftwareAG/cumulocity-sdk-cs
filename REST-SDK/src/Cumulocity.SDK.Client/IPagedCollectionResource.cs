using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Representation;

namespace Cumulocity.SDK.Client
{
	public interface IPagedCollectionResource<T, out TC> where TC : BaseCollectionRepresentation<T>
	{
		/// <summary>
		/// The method returns the first page.
		/// </summary>
		TC GetFirstPage(params QueryParam[] queryParams);

		/// <summary>
		/// The method returns the first page.
		/// </summary>
		/// <param name="pageSize">                 - page size </param>
		/// <returns> BaseCollectionRepresentation type of BaseCollectionRepresentation. </returns>
		/// <exception cref="SDKException"> </exception>
		TC GetFirstPage(int pageSize, params QueryParam[] queryParams);

		/// <summary>
		/// The method returns the specified page number.
		/// </summary>
		/// <param name="collectionRepresentation"> It uses the BaseCollectionRepresentation.getSelf() URL to find the collection. </param>
		/// <param name="pageNumber">               - page number </param>
		/// <returns> BaseCollectionRepresentation type of BaseCollectionRepresentation. </returns>
		/// <exception cref="SDKException"> </exception>
		TC GetPage(BaseCollectionRepresentation<T> collectionRepresentation, int pageNumber);

		/// <summary>
		/// The method returns the specified page number.
		/// </summary>
		/// <param name="collectionRepresentation"> It uses the BaseCollectionRepresentation.getSelf() URL to find the collection. </param>
		/// <param name="pageNumber">               - page number </param>
		/// <param name="pageSize">                 - page size </param>
		/// <returns> BaseCollectionRepresentation type of BaseCollectionRepresentation. </returns>
		/// <exception cref="SDKException"> </exception>
		TC GetPage(BaseCollectionRepresentation<T> collectionRepresentation, int pageNumber, int pageSize);

		/// <summary>
		/// The method returns the next page from the collection.
		/// </summary>
		/// <param name="collectionRepresentation"> It uses the BaseCollectionRepresentation.getNext() URL to find the collection. </param>
		/// <returns> collectionRepresentation type of BaseCollectionRepresentation. </returns>
		/// <exception cref="SDKException"> </exception>
		TC GetNextPage(BaseCollectionRepresentation<T> collectionRepresentation);

		/// <summary>
		/// This method returns the previous page in the collection.
		/// </summary>
		/// <param name="collectionRepresentation"> - It uses the BaseCollectionRepresentation.getPrevious() URL to find the collection. </param>
		/// <returns> BaseCollectionRepresentation type of BaseCollectionRepresentation. </returns>
		/// <exception cref="SDKException"> </exception>
		TC GetPreviousPage(BaseCollectionRepresentation<T> collectionRepresentation);
	}

	public static class PagedCollectionResource_Fields
	{
		public const string PAGE_SIZE_KEY = "pageSize";
		public const string PAGE_NUMBER_KEY = "currentPage";
	}
}