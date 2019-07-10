using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_MotionTracking")]
    public class MotionTracking : Tracking
    {
        private bool active;
        [JsonProperty("active")]
        public virtual bool Active
        {
            get => active;
            set => active = value;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj == this) return true;

            if (!(obj is MotionTracking)) return false;

            var rhs = (MotionTracking)obj;
            return active == rhs.active;
        }

        public override int GetHashCode()
        {
            return active ? 0 : 1;
        }
    }
}