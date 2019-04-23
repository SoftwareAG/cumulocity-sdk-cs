/*
 * Copyright (C) 2019 Cumulocity GmbH
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
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
