using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client
{
	public interface IRestOperations
	{
		T Get<T>(string path, CumulocityMediaType mediaType, Type responseType);

		T Get<T>(string path, MediaType mediaType, Type responseType);

		T postStream<T>(string path, CumulocityMediaType mediaType, Stream content, Type responseClass);

		T postText<T>(string path, string content, Type responseClass);

		T putText<T>(string path, string content, Type responseClass);

		T putStream<T>(string path, MediaType mediaType, Stream content, Type responseClass);

		T PostWithId<T>(string path, CumulocityMediaType mediaType, T representation) where T : IBaseResourceRepresentationWithId;

		T Post<T>(string path, CumulocityMediaType mediaType, T representation) where T : IResourceRepresentation;

		void PostWithoutResponse<T>(string path, MediaType mediaType, T representation) where T : IResourceRepresentation;

		T putStream<T>(string path, string contentType, Stream content, Type responseClass);

		T postFile<T>(string path, ManagedObjectRepresentation container, byte[] bytes, T representation);

		//T put<T>(string path, MediaType mediaType, T representation);

		//Future postAsync<T>(string path, CumulocityMediaType mediaType, T representation);

		//Future putAsync<T>(string path, CumulocityMediaType mediaType, T representation);

		//Result post<Result, Param>(string path, CumulocityMediaType contentType, CumulocityMediaType accept, Param representation, Type<Result> clazz);

		//Response.Status getStatus(string path, CumulocityMediaType mediaType);

		Task<T> PutAsync<T>(string path, CumulocityMediaType mediaType, T representation) where T : IResourceRepresentation;

		void Delete(string path);
	}
}