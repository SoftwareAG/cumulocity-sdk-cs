namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
	public class Tracking
	{
		// Reporting interval in seconds
		protected internal int interval;

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
	}
}