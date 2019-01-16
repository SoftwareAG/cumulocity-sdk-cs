using Cumulocity.SDK.Client.Rest.Representation.Cep;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Cep
{
	public class PagedCepModuleCollectionRepresentation<T> : CepModuleCollectionRepresentation, IPagedCollectionRepresentation<CepModuleRepresentation>
		where T : CepModuleCollectionRepresentation
	{
		private readonly IPagedCollectionResource<CepModuleRepresentation, T> collectionResource;

		public PagedCepModuleCollectionRepresentation(CepModuleCollectionRepresentation collection, IPagedCollectionResource<CepModuleRepresentation, T> collectionResource)
		{
			Modules = collection.Modules;
			PageStatistics = collection.PageStatistics;
			Self = collection.Self;
			Next = collection.Next;
			Prev = collection.Prev;
			this.collectionResource = collectionResource;
		}

		public IEnumerable<CepModuleRepresentation> allPages()
		{
			return new PagedCollectionIterable<CepModuleRepresentation, CepModuleCollectionRepresentation>(collectionResource, this);
		}

		public IEnumerable<CepModuleRepresentation> elements(int limit)
		{
			return new PagedCollectionIterable<CepModuleRepresentation, CepModuleCollectionRepresentation>(collectionResource, this, limit);
		}
	}
}