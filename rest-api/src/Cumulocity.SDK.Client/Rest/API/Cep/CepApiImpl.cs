using Cumulocity.SDK.Client.Rest.API.Cep.Notification;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Cep;
using System;
using System.IO;

namespace Cumulocity.SDK.Client.Rest.API.Cep
{
	public class CepApiImpl : ICepApi
	{
		private readonly PlatformParameters platformParameters;

		private readonly RestConnector restConnector;

		private readonly string url;

		private readonly int pageSize;

		public CepApiImpl(PlatformParameters platformParameters, RestConnector restConnector, int pageSize)
		{
			this.platformParameters = platformParameters;
			this.restConnector = restConnector;
			this.pageSize = pageSize;
			this.url = platformParameters.Host + "cep";
		}

		public CepCustomNotificationsSubscriber CustomNotificationsSubscriber => new CepCustomNotificationsSubscriber(platformParameters);

		public CepModuleRepresentation create(Stream content)
		{
			return restConnector.postStream<CepModuleRepresentation>(cepModulesUrl(), CepMediaType.CEP_MODULE, content, typeof(CepModuleRepresentation));
		}

		public CepModuleRepresentation create(string content)
		{
			return restConnector.postText<CepModuleRepresentation>(cepModulesUrl(), content, typeof(CepModuleRepresentation));
		}

		private string cepModulesUrl()
		{
			return url + "/modules";
		}

		public CepModuleRepresentation update(string id, Stream content)
		{
			return restConnector.putStream<CepModuleRepresentation>(cepModuleUrl(id), CepMediaType.CEP_MODULE, content, typeof(CepModuleRepresentation));
		}

		public CepModuleRepresentation update(string id, string content)
		{
			return restConnector.putText<CepModuleRepresentation>(cepModuleUrl(id), content, typeof(CepModuleRepresentation));
		}

		public CepModuleRepresentation update(CepModuleRepresentation module)
		{
			return restConnector.PutWithoutId<CepModuleRepresentation>(cepModuleUrl(module.Id), CepMediaType.CEP_MODULE, module);
		}

		private string cepModuleUrl(string id)
		{
			return $"{cepModulesUrl()}/{id}";
		}

		public void delete(CepModuleRepresentation module)
		{
			delete(module.Id);
		}

		public void delete(string id)
		{
			restConnector.Delete(cepModuleUrl(id));
		}

		public T health<T>(Type clazz)
		{
			return restConnector.Get<T>(url + "/health", CumulocityMediaType.APPLICATION_JSON_TYPE, clazz);
		}

		public CepModuleRepresentation get(string id)
		{
			return restConnector.Get<CepModuleRepresentation>(cepModuleUrl(id), CepMediaType.CEP_MODULE, typeof(CepModuleRepresentation));
		}

		public string getText(string id)
		{
			return restConnector.Get<string>(cepModuleUrl(id), CumulocityMediaType.TEXT_PLAIN_TYPE, typeof(string));
		}

		public ICepModuleCollection Modules
		{
			get
			{
				return new CepModuleCollectionImpl(restConnector, cepModulesUrl(), pageSize);
			}
		}
	}
}