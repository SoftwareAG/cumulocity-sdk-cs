namespace Cumulocity.MQTT.Enums
{
    public enum ValidApis
    {
        Inventory,
        Measurement,
        Alarm,
        Event,
        Operation
    }

    public enum HttpMethods
    {
        GET,
        PUT,
        POST
    }

    public enum CustomValueType
    {
        STRING,
        DATE,
        NUMBER,
        INTEGER,
        UNSIGNED,
        FLAG,
        SEVERITY,
        ALARMSTATUS,
        OPERATIONSTATUS
    }

    public enum OperationStatus
    {
        PENDING,
        EXECUTING,
        SUCCESSFUL,
        FAILED
    }

    public enum AlarmStatus
    {
        ACTIVE,
        ACKNOWLEDGED,
        CLEARED
    }

    public enum ProcessingMode
    {
        PERSISTENT,
        TRANSIENT
    }
}