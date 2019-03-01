using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Model;

namespace Cumulocity.SDK.Client.Rest.Representation
{
    public class
        BaseCumulocityMediaType : BaseMediaType 
    {
        protected internal const string VND_COM_NSN_CUMULOCITY = "vnd.com.nsn.cumulocity.";

        protected internal static readonly string APPLICATION_VND_COM_NSN_CUMULOCITY =
            "application/" + VND_COM_NSN_CUMULOCITY;

        public static readonly BaseCumulocityMediaType ERROR_MESSAGE = new BaseCumulocityMediaType("error");

        public static readonly string VND_COM_NSN_CUMULOCITY_CHARSET = "charset=" + CumulocityCharset_Fields.CHARSET;

        public static readonly string VND_COM_NSN_CUMULOCITY_VERSION =
            "ver=" + CumulocityVersionParameter_Fields.VERSION;

        public static readonly string VND_COM_NSN_CUMULOCITY_PARAMS =
            VND_COM_NSN_CUMULOCITY_CHARSET + ";" + VND_COM_NSN_CUMULOCITY_VERSION;

        public static readonly string ERROR_MESSAGE_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "error+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public BaseCumulocityMediaType()
        {
        }

        public BaseCumulocityMediaType(string type, string subtype) : base(type, subtype)
        {
        }

        public BaseCumulocityMediaType(string entity) : base("application",
            VND_COM_NSN_CUMULOCITY + entity + "+json;" + VND_COM_NSN_CUMULOCITY_PARAMS)
        {
        }

        public BaseCumulocityMediaType(string type, string subtype, IDictionary<string, string> parameters) : base(type,
            subtype, parameters)
        {
        }

        public virtual string TypeString => $"{Type}/{Subtype}";
    }
}