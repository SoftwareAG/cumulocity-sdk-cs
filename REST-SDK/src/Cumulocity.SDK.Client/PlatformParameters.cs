using Cumulocity.SDK.Client.Rest.API.Base;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.Buffering;
using System;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest
{
	public class PlatformParameters
	{
		public const int DEFAULT_PAGE_SIZE = 5;
		private string applicationKey;
		private BufferProcessor bufferProcessor;
		private IBufferRequestService bufferRequestService;
		private readonly ClientConfiguration clientConfiguration;
		private CumulocityLogin cumulocityLogin;
		private bool forceInitialHost;
		private string host;
		private string password;
		private int proxyPort;
		private string requestOrigin;
		private bool requireResponseBody_Renamed;
		private RestConnector restConnector;
		private string tenantId;
		private ISupplier<string> tfaToken;

		private string user;

		public PlatformParameters()
		{
			proxyPort = -1;
			requireResponseBody_Renamed = true;
			forceInitialHost = false;
			PageSize = 5;
		}

		public PlatformParameters(string host, CumulocityCredentials credentials,
			ClientConfiguration clientConfiguration) : this(host, credentials, clientConfiguration, 5)
		{
		}

		public PlatformParameters(string host, CumulocityCredentials credentials,
			ClientConfiguration clientConfiguration, int pageSize)
		{
			proxyPort = -1;
			requireResponseBody_Renamed = true;
			forceInitialHost = false;
			PageSize = 5;
			PageSize = pageSize;
			this.clientConfiguration = clientConfiguration;
			setMandatoryFields(host, credentials);
		}

		public virtual int PageSize { get; }

		public virtual string Host
		{
			get => host;
			set => host = value;
		}

		public virtual string TenantId
		{
			get => tenantId;
			set => tenantId = value;
		}

		public virtual string User
		{
			get => user;
			set => user = value;
		}

		public virtual string Password
		{
			get => password;
			set => password = value;
		}

		public virtual string ProxyHost { get; set; }

		public virtual int ProxyPort
		{
			get => proxyPort;
			set => proxyPort = value;
		}

		public virtual string ProxyUserId { get; set; }

		public virtual string ProxyPassword { get; set; }

		public virtual string ApplicationKey
		{
			get => applicationKey;
			set => applicationKey = value;
		}

		public virtual bool RequireResponseBody
		{
			set => requireResponseBody_Renamed = value;
		}

		public virtual bool ForceInitialHost
		{
			get => forceInitialHost;
			set => forceInitialHost = value;
		}

		public virtual string Principal => cumulocityLogin.toLoginString();

		public virtual string RequestOrigin
		{
			get => requestOrigin;
			set => requestOrigin = value;
		}

		internal virtual IBufferRequestService BufferRequestService => bufferRequestService;

		private void setMandatoryFields(string host, CumulocityCredentials credentials)
		{
			if (host[host.Length - 1] != '/') host = $"{host}/";

			this.host = host;
			tenantId = credentials.TenantId;
			user = credentials.Username;
			password = credentials.Password;
			applicationKey = credentials.ApplicationKey;
			cumulocityLogin = credentials.Login;
			requestOrigin = credentials.RequestOrigin;
		}

		private void startBufferProcessing()
		{
			if (clientConfiguration.AsyncEnabled)
			{
				var persistentProvider = clientConfiguration.PersistentProvider;
				bufferRequestService = new BufferRequestServiceImpl(persistentProvider);
				bufferProcessor = new BufferProcessor(persistentProvider, bufferRequestService, restConnector);
				bufferProcessor.StartProcessing();
			}
			else
			{
				bufferRequestService = new DisabledBufferRequestService(this);
			}
		}
		public bool ConfigurationAsyncEnabled => clientConfiguration.AsyncEnabled;

		public virtual RestConnector createRestConnector()
		{
			lock (this)
			{
				if (restConnector == null)
				{
					restConnector = new RestConnector(this, new ResponseParser());
					startBufferProcessing();
				}

				return restConnector;
			}
		}

		public virtual bool requireResponseBody()
		{
			return requireResponseBody_Renamed;
		}

		public virtual string GetTfaToken()
		{
			return tfaToken == null ? null : tfaToken.Get();
		}

		public virtual void SetTfaToken(string tfaToken)
		{
			this.tfaToken = Suppliers.OfInstance(tfaToken);
		}

		public virtual void setTfaToken(ISupplier<string> tfaToken)
		{
			this.tfaToken = tfaToken;
		}

		public virtual void close()
		{
			if (bufferProcessor != null) bufferProcessor.Shutdown();
		}

		private sealed class DisabledBufferRequestService : IBufferRequestService
		{
			private readonly PlatformParameters outerInstance;

			internal DisabledBufferRequestService(PlatformParameters outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public Task<object> Create(BufferedRequest request)
			{
				throw new Exception("Async feature is disabled in this platform client instance.");
			}
			public void addResponse(long requestId, object result)
			{
			}
		}
	}
}