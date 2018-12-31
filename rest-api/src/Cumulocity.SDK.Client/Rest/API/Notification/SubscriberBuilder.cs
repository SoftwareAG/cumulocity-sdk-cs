using Cometd.Bayeux.Client;
using Cometd.Client.Ext;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Notification.Watchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	public class SubscriberBuilder<T, R>
	{
		public const string NOTIFICATIONS = "cep/notifications";
		public const string REALTIME = "cep/realtime";

		public static ISubscriptionNameResolver<T> identityNameResolve<T>()
		{
			return new SubscriptionNameResolver<T>();
		}

		private string endpoint;

		private Type dataType;

		private PlatformParameters parameters;

		private ISubscriptionNameResolver<T> subscriptionNameResolver;

		private bool messageReliability = false;

		private UnauthorizedConnectionWatcher unauthorizedConnectionWatcher = new UnauthorizedConnectionWatcher();

		public static SubscriberBuilder<T, R> anSubscriber<T, R>()
		{
			return new SubscriberBuilder<T, R>();
		}

		public virtual SubscriberBuilder<T, R> withRealtimeEndpoint()
		{
			return withEndpoint(REALTIME);
		}

		public virtual SubscriberBuilder<T, R> withNotificationEndpoint()
		{
			return withEndpoint(NOTIFICATIONS);
		}

		public virtual SubscriberBuilder<T, R> withEndpoint(string endpoint)
		{
			this.endpoint = endpoint;
			return this;
		}

		public virtual SubscriberBuilder<T, R> withDataType(Type dataType)
		{
			this.dataType = dataType;
			return this;
		}

		public virtual SubscriberBuilder<T, R> withParameters(PlatformParameters parameters)
		{
			this.parameters = parameters;
			return this;
		}

		public virtual SubscriberBuilder<T, R> withIdentityNameResolver()
		{
			return withSubscriptionNameResolver(identityNameResolve<T>());
		}

		public virtual SubscriberBuilder<T, R> withSubscriptionNameResolver(ISubscriptionNameResolver<T> subscriptionNameResolver)
		{
			this.subscriptionNameResolver = subscriptionNameResolver;
			return this;
		}

		public virtual SubscriberBuilder<T, R> withMessageDeliveryAcknowlage(bool enabled)
		{
			this.messageReliability = enabled;
			return this;
		}

		public virtual ISubscriber<T,R> build()
		{
			verifyRequiredFields();
			return new TypedSubscriber<T, R>(new SubscriberImpl<T>(subscriptionNameResolver, createSessionProvider(), unauthorizedConnectionWatcher), dataType);
		}

		private void verifyRequiredFields()
		{
			checkNotNull(endpoint, "endpoint can't be null");
			checkNotNull(parameters, "platform parameters can't be null");
			checkNotNull(subscriptionNameResolver, "subscriptionNameResolver can't be null");
			checkNotNull(dataType, "dataType can't be null");
		}

		private void checkNotNull(object value, string message)
		{
			if (value == null)
			{
				throw new System.InvalidOperationException(message);
			}
		}

		private IBayeuxSessionProvider createSessionProvider()
		{
			return DefaultBayeuxClientProvider.createProvider(endpoint, parameters, dataType, unauthorizedConnectionWatcher, resolveEnabledExtensions());
		}

		private IExtension[] resolveEnabledExtensions()
		{
			ICollection<IExtension> extensions = new LinkedList<IExtension>();
			if (messageReliability)
			{
				extensions.Add(new AckExtension());
			}
			return extensions.ToArray();
		}
	}
}