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