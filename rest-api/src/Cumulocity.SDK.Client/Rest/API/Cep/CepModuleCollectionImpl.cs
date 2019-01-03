using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Cep;
using Cumulocity.SDK.Client.Rest.Representation.Event;

namespace Cumulocity.SDK.Client.Rest.API.Cep
{
	public class CepModuleCollectionImpl : PagedCollectionResourceImpl<CepModuleRepresentation, CepModuleCollectionRepresentation, PagedCepModuleCollectionRepresentation<CepModuleCollectionRepresentation>>,
		ICepModuleCollection
	{
		public CepModuleCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType
		{
			get
			{
				return CepMediaType.CEP_MODULE_COLLECTION;
			}
		}
		protected internal override Type ResponseClassProp => typeof(CepModuleCollectionRepresentation);
		protected internal override PagedCepModuleCollectionRepresentation<CepModuleCollectionRepresentation> wrap(CepModuleCollectionRepresentation collection)
		{
			return new PagedCepModuleCollectionRepresentation<CepModuleCollectionRepresentation>(collection, this);
		}
	}
}
