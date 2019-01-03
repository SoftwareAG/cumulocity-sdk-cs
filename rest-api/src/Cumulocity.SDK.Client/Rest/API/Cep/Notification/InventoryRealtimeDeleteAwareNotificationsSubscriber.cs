using Cumulocity.SDK.Client.Rest.API.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Cep.Notification
{
	public class InventoryRealtimeDeleteAwareNotificationsSubscriber : ISubscriber<string, ManagedObjectDeleteAwareNotification>
	{
		private const string REALTIME_NOTIFICATIONS_URL = "cep/realtime";

		private readonly ISubscriber<string, ManagedObjectDeleteAwareNotification> subscriber;

		private const string channelPrefix = "/managedobjects/";

		public InventoryRealtimeDeleteAwareNotificationsSubscriber(PlatformParameters parameters)
		{
			subscriber = createSubscriber(parameters);
		}

		private ISubscriber<string, ManagedObjectDeleteAwareNotification> createSubscriber(PlatformParameters parameters)
		{
			return SubscriberBuilder<string, ManagedObjectDeleteAwareNotification>.anSubscriber<string, ManagedObjectDeleteAwareNotification>().withParameters(parameters).withEndpoint(REALTIME_NOTIFICATIONS_URL).withSubscriptionNameResolver(new Identity()).withDataType(typeof(ManagedObjectDeleteAwareNotification)).build();
		}

		//ORIGINAL LINE: public Subscription<String> subscribe(final String channelID, final SubscriptionListener<String, ManagedObjectDeleteAwareNotification> handler) throws SDKException
		public virtual ISubscription<string> subscribe(string channelID, ISubscriptionListener<string, ManagedObjectDeleteAwareNotification> handler)
		{
			return subscriber.subscribe(channelPrefix + channelID, handler);
		}

		//ORIGINAL LINE: @Override public Subscription<String> subscribe(String agentId, SubscribeOperationListener subscribeOperationListener, SubscriptionListener<String, ManagedObjectDeleteAwareNotification> handler, boolean autoRetry) throws SDKException
		public ISubscription<string> subscribe(string agentId, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<string, ManagedObjectDeleteAwareNotification> handler, bool autoRetry)
		{
			return subscriber.subscribe(agentId, subscribeOperationListener, handler, autoRetry);
		}

		public virtual void disconnect()
		{
			subscriber.disconnect();
		}

		private sealed class Identity : ISubscriptionNameResolver<string>
		{
			public string apply(string id)
			{
				return id;
			}
		}
	}
}