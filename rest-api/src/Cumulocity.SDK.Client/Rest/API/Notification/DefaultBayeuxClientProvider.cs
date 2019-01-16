using Cometd.Bayeux.Client;
using Cometd.Client;
using Cometd.Client.Transport;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Notification.Transport;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	internal class DefaultBayeuxClientProvider : IBayeuxSessionProvider
	{
		private const int CONNECTED_STATE_TIMEOUT = 10000;

		private readonly PlatformParameters paramters;

		private readonly string endpoint;

		private readonly Type endpointDataType;

		private readonly UnauthorizedConnectionWatcher unauthorizedConnectionWatcher;

		private readonly ICollection<IExtension> extensions;

		public static IBayeuxSessionProvider createProvider(string endpoint, PlatformParameters paramters, Type endpointDataType, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher, params IExtension[] extensions)
		{
			return new DefaultBayeuxClientProvider(endpoint, paramters, endpointDataType, unauthorizedConnectionWatcher, extensions);
		}

		public DefaultBayeuxClientProvider(string endpoint, PlatformParameters paramters, Type endpointDataType, UnauthorizedConnectionWatcher unauthorizedConnectionWatcher, params IExtension[] extensions)
		{
			this.paramters = paramters;
			this.endpoint = endpoint;
			this.endpointDataType = endpointDataType;
			this.unauthorizedConnectionWatcher = unauthorizedConnectionWatcher;
			this.extensions = extensions.ToList();
		}

		public IClientSession get()
		{
			return openSession(createSession());
		}

		private BayeuxClient createSession()
		{
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
			string host = paramters.Host;
			return (host.EndsWith("/", StringComparison.Ordinal) ? host : host + "/") + endpoint;
		}

		private ClientTransport createTransport()
		{
			return new CumulocityLongPollingTransport(createTransportOptions(), paramters, unauthorizedConnectionWatcher);
		}

		private IDictionary<string, object> createTransportOptions()
		{
			IDictionary<string, object> options = new Dictionary<string, object>();
			return options;
		}

		public override string ToString()
		{
			return buildUrl();
		}
	}
}