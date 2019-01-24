namespace Cumulocity.SDK.Client.Rest.Representation.Cep
{
	/// <summary>
	/// We follow here convention from <seealso cref="MediaType"/> class, where we have both <seealso cref="MediaType"/>
	/// instances, and string representations (with '_TYPE' suffix in name).
	/// </summary>
	public class CepMediaType : CumulocityMediaType
	{
		public static readonly CepMediaType CEP_MODULE = new CepMediaType("cepModule");

		public static readonly string CEP_MODULE_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "cepModule+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly CepMediaType CEP_MODULE_COLLECTION = new CepMediaType("cepModuleCollection");

		public static readonly string CEP_MODULE_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "cepModuleCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly CepMediaType CEP_STATEMENT = new CepMediaType("cepStatement");

		public static readonly string CEP_STATEMENT_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "cepStatement+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly CepMediaType CEP_STATEMENT_COLLECTION = new CepMediaType("cepStatementCollection");

		public static readonly string CEP_STATEMENT_COLLECTION_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "cepStatementCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly CepMediaType CEP_API = new CepMediaType("cepApi");

		public static readonly string CEP_API_TYPE = APPLICATION_VND_COM_NSN_CUMULOCITY + "cepApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public CepMediaType(string entity) : base("application", VND_COM_NSN_CUMULOCITY + entity + "+json;" + VND_COM_NSN_CUMULOCITY_PARAMS)
		{
		}
	}
}