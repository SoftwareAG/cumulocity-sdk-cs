using Cometd.Bayeux;
using Cometd.Bayeux.Client;
using Cumulocity.SDK.Client.Logging;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	public class ReconnectOnSuccessfulConnected<T> : IExtension
	{
		private readonly SubscriberImpl<T> subscriberImpl;
		private static readonly ILog LOG = LogProvider.For<ReconnectOnSuccessfulConnected<T>>();
		public ReconnectOnSuccessfulConnected(SubscriberImpl<T> subscriberImpl)
		{
			this.subscriberImpl = subscriberImpl;
		}

		private volatile bool reHandshakeSuccessful = false;

		private volatile bool reconnectedSuccessful = false;

		public bool sendMeta(IClientSession session, IMutableMessage message)
		{
			return true;
		}

		public bool send(IClientSession session, IMutableMessage message)
		{
			return true;
		}

		public bool rcvMeta(IClientSession session, IMutableMessage message)
		{
			if (subscriberImpl.isSuccessfulHandshake(message))
			{
				reHandshakeSuccessful = true;
			}
			else if (subscriberImpl.isSuccessfulConnected(message))
			{
				reconnectedSuccessful = true;
			}
			else
			{
				return true;
			}
			// Resubscribe only there is a successful handshake and successfully connected.
			if (reHandshakeSuccessful && reconnectedSuccessful)
			{
				LOG.Debug("reconnect operation detected for session {} - {} ",  session.Id);
				reHandshakeSuccessful = false;
				reconnectedSuccessful = false;
				subscriberImpl.resubscribe();
			}
			return true;
		}

		public bool rcv(IClientSession session, IMutableMessage message)
		{
			return true;
		}
	}
}