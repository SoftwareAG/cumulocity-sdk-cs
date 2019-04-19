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
    [PackageName("c8y_CommunicationMode")]

    public class CommunicationMode 
    {

        private string mode;

        public CommunicationMode()
        {
        }

        public CommunicationMode(string mode)
        {
            this.mode = mode;
        }
        [JsonProperty(PropertyName = "mode")]
        public virtual string Mode
        {
            get
            {
                return mode;
            }
            set
            {
                this.mode = value;
            }
        }


        public override int GetHashCode()
        {
            int hash = 7;
            hash = 79 * hash + (!string.ReferenceEquals(this.mode, null) ? this.mode.GetHashCode() : 0);
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            CommunicationMode other = (CommunicationMode)obj;
            return !((string.ReferenceEquals(this.mode, null)) ? (!string.ReferenceEquals(other.mode, null)) : !this.mode.Equals(other.mode));
        }

    }
}
