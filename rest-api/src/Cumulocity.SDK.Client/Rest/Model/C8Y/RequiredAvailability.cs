using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_RequiredAvailability")]
    public class RequiredAvailability
    {

        private int responseInterval;

        public RequiredAvailability()
        {
        }

        public RequiredAvailability(int responseInterval)
        {
            this.responseInterval = responseInterval;
        }

        public virtual int ResponseInterval
        {
            get
            {
                return responseInterval;
            }
            set
            {
                this.responseInterval = value;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }
            if (!(obj is RequiredAvailability))
            {
                return false;
            }

            RequiredAvailability rhs = (RequiredAvailability) obj;
            return rhs.responseInterval == this.responseInterval;
        }

        public override int GetHashCode()
        {
            return responseInterval;
        }
    }

}