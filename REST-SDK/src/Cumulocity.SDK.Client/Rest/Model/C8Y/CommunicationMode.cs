using System;
using System.Collections.Generic;
using System.Text;
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
