using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Firmware")]
    public class Firmware 
    {

        private string name;
        private string version;
        private string url;

        public Firmware()
        {
        }

        public Firmware(string name, string version, string url)
        {
            this.name = name;
            this.version = version;
            this.url = url;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Url
        {
            get
            {
                return url;
            }
            set
            {
                this.url = value;
            }
        }


        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        public virtual string Version
        {
            get
            {
                return version;
            }
            set
            {
                this.version = value;
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
            if (!(obj is Hardware))
            {
                return false;
            }

            Firmware rhs = (Firmware) obj;
            bool result = string.ReferenceEquals(name, null) ? string.ReferenceEquals(rhs.name, null) : name.Equals(rhs.name);
            result = result && (string.ReferenceEquals(version, null) ? (string.ReferenceEquals(rhs.version, null)) : version.Equals(rhs.version));
            result = result && (string.ReferenceEquals(url, null) ? (string.ReferenceEquals(rhs.url, null)) : url.Equals(rhs.url));
            return result;
        }

        public override int GetHashCode()
        {
            int result = string.ReferenceEquals(name, null) ? 0 : name.GetHashCode();
            result = 31 * result + (string.ReferenceEquals(version, null) ? 0 : version.GetHashCode());
            result = 31 * result + (string.ReferenceEquals(url, null) ? 0 : url.GetHashCode());
            return result;
        }
    }
}