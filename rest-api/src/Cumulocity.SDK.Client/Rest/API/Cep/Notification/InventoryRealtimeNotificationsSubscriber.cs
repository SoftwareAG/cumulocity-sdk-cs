using Cumulocity.SDK.Client.Rest.API.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Cep.Notification
{
	/// <summary>
	/// This subscriber does not support realtime DELETE actions.
	/// Please use instead InventoryRealtimeDeleteAwareNotificationsSubscriber.cs
	///
	/// </summary>
	[Obsolete]
	public class InventoryRealtimeNotificationsSubscriber : ISubscriber<string, ManagedObjectNotification>
	{
		private const string REALTIME_NOTIFICATIONS_URL = "cep/realtime";

		private readonly ISubscriber<string, ManagedObjectNotification> subscriber;

		private const string channelPrefix = "/managedobjects/";

		public InventoryRealtimeNotificationsSubscriber(PlatformParameters parameters)
		{
			subscriber = createSubscriber(parameters);
		}

		private ISubscriber<string, ManagedObjectNotification> createSubscriber(PlatformParameters parameters)
		{
			return SubscriberBuilder<string, ManagedObjectNotification>.anSubscriber<string, ManagedObjectNotification>().withParameters(parameters).withEndpoint(REALTIME_NOTIFICATIONS_URL).withSubscriptionNameResolver(new Identity()).withDataType(typeof(ManagedObjectNotification)).build();
		}

		/// <summary>
		/// This method does NOT allow to receive device realtime DELETE actions
		/// </summary>
		//ORIGINAL LINE: public Subscription<String> subscribe(final String channelID, final SubscriptionListener<String, ManagedObjectNotification> handler) throws SDKException
		public virtual ISubscription<string> subscribe(string channelID, ISubscriptionListener<string, ManagedObjectNotification> handler)
		{
			return subscriber.subscribe(channelPrefix + channelID, handler);
		}

		//ORIGINAL LINE: @Override public Subscription<String> subscribe(String channelID, SubscribeOperationListener subscribeOperationListener, SubscriptionListener<String, ManagedObjectNotification> handler, boolean autoRetry) throws SDKException
		public ISubscription<string> subscribe(string channelID, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<string, ManagedObjectNotification> handler, bool autoRetry)
		{
			return subscriber.subscribe(channelID, subscribeOperationListener, handler, autoRetry);
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