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
    [PackageName("c8y_WAN")]
    public class WAN 
    {

        private string simStatus;
        private string apn;
        private string username;
        private string password;
        private string authType;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "simStatus")]
        public virtual string SimStatus
        {
            get
            {
                return simStatus;
            }
            set
            {
                this.simStatus = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "apn")]
        public virtual string Apn
        {
            get
            {
                return apn;
            }
            set
            {
                this.apn = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "username")]
        public virtual string Username
        {
            get
            {
                return username;
            }
            set
            {
                this.username = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "password")]
        public virtual string Password
        {
            get
            {
                return password;
            }
            set
            {
                this.password = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "authType")]
        public virtual string AuthType
        {
            get
            {
                return authType;
            }
            set
            {
                this.authType = value;
            }
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((string.ReferenceEquals(apn, null)) ? 0 : apn.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(authType, null)) ? 0 : authType.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(password, null)) ? 0 : password.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(simStatus, null)) ? 0 : simStatus.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(username, null)) ? 0 : username.GetHashCode());
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
            WAN other = (WAN)obj;
            if (string.ReferenceEquals(apn, null))
            {
                if (!string.ReferenceEquals(other.apn, null))
                {
                    return false;
                }
            }
            else if (!apn.Equals(other.apn))
            {
                return false;
            }
            if (string.ReferenceEquals(authType, null))
            {
                if (!string.ReferenceEquals(other.authType, null))
                {
                    return false;
                }
            }
            else if (!authType.Equals(other.authType))
            {
                return false;
            }
            if (string.ReferenceEquals(password, null))
            {
                if (!string.ReferenceEquals(other.password, null))
                {
                    return false;
                }
            }
            else if (!password.Equals(other.password))
            {
                return false;
            }
            if (string.ReferenceEquals(simStatus, null))
            {
                if (!string.ReferenceEquals(other.simStatus, null))
                {
                    return false;
                }
            }
            else if (!simStatus.Equals(other.simStatus))
            {
                return false;
            }
            if (string.ReferenceEquals(username, null))
            {
                if (!string.ReferenceEquals(other.username, null))
                {
                    return false;
                }
            }
            else if (!username.Equals(other.username))
            {
                return false;
            }
            return true;
        }
    }

}
