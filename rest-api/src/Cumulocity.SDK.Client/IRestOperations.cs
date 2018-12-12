using System;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;

namespace Cumulocity.SDK.Client
{
    public interface IRestOperations
    {
        T Get<T>(string path, CumulocityMediaType mediaType, Type responseType);

        T Get<T>(string path, MediaType mediaType, Type responseType);

        //Response.Status getStatus(string path, CumulocityMediaType mediaType);

        //T postStream<T>(string path, CumulocityMediaType mediaType, InputStream content, Type<T> responseClass);

        //T postText<T>(string path, string content, Type<T> responseClass);

        //T putText<T>(string path, string content, Type<T> responseClass);

        //T putStream<T>(string path, string contentType, InputStream content, Type<T> responseClass);

        //T putStream<T>(string path, MediaType mediaType, InputStream content, Type<T> responseClass);

        //T postFile<T>(string path, T representation, sbyte[] bytes, Type<T> responseClass);

        //T put<T>(string path, MediaType mediaType, T representation);

        //Future postAsync<T>(string path, CumulocityMediaType mediaType, T representation);

        //Future putAsync<T>(string path, CumulocityMediaType mediaType, T representation);

        T PostWithId<T>(string path, CumulocityMediaType mediaType, T representation) where T : IBaseResourceRepresentationWithId;

        T Post<T>(string path, CumulocityMediaType mediaType, T representation) where T : IResourceRepresentation;

        void PostWithoutResponse<T>(string path, MediaType mediaType, T representation) where T : IResourceRepresentation;

        //Result post<Result, Param>(string path, CumulocityMediaType contentType, CumulocityMediaType accept, Param representation, Type<Result> clazz);

        T Put<T>(string path, CumulocityMediaType mediaType, T representation) where T : IBaseResourceRepresentationWithId;

        void Delete(string path);
    }
}