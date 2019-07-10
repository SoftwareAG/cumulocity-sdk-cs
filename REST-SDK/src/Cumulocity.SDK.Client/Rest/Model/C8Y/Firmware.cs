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

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
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

        [JsonProperty("name")]
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
        [JsonProperty("version")]
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

            Firmware rhs = (Firmware)obj;
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