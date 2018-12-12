namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
    public class DeviceControlMediaType : CumulocityMediaType
    {
        public static readonly DeviceControlMediaType OPERATION = new DeviceControlMediaType("operation");

        public static readonly string OPERATION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "operation+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType OPERATION_COLLECTION =
            new DeviceControlMediaType("operationCollection");

        public static readonly string OPERATION_COLLECTION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "operationCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType DEVICE_CONTROL_API =
            new DeviceControlMediaType("devicecontrolApi");

        public static readonly string DEVICE_CONTROL_API_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "devicecontrolApi+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType NEW_DEVICE_REQUEST =
            new DeviceControlMediaType("newDeviceRequest");

        public static readonly string NEW_DEVICE_REQUEST_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "newDeviceRequest+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType NEW_DEVICE_REQUEST_COLLECTION =
            new DeviceControlMediaType("newDeviceRequestCollection");

        public static readonly string NEW_DEVICE_REQUEST_COLLECTION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "newDeviceRequestCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType DEVICE_CREDENTIALS =
            new DeviceControlMediaType("deviceCredentials");

        public static readonly string DEVICE_CREDENTIALS_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "deviceCredentials+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType DEVICE_CREDENTIALS_COLLECTION =
            new DeviceControlMediaType("deviceCredentialsCollection");

        public static readonly string DEVICE_CREDENTIALS_COLLECTION_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "deviceCredentialsCollection+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public static readonly DeviceControlMediaType BULK_NEW_DEVICE_REQUEST =
            new DeviceControlMediaType("bulkNewDeviceRequest");

        public static readonly string BULK_NEW_DEVICE_REQUEST_TYPE =
            APPLICATION_VND_COM_NSN_CUMULOCITY + "bulkNewDeviceRequest+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

        public DeviceControlMediaType(string @string) : base(@string)
        {
        }
    }
}