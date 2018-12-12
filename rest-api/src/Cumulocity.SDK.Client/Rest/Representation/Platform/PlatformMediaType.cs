namespace Cumulocity.SDK.Client.Rest.Representation.Platform
{
    public class PlatformMediaType : CumulocityMediaType
    {

        public static readonly PlatformMediaType PLATFORM_API = new PlatformMediaType("platformApi");

        public static readonly string PLATFORM_API_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "platformApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public PlatformMediaType(string @string) : base(@string)
        {
        }
    }
}