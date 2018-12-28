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

		//ORIGINAL LINE: public Subscription<T> subscribe(T object, SubscriptionListener<T, R> handler) throws SDKException
		public virtual ISubscription<T> subscribe(T @object, ISubscriptionListener<T, R> handler)
		{
			return subscriber.subscribe(@object, new MappingSubscriptionListener<T, R>(handler, dataType));
		}

		public ISubscription<T> subscribe(T agentId, ISubscribeOperationListener subscribeOperationListener,
			ISubscriptionListener<T, R> handler, bool autoRetry)
		{
			return subscriber.subscribe(agentId, subscribeOperationListener,
				new MappingSubscriptionListener<T, R>(handler, dataType), autoRetry);
		}

		public virtual void disconnect()
		{
			subscriber.disconnect();
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

		public void onError(ISubscription<T> subscription, Exception ex)
		{
			handler.onError(subscription, ex);
		}

		public void onNotification(ISubscription<T> subscription, IMessage notification)
		{
			//ORIGINAL LINE: final R data = asDataType(notification);
			R data = asDataType(notification);
			handler.onNotification(subscription, data);
		}

		internal R asDataType(IMessage notification)
		{
			//ORIGINAL LINE: final Object data = notification.getData();
			object data = notification.Data;
			//return data != null && typeof(data).IsAssignableFrom(typeof(data)) ? dataType.cast(data) : null;

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
