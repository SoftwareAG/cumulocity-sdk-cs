using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
	public class Tracking
	{
		// Reporting interval in seconds
		protected internal  int interval;
		[JsonProperty("interval")]
		public virtual int Interval
		{
			get => interval;
			set => interval = value;
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj == this) return true;
			if (!(obj is MotionTracking)) return false;

			var rhs = (Tracking)obj;
			return interval == rhs.interval;
		}

		protected bool Equals(Tracking other)
		{
			if (other == null) return false;
			if (ReferenceEquals(other, this)) return true;
			if (!(other is MotionTracking)) return false;

			return interval == other.interval;
		}

		public override int GetHashCode()
		{
			return interval;
		}
	}
}