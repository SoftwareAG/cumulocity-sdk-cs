using Cumulocity.SDK.Client.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Cumulocity.SDK.Client
{
#pragma warning disable CS0168
#pragma warning disable CS0169
	public class CumulocityHttpClient : HttpClient
	{
		private readonly Regex hostPattern = new Regex("((http|https):\\/\\/.+?)(\\/|\\?|$)");

		private PlatformParameters platformParameters;
		private readonly SerializerSettings _serializerSettings;

		public CumulocityHttpClient(HttpClientHandler createDefaultClientHandler, bool disposeHandler) : base(
			createDefaultClientHandler, disposeHandler)
		{
		}

		public virtual PlatformParameters PlatformParameters
		{
			set => platformParameters = value;
		}

		private string InitialHost
		{
			get
			{
				var initialHost = platformParameters.Host;
				if (initialHost.EndsWith("/", StringComparison.Ordinal))
					initialHost = initialHost.Substring(0, initialHost.Length - 1);
				return initialHost;
			}
		}

		public CumulocityHttpClient resource(string path)
		{
			BaseAddress = new Uri(resolvePath(path));

			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				Formatting = Newtonsoft.Json.Formatting.Indented,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			return this;
		}

		public string resolvePath(string path)
		{
			if (path.StartsWith("/", StringComparison.Ordinal)) path = InitialHost + path;
			return platformParameters.ForceInitialHost ? insertInitialHost(path) : path;
		}

		private string insertInitialHost(string path)
		{
			var matcher = hostPattern.Match(path);
			if (matcher.Success)
			{
				var capturedHost = matcher.Groups[1];
				return path.Replace(capturedHost.Value, InitialHost);
			}

			return path;
		}

		public void addBasicAuthFilter(string platformParametersPrincipal, string platformParametersPassword)
		{
			this.DefaultRequestHeaders.Authorization
				 = new AuthenticationHeaderValue("Basic",
					 Convert.ToBase64String(
						 System.Text.ASCIIEncoding.ASCII.GetBytes(
							 string.Format("{0}:{1}", platformParameters.Principal, platformParameters.Password))));
		}

		private class SerializerSettings : JsonSerializerSettings

		{
			public SerializerSettings()

			{
				this.ContractResolver = new CamelCasePropertyNamesContractResolver();

				this.NullValueHandling = NullValueHandling.Ignore;

				this.Converters.Add(new StringEnumConverter());
			}
		}
	}
}