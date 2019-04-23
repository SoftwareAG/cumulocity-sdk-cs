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

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_SmsInformation")]
    public class SmsInformation
    {

        private string sender;
        private string receiver;
        private string @operator;
        private string data;

        public SmsInformation()
        {
        }

        public SmsInformation(string sender, string receiver, string @operator)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.@operator = @operator;
        }

        public SmsInformation(string sender, string receiver, string @operator, string data) : this(sender, receiver, @operator)
        {
            this.data = data;
        }

        public virtual string Sender
        {
            get
            {
                return sender;
            }
            set
            {
                this.sender = value;
            }
        }
        public virtual string Receiver
        {
            get
            {
                return receiver;
            }
            set
            {
                this.receiver = value;
            }
        }
        public virtual string Operator
        {
            get
            {
                return @operator;
            }
            set
            {
                this.@operator = value;
            }
        }

        public virtual string Data
        {
            get
            {
                return data;
            }
            set
            {
                this.data = value;
            }
        }


        public override int GetHashCode()
        {
            int hash = 3;
            hash = 53 * hash + (!string.ReferenceEquals(this.sender, null) ? this.sender.GetHashCode() : 0);
            hash = 53 * hash + (!string.ReferenceEquals(this.receiver, null) ? this.receiver.GetHashCode() : 0);
            hash = 53 * hash + (!string.ReferenceEquals(this.@operator, null) ? this.@operator.GetHashCode() : 0);
            hash = 53 * hash + (!string.ReferenceEquals(this.data, null) ? this.data.GetHashCode() : 0);
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
            //ORIGINAL LINE: final SmsInformation other = (SmsInformation) obj;
            SmsInformation other = (SmsInformation)obj;
            if ((string.ReferenceEquals(this.sender, null)) ? (!string.ReferenceEquals(other.sender, null)) : !this.sender.Equals(other.sender))
            {
                return false;
            }
            if ((string.ReferenceEquals(this.receiver, null)) ? (!string.ReferenceEquals(other.receiver, null)) : !this.receiver.Equals(other.receiver))
            {
                return false;
            }
            if ((string.ReferenceEquals(this.@operator, null)) ? (!string.ReferenceEquals(other.@operator, null)) : !this.@operator.Equals(other.@operator))
            {
                return false;
            }
            if ((string.ReferenceEquals(this.data, null)) ? (!string.ReferenceEquals(other.data, null)) : !this.data.Equals(other.data))
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "SmsInformation{" + "sender=" + sender + ", receiver=" + receiver + ", operator=" + @operator + ", data=" + data + '}';
        }

    }
}
