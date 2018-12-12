namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
    public class AlarmMediaType : CumulocityMediaType
    {
        public static readonly AlarmMediaType ALARM = new AlarmMediaType("alarm");

        public static readonly string ALARM_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "alarm+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly AlarmMediaType ALARM_COLLECTION = new AlarmMediaType("alarmCollection");

        public static readonly string ALARM_COLLECTION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "alarmCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly AlarmMediaType ALARM_API = new AlarmMediaType("alarmApi");

        public static readonly string ALARM_API_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "alarmApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly string AVAILABILITY_STAT_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "availabilityStat+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly string AVAILABILITY_STAT_COLLECTION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "availabilityStatCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public AlarmMediaType(string @string) : base(@string)
        {
        }
    }
}