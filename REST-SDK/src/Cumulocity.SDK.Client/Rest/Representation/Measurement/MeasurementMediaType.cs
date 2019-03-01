namespace Cumulocity.SDK.Client.Rest.Representation.Measurement
{
	public class MeasurementMediaType : CumulocityMediaType
	{
		public static readonly MeasurementMediaType MEASUREMENT = new MeasurementMediaType("measurement");

		public static readonly string MEASUREMENT_TYPE =
			APPLICATION_VND_COM_NSN_CUMULOCITY + "measurement+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly MeasurementMediaType MEASUREMENT_COLLECTION =
			new MeasurementMediaType("measurementCollection");

		public static readonly string MEASUREMENT_COLLECTION_TYPE =
			APPLICATION_VND_COM_NSN_CUMULOCITY + "measurementCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public static readonly MeasurementMediaType MEASUREMENT_API = new MeasurementMediaType("measurementApi");

		public static readonly string MEASUREMENT_API_TYPE =
			APPLICATION_VND_COM_NSN_CUMULOCITY + "measurementApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		public MeasurementMediaType(string entity) : base("application",
			VND_COM_NSN_CUMULOCITY + entity + "+json;" + VND_COM_NSN_CUMULOCITY_PARAMS)
		{
		}
	}
}