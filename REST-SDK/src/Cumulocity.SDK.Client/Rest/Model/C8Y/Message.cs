using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Message")]
    public class Message { 
        private string text;

        public Message()
        {
        }

        public Message(string text)
        {
            this.text = text;
        }
        [JsonProperty(PropertyName = "text")]
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                this.text = value;
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
            if (!(obj is Message))
            {
                return false;
            }

            Message rhs = (Message)obj;
            return string.ReferenceEquals(text, null) ? string.ReferenceEquals(rhs.text, null) : text.Equals(rhs.text);
        }

        public override int GetHashCode()
        {
            return string.ReferenceEquals(text, null) ? 0 : text.GetHashCode();
        }
    }
}
