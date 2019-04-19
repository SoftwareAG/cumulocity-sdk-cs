using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_PushButton")]
    public class PushButton
    {

        private int duration;

        private string button;

        [JsonProperty(PropertyName = "duration")]
        public virtual int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                this.duration = value;
            }
        }

        [JsonProperty(PropertyName = "button")]
        public virtual string Button
        {
            get
            {
                return button;
            }
            set
            {
                this.button = value;
            }
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((string.ReferenceEquals(button, null)) ? 0 : button.GetHashCode());
            result = prime * result + duration;
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
            PushButton other = (PushButton)obj;
            if (string.ReferenceEquals(button, null))
            {
                if (!string.ReferenceEquals(other.button, null))
                {
                    return false;
                }
            }
            else if (!button.Equals(other.button))
            {
                return false;
            }
            if (duration != other.duration)
            {
                return false;
            }
            return true;
        }


    }
}
