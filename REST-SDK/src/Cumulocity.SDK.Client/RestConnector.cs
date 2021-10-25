using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest.Model.Buffering;

namespace Cumulocity.SDK.Client
{
	//http://www.mkyong.com/webservices/jax-rs/restful-java-client-with-jersey-client/
	public class RestConnector : IRestOperations
	{
		public const string XCumulocityApplicationKey = "X-Cumulocity-Application-Key";

		public const string XCumulocityRequestOrigin = "X-Cumulocity-Request-Origin";

		private const string TfaTokenHeader = "TFAToken";

		private const int ReadTimeoutInMillis = 180000;

		// Max number of connections per server endpoint
		private const int MaxConnectionsPerRoute = 50;

		private CumulocityHttpClient client;

		public RestConnector(PlatformParameters platformParameters, ResponseParser responseParser) : this(
			platformParameters, responseParser, null)
		{
		}

		protected internal RestConnector(PlatformParameters platformParameters, ResponseParser responseParser,
			CumulocityHttpClient client)
		{
			PlatformParameters = platformParameters;
			ResponseParser = responseParser;
			Client = createClient(platformParameters);
		}

		public virtual PlatformParameters PlatformParameters { get; }

		public CumulocityHttpClient Client { get; }

		public virtual ResponseParser ResponseParser { get; }

		public virtual T Get<T>(string path, CumulocityMediaType mediaType, Type responseType)
		{
			var response = getClientResponse(path, mediaType);
			return ResponseParser.parse<T>(response.Result, responseType, (int)HttpStatusCode.OK);
		}

		public T Get<T>(string path, MediaType mediaType, Type responseType)
		{
			var response = getClientResponse(path, mediaType);
			return ResponseParser.parse<T>(response.Result, responseType, (int)HttpStatusCode.OK);
		}

		public async Task<Stream> GetStream(string path, MediaType aPPLICATION_OCTET_STREAM_TYPE)
		{
			path = Client.resolvePath(path);
			return await client.GetStreamAsync(path);
		}

		public T postStream<T>(string path, CumulocityMediaType mediaType, Stream content, Type representation)
		{
			var response = httpStreamPost(path, mediaType, mediaType, content, representation);
			return ResponseParser.parse<T>(response.Result, representation, (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		public T postText<T>(string path, string content, Type representation)
		{
			var response = httpPostText<T>(path, content, representation);
			return ResponseParser.parse<T>(response.Result, representation, (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		public virtual T Post<T>(string path, CumulocityMediaType mediaType, T representation)
			where T : IResourceRepresentation
		{
			try
            {
				var response = httpPost(path, mediaType, mediaType, representation);

				if (!PlatformParameters.requireResponseBody())
				{
					var repFromPlatform = ResponseParser.parse<T>(response.Result, typeof(T), (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
					return repFromPlatform != null ? repFromPlatform : representation;
				}

				//if (((IList)typeof(T).GetInterfaces()).Contains(typeof(IBaseResourceRepresentationWithId)))
				//{
				//	return (T)parseResponseWithId((IBaseResourceRepresentationWithId)representation, response.Result, (int)HttpStatusCode.Created);
				//}

				return ResponseParser.parse<T>(response.Result, typeof(T), (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
			}
			catch(UriFormatException ex) { }
			return default(T);
		}

		// C# methods implemented from interfaces are not virtual by default. Cant be used by used in Moq verify or setup. 
		public virtual T Post<T, S>(String path, CumulocityMediaType contentType, CumulocityMediaType accept, S representation, T clazz)
			where T : IResourceRepresentation where S: IResourceRepresentation
        {
			var response = httpPost(path, contentType, accept, representation);
			return ResponseParser.parse<T>(response.Result, typeof(T), (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		public async Task<T> PostAsync<T>(string path, CumulocityMediaType mediaType, T representation)
			where T : IResourceRepresentation
		{
			//var response = await httpPost(path, mediaType, mediaType, representation);
			//return await ResponseParser.ParseAsync<T>(response, (int)HttpStatusCode.Created, typeof(T));
			return await sendAsyncRequest(HttpMethod.Post.Method, path, mediaType, representation);
		}

		public T PostWithId<T>(string path, CumulocityMediaType mediaType, T representation)
			where T : IBaseResourceRepresentationWithId
		{
			var response = httpPost(path, mediaType, mediaType, representation);
			return parseResponseWithId(representation, response.Result, (int)HttpStatusCode.Created);
		}

		public void PostWithoutResponse<T>(string path, MediaType mediaType, T representation)
			where T : IResourceRepresentation
		{
			var response = httpPost(path, mediaType, representation);
			ResponseParser.checkStatus(response.Result, (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}
		public T Put<T>(string path, CumulocityMediaType mediaType, T representation)
			where T : IBaseResourceRepresentationWithId
		{
			var response = this.putClientResponse<T>(path, mediaType, representation);
			return parseResponseWithId(representation, response.Result, (int)HttpStatusCode.OK);
		}
		public async Task<T> PutAsync<T>(string path, CumulocityMediaType mediaType, T representation)
			where T : IResourceRepresentation
		{
			return await sendAsyncRequest(HttpMethod.Put.Method, path, mediaType, representation);
			//return await Task.FromResult(default(T));
			//var response = this.putClientResponse<T>(path, mediaType, representation);
			//return parseResponseWithId(representation, response.Result, (int)HttpStatusCode.OK);
		}
		private async Task<T> sendAsyncRequest<T>(String method, String path, CumulocityMediaType mediaType,T representation)
			where T : IResourceRepresentation
		{
			path = Client.resolvePath(path);
			var bufferRequestService = PlatformParameters.BufferRequestService;
			var request = BufferedRequest.create(method, path, mediaType, representation);
			return (T) await bufferRequestService.Create(request);
		}

		public T PutWithoutId<T>(string path, CumulocityMediaType mediaType, T representation)
		{
			var response = this.putClientResponse<T>(path, mediaType, representation);
			return ResponseParser.parse<T>(response.Result, representation.GetType(), (int)HttpStatusCode.OK);
		}

		public virtual void Delete(string path)
		{
			var response = this.deleteClientResponse(path);
			this.ResponseParser.checkStatus(response.Result, new int[] { (int)HttpStatusCode.NoContent });
		}

		private Task<HttpResponseMessage> getClientResponse(string path, MediaType mediaType)
		{
			path = Client.resolvePath(path);
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(path)
			};
			request.Headers.TryAddWithoutValidation("Accept", mediaType.Subtype);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> getClientResponse(string path, CumulocityMediaType mediaType)
		{
			path = Client.resolvePath(path);
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(path)
			};
			request.Headers.TryAddWithoutValidation("Accept", mediaType.TypeString);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpStreamPost<T>(string path, CumulocityMediaType contentType,
			CumulocityMediaType accept, Stream content, T representation)
		{
			path = Client.resolvePath(path);
			MultipartFormDataContent multipartContent = new MultipartFormDataContent();
			ByteArrayContent byteContent;

			using (MemoryStream ms = new MemoryStream())
			{
				content.CopyTo(ms);
				byteContent = new ByteArrayContent(ms.ToArray());
			}

			multipartContent.Add(byteContent);

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(path),
				Content = multipartContent
			};

			if (PlatformParameters.requireResponseBody())
				request.Headers.TryAddWithoutValidation("Accept", accept.TypeString);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpPostText<T>(string path, String content, Type representation)
		{
			path = Client.resolvePath(path);
			var stringContent = new StringContent(content, Encoding.UTF8).Replace(MediaType.TEXT_PLAIN);

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(path),
				Content = stringContent
			};

			if (PlatformParameters.requireResponseBody())
				request.Headers.TryAddWithoutValidation("Accept", MediaType.APPLICATION_JSON);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpPutText<T>(string path, String content, Type representation)
		{
			path = Client.resolvePath(path);
			var stringContent = new StringContent(content, Encoding.UTF8).Replace(MediaType.TEXT_PLAIN);

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri(path),
				Content = stringContent
			};

			if (PlatformParameters.requireResponseBody())
				request.Headers.TryAddWithoutValidation("Accept", MediaType.APPLICATION_JSON);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpPutStream<T>(string path, Stream content, Type representation)
		{
			path = Client.resolvePath(path);
			var streamContent = new StreamContent(content);

			MultipartFormDataContent multipartContent = new MultipartFormDataContent();
			multipartContent.Add(streamContent, "file", "fileName");

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri(path),
				Content = multipartContent
			};

			if (PlatformParameters.requireResponseBody())
				request.Headers.TryAddWithoutValidation("Accept", MediaType.APPLICATION_JSON);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpPost<T>(string path, MediaType contentType,
			 T representation)
		{
			path = Client.resolvePath(path);
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

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpPost<T>(string path, CumulocityMediaType contentType,
			CumulocityMediaType accept, T representation)
		{
			try
            {
				path = Client.resolvePath(path);
				var json = JsonConvert.SerializeObject(representation,
					new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver(),
						Formatting = Formatting.Indented
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
				request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
				request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

				return client.SendAsync(request);
			}
			catch (UriFormatException ex)
            {
				throw new UriFormatException("Not a valid Uri");
            }
			return null;
		}

		private Task<HttpResponseMessage> deleteClientResponse(string path)
		{
			try
            {
				path = Client.resolvePath(path);
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Delete,
					RequestUri = new Uri(path)
				};

				request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
				request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
				request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

				return client.SendAsync(request);
			}
			catch(UriFormatException ex)
            {
            }
			return null;
		}

		private Task<HttpResponseMessage> putClientResponse<T>(string path, CumulocityMediaType mediaType, T representation)
		{
			path = Client.resolvePath(path);
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
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}

		//ORIGINAL LINE: private <T extends ResourceRepresentationWithId> T parseResponseWithId(T representation, ClientResponse response, int responseCode) throws SDKException
		private T parseResponseWithId<T>(T representation, HttpResponseMessage response, int responseCode)
			where T : IBaseResourceRepresentationWithId
		{
			var repFromPlatform = ResponseParser.parse<T>(response, representation.GetType(), responseCode);
			var repToReturn = isDefined(repFromPlatform) ? repFromPlatform : representation;
			if (response.Headers.Location != null) repToReturn.Id = ResponseParser.parseIdFromLocation(response);
			return repToReturn;
		}

		private bool isDefined<T>(T repFromPlatform) where T : IBaseResourceRepresentationWithId
		{
			return repFromPlatform != null;
		}

		public CumulocityHttpClient createClient(PlatformParameters platformParameters)
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

			client.addBasicAuthFilter(platformParameters.Principal, platformParameters.Password);

			return client;
		}

		private HttpClientHandler createDefaultClientHander(HttpClientHandler config)
		{
			var credentials = new NetworkCredential("user", "pass");
			var handler = new HttpClientHandler { Credentials = credentials, MaxConnectionsPerServer = MaxConnectionsPerRoute };

			return handler;
		}

		public T putText<T>(string path, string content, Type representation)
		{
			var response = httpPutText<T>(path, content, representation);
			return ResponseParser.parse<T>(response.Result, representation, (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		public T putStream<T>(string path, string contentType, Stream content, Type representation)
		{
			var response = httpPutStream<T>(path, content, representation);
			return ResponseParser.parse<T>(response.Result, representation, (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		public T putStream<T>(string path, MediaType mediaType, Stream content, Type responseClass)
		{
			var response = httpPutStream<T>(path, content, responseClass);
			return ResponseParser.parse<T>(response.Result, responseClass, (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		public T postFile<T>(string path, ManagedObjectRepresentation container, byte[] bytes, T representation)
		{
			var response = httpPostFile<T>(path, bytes, container);
			return ResponseParser.parse<T>(response.Result, typeof(T), (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
		}

		private Task<HttpResponseMessage> httpPostFile<T>(string path, byte[] bytes, ManagedObjectRepresentation container)
		{
			path = Client.resolvePath(path);
			var stringContentObject = new StringContent(JsonConvert.SerializeObject(container, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
			stringContentObject.Headers.Clear();

			var stringContentFilesize = new StringContent(bytes.Length.ToString());
			stringContentObject.Headers.Clear();

			var streamContent = new StreamContent(new MemoryStream(bytes));
			streamContent.Headers.TryAddWithoutValidation("Content-Type", container.Type);

			MultipartFormDataContent multipartContent = new MultipartFormDataContent();
			multipartContent.Add(stringContentObject, "object");
			multipartContent.Add(stringContentFilesize, "filesize");
			multipartContent.Add(streamContent, "file", container.Name);

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(path),
				Content = multipartContent
			};

			if (PlatformParameters.requireResponseBody())
				request.Headers.TryAddWithoutValidation("Accept", MediaType.APPLICATION_JSON);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.GetTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}
	}
	
}