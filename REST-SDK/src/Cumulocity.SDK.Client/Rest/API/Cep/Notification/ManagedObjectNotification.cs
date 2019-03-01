using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Cep.Notification
{
	public class ManagedObjectNotification
	{
		public ManagedObjectNotification()
		{
		}

		public ManagedObjectNotification(ManagedObjectRepresentation data, string realtimeAction)
		{
			this.data = data;
			this.realtimeAction = realtimeAction;
		}

		private const string UPDATE_ACTION = "UPDATE";

		private ManagedObjectRepresentation data;
		private string realtimeAction;

		public virtual ManagedObjectRepresentation Data
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