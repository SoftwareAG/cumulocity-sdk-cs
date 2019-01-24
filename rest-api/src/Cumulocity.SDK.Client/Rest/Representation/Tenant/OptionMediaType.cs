namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class OptionMediaType : CumulocityMediaType
	{
		public static readonly OptionMediaType OPTION = new OptionMediaType("option");

		public static readonly string OPTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "option+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly OptionMediaType OPTION_COLLECTION = new OptionMediaType("optionCollection");

		public static readonly string OPTION_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "optionCollection+json;"
																								  + VND_COM_NSN_CUMULOCITY_PARAMS;

		public OptionMediaType(string entity) : base(entity)
		{
		}
	}
}