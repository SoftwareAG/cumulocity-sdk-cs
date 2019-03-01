using System;
using System.Collections.Generic;
using System.Text;
using Cometd.Bayeux;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Newtonsoft.Json.Linq;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	public class TypedSubscriber<T,R> : ISubscriber<T,R>
	{
		private readonly ISubscriber<T, IMessage> subscriber;

		private readonly Type dataType;

		public TypedSubscriber(ISubscriber<T, IMessage> subscriber, Type dataType)
		{
			this.subscriber = subscriber;
			this.dataType = dataType;
		}

		public virtual ISubscription<T> Subscribe(T @object, ISubscriptionListener<T, R> handler)
		{
			return subscriber.Subscribe(@object, new MappingSubscriptionListener<T, R>(handler, dataType));
		}

		public ISubscription<T> Subscribe(T agentId, ISubscribeOperationListener subscribeOperationListener,
			ISubscriptionListener<T, R> handler, bool autoRetry)
		{
			return subscriber.Subscribe(agentId, subscribeOperationListener,
				new MappingSubscriptionListener<T, R>(handler, dataType), autoRetry);
		}

		public virtual void Disconnect()
		{
			subscriber.Disconnect();
		}

	}

	internal sealed class MappingSubscriptionListener<T, R> : ISubscriptionListener<T, IMessage>
	{
		internal readonly ISubscriptionListener<T, R> handler;

		internal readonly Type dataType;

		internal MappingSubscriptionListener(ISubscriptionListener<T, R> handler, Type dataType)
		{
			this.handler = handler;
			this.dataType = dataType;
		}

		public void OnError(ISubscription<T> subscription, Exception ex)
		{
			handler.OnError(subscription, ex);
		}

		public void OnNotification(ISubscription<T> subscription, IMessage notification)
		{
			R data = asDataType(notification);
			handler.OnNotification(subscription, data);
		}

		internal R asDataType(IMessage notification)
		{
			object data = notification.Data;

			try
			{
				return Helpers.GetObject<R>(((JObject)data).ToObject<Dictionary<string, object>>());
			}
			catch (InvalidCastException)
			{
				return default(R);
			}
		}
	}

}
