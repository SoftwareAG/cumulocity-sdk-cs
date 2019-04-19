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
    [PackageName("c8y_AddressRange")]
    public class AddressRange
    {
        private string start;
        private string end;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "start")]
        public virtual string Start
        {
            get
            {
                return start;
            }
            set
            {
                this.start = value;
            }
        }

        //ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getEnd()
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "end")]
        public virtual string End
        {
            get
            {
                return end;
            }
            set
            {
                this.end = value;
            }
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((string.ReferenceEquals(end, null)) ? 0 : end.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(start, null)) ? 0 : start.GetHashCode());
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
            AddressRange other = (AddressRange)obj;
            if (string.ReferenceEquals(end, null))
            {
                if (!string.ReferenceEquals(other.end, null))
                {
                    return false;
                }
            }
            else if (!end.Equals(other.end))
            {
                return false;
            }
            if (string.ReferenceEquals(start, null))
            {
                if (!string.ReferenceEquals(other.start, null))
                {
                    return false;
                }
            }
            else if (!start.Equals(other.start))
            {
                return false;
            }
            return true;
        }

    }
}
