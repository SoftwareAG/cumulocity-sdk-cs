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
    [PackageName("c8y_Hardware")]
    public class Hardware
    {
        private string model;
        private string serialNumber;
        private string revision;

        public Hardware()
        {
        }

        public Hardware(string model, string serialNumber, string revision)
        {
            this.model = model;
            this.serialNumber = serialNumber;
            this.revision = revision;
        }
        [JsonProperty("model")]
        public virtual string Model
        {
            get
            {
                return model;
            }
            set
            {
                this.model = value;
            }
        }
        [JsonProperty("serialNumber")]
        public virtual string SerialNumber
        {
            get
            {
                return serialNumber;
            }
            set
            {
                this.serialNumber = value;
            }
        }
        [JsonProperty("revision")]
        public virtual string Revision
        {
            get
            {
                return revision;
            }
            set
            {
                this.revision = value;
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

            Hardware rhs = (Hardware)obj;
            return (string.ReferenceEquals(model, null) ? string.ReferenceEquals(rhs.Model, null) : model.Equals(rhs.Model)) && (string.ReferenceEquals(serialNumber, null) ? string.ReferenceEquals(rhs.SerialNumber, null) : serialNumber.Equals(rhs.SerialNumber)) && (string.ReferenceEquals(revision, null) ? string.ReferenceEquals(rhs.Revision, null) : revision.Equals(rhs.Revision));
        }

        public override int GetHashCode()
        {
            int result = !string.ReferenceEquals(model, null) ? model.GetHashCode() : 0;
            result = 31 * result + (!string.ReferenceEquals(serialNumber, null) ? serialNumber.GetHashCode() : 0);
            result = 31 * result + (!string.ReferenceEquals(revision, null) ? revision.GetHashCode() : 0);
            return result;
        }
    }
}