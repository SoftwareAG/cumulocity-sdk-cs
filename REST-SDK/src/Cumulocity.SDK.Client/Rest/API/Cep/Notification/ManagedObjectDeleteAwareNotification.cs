namespace Cumulocity.SDK.Client.Rest.API.Cep.Notification
{
	public class ManagedObjectDeleteAwareNotification
	{
		public ManagedObjectDeleteAwareNotification()
		{
		}

		public ManagedObjectDeleteAwareNotification(object data, string realtimeAction)
		{
			this.data = data;
			this.realtimeAction = realtimeAction;
		}

		private const string UPDATE_ACTION = "UPDATE";

		private object data;
		private string realtimeAction;

		public virtual object Data
		{
			get
			{
				return data;
			}
			set
			{
				this.data = value;
			}
		}

		public virtual string RealtimeAction
		{
			get
			{
				return realtimeAction;
			}
			set
			{
				this.realtimeAction = value;
			}
		}

		public virtual bool UpdateNotification
		{
			get
			{
				return UPDATE_ACTION.Equals(realtimeAction);
			}
		}
	}
}