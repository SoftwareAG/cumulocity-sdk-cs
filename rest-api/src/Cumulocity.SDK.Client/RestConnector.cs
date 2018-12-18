using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cumulocity.SDK.Client
{
    //http://www.mkyong.com/webservices/jax-rs/restful-java-client-with-jersey-client/
    public class RestConnector : IRestOperations
    {
        public const string XCumulocityApplicationKey = "X-Cumulocity-Application-Key";

        public const string XCumulocityRequestOrigin = "X-Cumulocity-Request-Origin";

        private const string TfaTokenHeader = "TFAToken";

        //private static readonly object[] ProvidersClasses = new object[] {typeof(CumulocityJSONMessageBodyWriter), typeof(CumulocityJSONMessageBodyReader), typeof(ErrorMessageRepresentationReader)};

        private const int ReadTimeoutInMillis = 180000;

        private static CumulocityHttpClient client; //= new CumulocityHttpClient();


        public RestConnector(PlatformParameters platformParameters, ResponseParser responseParser) : this(
            platformParameters, responseParser, createClient(platformParameters))
        {
        }

        protected internal RestConnector(PlatformParameters platformParameters, ResponseParser responseParser,
            CumulocityHttpClient client)
        {
            PlatformParameters = platformParameters;
            ResponseParser = responseParser;
            Client = client;
        }

        public virtual PlatformParameters PlatformParameters { get; }

        public virtual CumulocityHttpClient Client { get; }

        public virtual ResponseParser ResponseParser { get; }


        public T Get<T>(string path, CumulocityMediaType mediaType, Type responseType)
        {
            var response = getClientResponse(path, mediaType);
            return ResponseParser.parse<T>(response.Result, (int) HttpStatusCode.OK, responseType);
        }

        public T Get<T>(string path, MediaType mediaType, Type responseType)
        {
            throw new NotImplementedException();
        }

        public T Post<T>(string path, CumulocityMediaType mediaType, T representation)
            where T : IResourceRepresentation
        {
            var response = httpPost(path, mediaType, mediaType, representation);
            
            return ResponseParser.parse<T>(response.Result, (int) HttpStatusCode.Created,typeof(T));
        }

	    public async Task<T> PostAsync<T>(string path, CumulocityMediaType mediaType, T representation)
		    where T : IResourceRepresentation
	    {
		    var response = await httpPost(path, mediaType, mediaType, representation);
		    return await ResponseParser.ParseAsync<T>(response, (int)HttpStatusCode.Created, typeof(T));
	    }

		public T PostWithId<T>(string path, CumulocityMediaType mediaType, T representation)
            where T : IBaseResourceRepresentationWithId
        {
            var response = httpPost(path, mediaType, mediaType, representation);
            return parseResponseWithId(representation, response.Result, (int) HttpStatusCode.Created);
        }

        public void PostWithoutResponse<T>(string path, MediaType mediaType, T representation)
            where T : IResourceRepresentation
        {
            throw new NotImplementedException();
        }

        public T Put<T>(string path, CumulocityMediaType mediaType, T representation)
            where T : IBaseResourceRepresentationWithId
        {
            var response = this.putClientResponse<T>(path, mediaType, representation);
            return parseResponseWithId(representation, response.Result, (int) HttpStatusCode.OK);
        }

	    public T PutWithoutId<T>(string path, CumulocityMediaType mediaType, T representation)		    
	    {
		    var response = this.putClientResponse<T>(path, mediaType, representation);
		    return ResponseParser.parse<T>(response.Result, (int)HttpStatusCode.OK, representation.GetType());
	    }

		public void Delete(string path)
        {
            var response = this.deleteClientResponse(path);
            this.ResponseParser.checkStatus(response.Result, new int[]{ (int)HttpStatusCode.NoContent});
        }

        private Task<HttpResponseMessage> getClientResponse(string path, CumulocityMediaType mediaType)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(path)
            };
            request.Headers.TryAddWithoutValidation("Accept", mediaType.TypeString);

            request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
            request.AddTfaHeader(this.PlatformParameters.getTfaToken());
            request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);
            
            return client.SendAsync(request);
        }

        private Task<HttpResponseMessage> httpPost<T>(string path, CumulocityMediaType contentType,
            CumulocityMediaType accept, T representation)
        {
            var json = JsonConvert.SerializeObject(representation,
                new JsonSerializerSettings 
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver() 
                });
            var stringContent = new StringContent(json, Encoding.UTF8).Replace(contentType.TypeString);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(path),
                Content = stringContent
            };
            
            if (PlatformParameters.requireResponseBody())
                request.Headers.TryAddWithoutValidation("Accept", accept.TypeString);
            
            request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
            request.AddTfaHeader(this.PlatformParameters.getTfaToken());
            request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);
            

            return client.SendAsync(request);
        }
        
        private Task<HttpResponseMessage> deleteClientResponse(string path)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(path)
            };

            request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
            request.AddTfaHeader(this.PlatformParameters.getTfaToken());
            request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);
            
            return client.SendAsync(request);
        }
        
        private Task<HttpResponseMessage> putClientResponse<T>(string path, CumulocityMediaType mediaType, T representation)
        {
            var json = JsonConvert.SerializeObject(representation,
                new JsonSerializerSettings 
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver() 
                });
            var stringContent = new StringContent(json, Encoding.UTF8).Replace(mediaType.TypeString);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(path),
                Content = stringContent
            };
            
            if (PlatformParameters.requireResponseBody())
                request.Headers.TryAddWithoutValidation("Accept", mediaType.TypeString);

            request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
            request.AddTfaHeader(this.PlatformParameters.getTfaToken());
            request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);
            
            return client.SendAsync(request);
        }

        //ORIGINAL LINE: private <T extends ResourceRepresentationWithId> T parseResponseWithId(T representation, ClientResponse response, int responseCode) throws SDKException
        private T parseResponseWithId<T>(T representation, HttpResponseMessage response, int responseCode)
            where T : IBaseResourceRepresentationWithId
        {
            //T repFromPlatform = (IResourceRepresentationWithId) ResponseParser.parse<T>(response, responseCode, representation.GetType());
            var repFromPlatform = ResponseParser.parse<T>(response, responseCode, representation.GetType());
            var repToReturn = isDefined(repFromPlatform) ? repFromPlatform : representation;
            if (response.Headers.Location != null) repToReturn.Id = ResponseParser.parseIdFromLocation(response);
            return repToReturn;
        }

        private bool isDefined<T>(T repFromPlatform) where T : IBaseResourceRepresentationWithId
        {
            return repFromPlatform != null;
        }

        public static CumulocityHttpClient createClient(PlatformParameters platformParameters)
        {
            var config = new HttpClientHandler();
//            if (isProxyRequired(platformParameters))
            //            {
            //                config.Properties.put("com.sun.jersey.impl.client.httpclient.proxyURI", "http://" + platformParameters.ProxyHost + ":" + platformParameters.ProxyPort);
            //                if (isProxyAuthenticationRequired(platformParameters))
            //                {
            //                    config.State.setProxyCredentials((string)null, platformParameters.ProxyHost, platformParameters.ProxyPort, platformParameters.ProxyUserId, platformParameters.ProxyPassword);
            //                }
            //            }

            //registerClasses(config);
            //config.Properties.put("com.sun.jersey.client.property.readTimeout", 180000);
            client = new CumulocityHttpClient(createDefaultClientHander(config), true);
            client.PlatformParameters = platformParameters;
            //client.Fo FollowRedirects = true;
            //client.addFilter(new HTTPBasicAuthFilter(platformParameters.Principal, platformParameters.Password));

            client.addBasicAuthFilter(platformParameters.Principal, platformParameters.Password);

            return client;
        }

        private static HttpClientHandler createDefaultClientHander(HttpClientHandler config)
        {
            var credentials = new NetworkCredential("user", "pass");
            var handler = new HttpClientHandler {Credentials = credentials}; 

            return handler;
        }
    }
}