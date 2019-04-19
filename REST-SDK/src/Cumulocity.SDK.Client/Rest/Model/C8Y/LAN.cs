using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_LAN")]
    public class LAN
    {

        private string name;
        private string mac;
        private string ip;
        private string subnetMask;
        private bool enabled;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name")]
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


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mac")]
        public virtual string Mac
        {
            get
            {
                return mac;
            }
            set
            {
                this.mac = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ip")]
        public virtual string Ip
        {
            get
            {
                return ip;
            }
            set
            {
                this.ip = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "")]
        public virtual string SubnetMask
        {
            get
            {
                return subnetMask;
            }
            set
            {
                this.subnetMask = value;
            }
        }


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


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + (enabled ? 1231 : 1237);
            result = prime * result + ((string.ReferenceEquals(ip, null)) ? 0 : ip.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(mac, null)) ? 0 : mac.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(name, null)) ? 0 : name.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(subnetMask, null)) ? 0 : subnetMask.GetHashCode());
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
            LAN other = (LAN)obj;
            if (enabled != other.enabled)
            {
                return false;
            }
            if (string.ReferenceEquals(ip, null))
            {
                if (!string.ReferenceEquals(other.ip, null))
                {
                    return false;
                }
            }
            else if (!ip.Equals(other.ip))
            {
                return false;
            }
            if (string.ReferenceEquals(mac, null))
            {
                if (!string.ReferenceEquals(other.mac, null))
                {
                    return false;
                }
            }
            else if (!mac.Equals(other.mac))
            {
                return false;
            }
            if (string.ReferenceEquals(name, null))
            {
                if (!string.ReferenceEquals(other.name, null))
                {
                    return false;
                }
            }
            else if (!name.Equals(other.name))
            {
                return false;
            }
            if (string.ReferenceEquals(subnetMask, null))
            {
                if (!string.ReferenceEquals(other.subnetMask, null))
                {
                    return false;
                }
            }
            else if (!subnetMask.Equals(other.subnetMask))
            {
                return false;
            }
            return true;
        }
    }
}
