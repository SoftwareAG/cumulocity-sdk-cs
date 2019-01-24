using Cumulocity.SDK.Client.Rest.API.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Notification
{
	public class OperationNotificationSubscriber : ISubscriber<GId, OperationRepresentation>
	{
		public const string DEVICE_CONTROL_NOTIFICATIONS_URL = "devicecontrol/notifications";

		private readonly ISubscriber<GId, OperationRepresentation> subscriber;

		public OperationNotificationSubscriber(PlatformParameters parameters)
		{
			subscriber = CreateSubscriber(parameters);
		}

		private ISubscriber<GId, OperationRepresentation> CreateSubscriber(PlatformParameters parameters)
		{
			return SubscriberBuilder<GId, OperationRepresentation>.anSubscriber<GId, OperationRepresentation>().withEndpoint(DEVICE_CONTROL_NOTIFICATIONS_URL).withSubscriptionNameResolver(new AgentDeviceIdAsSubscriptonName()).withParameters(parameters).withDataType(typeof(OperationRepresentation)).withMessageDeliveryAcknowlage(true).build();
		}

		public virtual ISubscription<GId> Subscribe(GId agentId, ISubscriptionListener<GId, OperationRepresentation> handler)
		{
			return subscriber.Subscribe(agentId, handler);
		}

		public ISubscription<GId> Subscribe(GId agentId, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<GId, OperationRepresentation> handler, bool autoRetry)
		{
			return subscriber.Subscribe(agentId, subscribeOperationListener, handler, autoRetry);
		}

		public virtual void Disconnect()
		{
			subscriber.Disconnect();
		}

		private sealed class AgentDeviceIdAsSubscriptonName : ISubscriptionNameResolver<GId>
		{
			public string apply(GId id)
			{
				return $"/{id.Value}";
			}
		}
	}
}