using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model
{
    [PackageName("com_cumulocity_model_Agent")]
    public class Agent
    {

        /// <summary>
        /// Agent address; either IP address or host name.
        /// </summary>
        private string address;

        /// <summary>
        /// Agent TCP port number.
        /// </summary>
        private int? port;

        /// <summary>
        /// Indicate whether the agent is responsible for the life cycle of the
        /// managed object.
        /// </summary>
        private bool? primary;


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Address
        {
            get
            {
                return address;
            }
            set
            {
                this.address = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual int? Port
        {
            get
            {
                return port;
            }
            set
            {
                this.port = value;
            }
        }


        /// <summary>
        /// @deprecated
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? Primary
        {
            get
            {
                return primary;
            }
            set
            {
                this.primary = value;
            }
        }

    }

}