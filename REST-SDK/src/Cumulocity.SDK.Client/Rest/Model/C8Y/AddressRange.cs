using System;
using System.Collections.Generic;
using System.Text;
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
