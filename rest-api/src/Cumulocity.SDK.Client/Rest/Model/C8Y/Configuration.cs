using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Configuration")]
    public class Configuration
    {
        private string config;

        public Configuration()
        {
        }

        public Configuration(string config)
        {
            this.config = config;
        }

        public virtual string Config
        {
            get
            {
                return config;
            }
            set
            {
                this.config = value;
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
            if (!(obj is Configuration))
            {
                return false;
            }

            Configuration rhs = (Configuration) obj;
            return string.ReferenceEquals(config, null) ? string.ReferenceEquals(rhs.Config, null) : config.Equals(rhs.Config);
        }

        public override int GetHashCode()
        {
            return !string.ReferenceEquals(config, null) ? config.GetHashCode() : 0;
        }
    }
}