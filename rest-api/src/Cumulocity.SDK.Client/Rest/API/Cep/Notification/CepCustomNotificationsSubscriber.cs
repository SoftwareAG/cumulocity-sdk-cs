using Cumulocity.SDK.Client.Rest.API.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Cep.Notification
{
	public class CepCustomNotificationsSubscriber : ISubscriber<string, object>
	{
		public const string CEP_CUSTOM_NOTIFICATIONS_URL = "cep/customnotifications";

		private readonly ISubscriber<string, object> subscriber;

		public CepCustomNotificationsSubscriber(PlatformParameters parameters)
		{
			subscriber = CreateSubscriber(parameters);
		}

		private ISubscriber<string, object> CreateSubscriber(PlatformParameters parameters)
		{
			return SubscriberBuilder<string, object>.anSubscriber<string, object>().withParameters(parameters).withEndpoint(CEP_CUSTOM_NOTIFICATIONS_URL).withSubscriptionNameResolver(new Identity()).withDataType(typeof(object)).build();
		}

		public virtual ISubscription<string> Subscribe(string channelID, ISubscriptionListener<string, object> handler)
		{
			return subscriber.Subscribe(channelID, handler);
		}

		public ISubscription<string> Subscribe(string channelID, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<string, object> handler, bool autoRetry)
		{
			return subscriber.Subscribe(channelID, subscribeOperationListener, handler, autoRetry);
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