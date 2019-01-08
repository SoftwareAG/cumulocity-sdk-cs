using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

		private CumulocityHttpClient client; //= new CumulocityHttpClient();

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

		public T Get<T>(string path, CumulocityMediaType mediaType, Type responseType)
		{
			var response = getClientResponse(path, mediaType);
			return ResponseParser.parse<T>(response.Result, responseType, (int)HttpStatusCode.OK);
		}

		public T Get<T>(string path, MediaType mediaType, Type responseType)
		{
			var response = getClientResponse(path, mediaType);
			return ResponseParser.parse<T>(response.Result, responseType, (int)HttpStatusCode.OK);
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

		public T putText<T>(string path, string content, Type responseClass)
		{
			throw new NotImplementedException();
		}

		public T putStream<T>(string path, MediaType mediaType, Stream content, Type responseClass)
		{
			throw new NotImplementedException();
		}

		public T Post<T>(string path, CumulocityMediaType mediaType, T representation)
			where T : IResourceRepresentation
		{
			var response = httpPost(path, mediaType, mediaType, representation);

			return ResponseParser.parse<T>(response.Result, typeof(T), (int)HttpStatusCode.Created, (int)HttpStatusCode.OK);
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
			return parseResponseWithId(representation, response.Result, (int)HttpStatusCode.Created);
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
			return parseResponseWithId(representation, response.Result, (int)HttpStatusCode.OK);
		}

		public T PutWithoutId<T>(string path, CumulocityMediaType mediaType, T representation)
		{
			var response = this.putClientResponse<T>(path, mediaType, representation);
			return ResponseParser.parse<T>(response.Result, representation.GetType(), (int)HttpStatusCode.OK);
		}

		public void Delete(string path)
		{
			var response = this.deleteClientResponse(path);
			this.ResponseParser.checkStatus(response.Result, new int[] { (int)HttpStatusCode.NoContent });
		}

		private Task<HttpResponseMessage> getClientResponse(string path, MediaType mediaType)
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(path)
			};
			request.Headers.TryAddWithoutValidation("Accept", mediaType.Subtype);

			request.AddApplicationKeyHeader(this.PlatformParameters.ApplicationKey);
			request.AddTfaHeader(this.PlatformParameters.getTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
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

		private Task<HttpResponseMessage> httpStreamPost<T>(string path, CumulocityMediaType contentType,
			CumulocityMediaType accept, Stream content, T representation)
		{
			// Send binary data and string data in a single request.
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
			request.AddTfaHeader(this.PlatformParameters.getTfaToken());
			request.AddRequestOriginHeader(this.PlatformParameters.RequestOrigin);

			return client.SendAsync(request);
		}

		private Task<HttpResponseMessage> httpPostText<T>(string path, String content, Type representation)
		{
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
			request.AddTfaHeader(this.PlatformParameters.getTfaToken());

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
			//client.addFilter(new HTTPBasicAuthFilter(platformParameters.Principal, platformParameters.Password));

			client.addBasicAuthFilter(platformParameters.Principal, platformParameters.Password);

			return client;
		}

		private HttpClientHandler createDefaultClientHander(HttpClientHandler config)
		{
			var credentials = new NetworkCredential("user", "pass");
			var handler = new HttpClientHandler { Credentials = credentials };

			return handler;
		}
	}
}