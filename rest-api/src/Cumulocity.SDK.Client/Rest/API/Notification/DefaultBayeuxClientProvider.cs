using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cometd.Bayeux.Client;
using Cometd.Client;
using Cometd.Client.Transport;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Notification.Transport;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	internal class DefaultBayeuxClientProvider : IBayeuxSessionProvider
	{
		private const int CONNECTED_STATE_TIMEOUT = 30;

		private readonly PlatformParameters paramters;

		private readonly string endpoint;

		private readonly Type endpointDataType;

		private readonly UnauthorizedConnectionWatcher unauthorizedConnectionWatcher;

		private readonly ICollection<IExtension> extensions;

		//ORIGINAL LINE: public static BayeuxSessionProvider createProvider(final String endpoint, final PlatformParameters paramters, Class endpointDataType, final Provider<Client> httpClient, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher, Extension... extensions)
		public static IBayeuxSessionProvider createProvider(string endpoint, PlatformParameters paramters, Type endpointDataType,  UnauthorizedConnectionWatcher unauthorizedConnectionWatcher, params IExtension[] extensions)
		{
			return new DefaultBayeuxClientProvider(endpoint, paramters, endpointDataType,  unauthorizedConnectionWatcher, extensions);
		}

		public DefaultBayeuxClientProvider(string endpoint, PlatformParameters paramters, Type endpointDataType, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher, params IExtension[] extensions)
		{
			this.paramters = paramters;
			this.endpoint = endpoint;
			//this.httpClient = httpClient;
			this.endpointDataType = endpointDataType;
			this.unauthorizedConnectionWatcher = unauthorizedConnectionWatcher;
			this.extensions = extensions.ToList();
		}

		//ORIGINAL LINE: @Override public ClientSession get() throws SDKException
		public IClientSession get()
		{
			return openSession(createSession());
		}

		//ORIGINAL LINE: private BayeuxClient createSession() throws SDKException
		private BayeuxClient createSession()
		{
			//ORIGINAL LINE: final BayeuxClient session = new BayeuxClient(buildUrl(), createTransport(httpClient));
			BayeuxClient session = new BayeuxClient(buildUrl(), new List<ClientTransport>() { createTransport() });
			foreach (var extension in extensions)
			{
				session.addExtension(extension);
			}
			return session;
		}

		private BayeuxClient openSession(BayeuxClient bayeuxClient)
		{
			bayeuxClient.handshake();

			var handshake = bayeuxClient.waitFor(CONNECTED_STATE_TIMEOUT, new List<BayeuxClient.State>() { BayeuxClient.State.CONNECTED });
			if (handshake != BayeuxClient.State.CONNECTED)
			{
				throw new SDKException("unable to connect to server");
			}
			return bayeuxClient;
		}

		private string buildUrl()
		{
			//ORIGINAL LINE: final String host = paramters.getHost();
			string host = paramters.Host;
			return (host.EndsWith("/", StringComparison.Ordinal) ? host : host + "/") + endpoint;
		}

		//ORIGINAL LINE: private ClientTransport createTransport(final Provider<Client> httpClient)
		private ClientTransport createTransport()
		{
			return new CumulocityLongPollingTransport(createTransportOptions(), paramters, unauthorizedConnectionWatcher);
		}

		private IDictionary<string, object> createTransportOptions()
		{
			//ORIGINAL LINE: final Map<String, Object> options = new HashMap<String, Object>();
			//IDictionary<string, object> options = new Dictionary<string, object>();
			//options[ClientTransport.JSON_CONTEXT_OPTION] = new ClientSvensonJSONContext(endpointDataType);
			IDictionary<string, object> options = new Dictionary<string, object>();
			return options;
		}

		public override string ToString()
		{
			return buildUrl();
		}

	}

}
