using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_DHCP")]
    public class DHCP 
    {

        private bool enabled;
        private string dns1;
        private string dns2;
        private string domainName;
        private AddressRange addressRange;

        [JsonProperty(PropertyName = "enabled")]
        public virtual bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                this.enabled = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "dns1")]
        public virtual string Dns1
        {
            get
            {
                return dns1;
            }
            set
            {
                this.dns1 = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "dns2")]
        public virtual string Dns2
        {
            get
            {
                return dns2;
            }
            set
            {
                this.dns2 = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "domainName")]
        public virtual string DomainName
        {
            get
            {
                return domainName;
            }
            set
            {
                this.domainName = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "addressRange")]
        public virtual AddressRange AddressRange
        {
            get
            {
                return addressRange;
            }
            set
            {
                this.addressRange = value;
            }
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((addressRange == null) ? 0 : addressRange.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(dns1, null)) ? 0 : dns1.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(dns2, null)) ? 0 : dns2.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(domainName, null)) ? 0 : domainName.GetHashCode());
            result = prime * result + (enabled ? 1231 : 1237);
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
            DHCP other = (DHCP)obj;
            if (addressRange == null)
            {
                if (other.addressRange != null)
                {
                    return false;
                }
            }
            else if (!addressRange.Equals(other.addressRange))
            {
                return false;
            }
            if (string.ReferenceEquals(dns1, null))
            {
                if (!string.ReferenceEquals(other.dns1, null))
                {
                    return false;
                }
            }
            else if (!dns1.Equals(other.dns1))
            {
                return false;
            }
            if (string.ReferenceEquals(dns2, null))
            {
                if (!string.ReferenceEquals(other.dns2, null))
                {
                    return false;
                }
            }
            else if (!dns2.Equals(other.dns2))
            {
                return false;
            }
            if (string.ReferenceEquals(domainName, null))
            {
                if (!string.ReferenceEquals(other.domainName, null))
                {
                    return false;
                }
            }
            else if (!domainName.Equals(other.domainName))
            {
                return false;
            }
            if (enabled != other.enabled)
            {
                return false;
            }
            return true;
        }

    }

}
