using Cometd.Bayeux;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Subscriber
{
	public sealed class SubscriptionRecord<T>
	{
		private readonly T id;
		private readonly ISubscriptionListener<T, IMessage> listener;
		public readonly ISubscribeOperationListener subscribeOperationListener;
		private readonly SubscriberImpl<T> subscriberImpl;

		public SubscriptionRecord(T id, ISubscriptionListener<T, IMessage> listener, ISubscribeOperationListener subscribeOperationListener, SubscriberImpl<T> subscriberImpl)
		{
			this.id = id;
			this.listener = listener;
			this.subscribeOperationListener = subscribeOperationListener;
			this.subscriberImpl = subscriberImpl;
		}

		public void Remove()
		{
			subscriberImpl.removeSubscriptions(this);
		}

		public T Id
		{
			get
			{
				return id;
			}
		}

		public ISubscriptionListener<T, IMessage> Listener
		{
			get
			{
				return listener;
			}
		}

		public override int GetHashCode()
		{
			int result = id != null ? id.GetHashCode() : 0;
			result = 31 * result + (listener != null ? listener.GetHashCode() : 0);
			result = 31 * result + (subscribeOperationListener != null ? subscribeOperationListener.GetHashCode() : 0);
			return result;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || this.GetType() != o.GetType())
			{
				return false;
			}

			var that = (SubscriptionRecord<T>)o;

			if (id != null ? !id.Equals(that.id) : that.id != null)
			{
				return false;
			}
			if (listener != null ? !listener.Equals(that.listener) : that.listener != null)
			{
				return false;
			}
			return subscribeOperationListener != null ? subscribeOperationListener.Equals(that.subscribeOperationListener) : that.subscribeOperationListener == null;
		}

	}
}