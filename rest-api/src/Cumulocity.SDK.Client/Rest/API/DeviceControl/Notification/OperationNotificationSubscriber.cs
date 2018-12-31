using System;
using System.Collections.Generic;
using System.Text;
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
			subscriber = createSubscriber(parameters);
		}

		private ISubscriber<GId, OperationRepresentation> createSubscriber(PlatformParameters parameters)
		{
			return SubscriberBuilder<GId, OperationRepresentation>.anSubscriber<GId, OperationRepresentation>().withEndpoint(DEVICE_CONTROL_NOTIFICATIONS_URL).withSubscriptionNameResolver(new AgentDeviceIdAsSubscriptonName()).withParameters(parameters).withDataType(typeof(OperationRepresentation)).withMessageDeliveryAcknowlage(true).build();
		}

		//ORIGINAL LINE: public Subscription<GId> subscribe(final GId agentId, final SubscriptionListener<GId, OperationRepresentation> handler) throws SDKException
		public virtual ISubscription<GId> subscribe(GId agentId, ISubscriptionListener<GId, OperationRepresentation> handler)
		{
			return subscriber.subscribe(agentId, handler);
		}

		//ORIGINAL LINE: @Override public Subscription<GId> subscribe(GId agentId, SubscribeOperationListener subscribeOperationListener, SubscriptionListener<GId, OperationRepresentation> handler, boolean autoRetry) throws SDKException
		public ISubscription<GId> subscribe(GId agentId, ISubscribeOperationListener subscribeOperationListener, ISubscriptionListener<GId, OperationRepresentation> handler, bool autoRetry)
		{
			return subscriber.subscribe(agentId, subscribeOperationListener, handler, autoRetry);
		}

		public virtual void disconnect()
		{
			subscriber.disconnect();
		}

		private sealed class AgentDeviceIdAsSubscriptonName : ISubscriptionNameResolver<GId>
		{
			public  string apply(GId id)
			{
				return "/" + id.Value;
			}
		}
	}
}
