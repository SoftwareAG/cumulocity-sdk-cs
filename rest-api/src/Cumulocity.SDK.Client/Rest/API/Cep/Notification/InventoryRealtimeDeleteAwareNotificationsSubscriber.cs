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

		public virtual ISubscription<string> Subscribe(string channelID, ISubscriptionListener<string, ManagedObjectDeleteAwareNotification> handler)
		{
			return subscriber.Subscribe(channelPrefix + channelID, handler);
		}

		public ISubscription<string> Subscribe(string agentId, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<string, ManagedObjectDeleteAwareNotification> handler, bool autoRetry)
		{
			return subscriber.Subscribe(agentId, subscribeOperationListener, handler, autoRetry);
		}

		public virtual void Disconnect()
		{
			subscriber.Disconnect();
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