namespace Cumulocity.SDK.Client.Rest.Representation.Event
{
    public class EventMediaType : CumulocityMediaType
    {
        public static readonly EventMediaType EVENT = new EventMediaType("event");

        public static readonly string EVENT_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "event+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly EventMediaType EVENT_COLLECTION = new EventMediaType("eventCollection");

        public static readonly string EVENT_COLLECTION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "eventCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly EventMediaType EVENT_API = new EventMediaType("eventApi");

        public static readonly string EVENTS_API_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "eventApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public EventMediaType(string @string) : base(@string)
        {
        }
    }
}