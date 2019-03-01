using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;
using System.Data;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
	[PackageName("c8y_Availability")]
	public class Availability
	{
		private DateTime lastMessage;
		private ConnectionState status;

		public Availability()
		{
		}

		public Availability(DateTime lastMessage, ConnectionState status)
		{
			this.lastMessage = lastMessage;
			this.status = status;
		}

		[JsonProperty("lastMessage", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime LastMessage
		{
			get
			{
				return lastMessage;
			}
			set
			{
				this.lastMessage = value;
			}
		}
		[JsonProperty("status")]
		public virtual ConnectionState Status
		{
			get
			{
				return status;
			}
			set
			{
				this.status = value;
			}
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((lastMessage == null) ? 0 : lastMessage.GetHashCode());
			result = prime * result + ((status == null) ? 0 : status.GetHashCode());
			return result;
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			Availability other = (Availability)obj;
			if (lastMessage == null)
			{
				if (other.lastMessage != null)
				{
					return false;
				}
			}
			else if (!lastMessage.Equals(other.lastMessage))
			{
				return false;
			}
			if (status != other.status)
			{
				return false;
			}
			return true;
		}
	}
}