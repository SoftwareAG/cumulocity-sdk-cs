using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class BufferedRequest
    {
        private const string SEPARATOR = "##";

        private static readonly IDictionary<string, CumulocityMediaType> toMediaTypeMappings =
            new Dictionary<string, CumulocityMediaType>();

        private static readonly IDictionary<CumulocityMediaType, string> fromMediaTypeMappings =
            new Dictionary<CumulocityMediaType, string>();

        private static readonly IDictionary<string, Type> toClassMappings = new Dictionary<string, Type>();
        private static readonly IDictionary<Type, string> fromClassMappings = new Dictionary<Type, string>();
        private readonly CumulocityMediaType mediaType;
        private readonly string method;

        private readonly string path;
        private readonly IResourceRepresentation representation;

        static BufferedRequest()
        {
            toMediaTypeMappings["A"] = AlarmMediaType.ALARM;
            toMediaTypeMappings["E"] = EventMediaType.EVENT;
            toMediaTypeMappings["M"] = MeasurementMediaType.MEASUREMENT;
            toMediaTypeMappings["D"] = DeviceControlMediaType.OPERATION;

            fromMediaTypeMappings[AlarmMediaType.ALARM] = "A";
            fromMediaTypeMappings[EventMediaType.EVENT] = "E";
            fromMediaTypeMappings[MeasurementMediaType.MEASUREMENT] = "M";
            fromMediaTypeMappings[DeviceControlMediaType.OPERATION] = "D";

            toClassMappings["A"] = typeof(AlarmRepresentation);
            toClassMappings["E"] = typeof(EventRepresentation);
            toClassMappings["M"] = typeof(MeasurementRepresentation);
            toClassMappings["D"] = typeof(OperationRepresentation);

            fromClassMappings[typeof(AlarmRepresentation)] = "A";
            fromClassMappings[typeof(EventRepresentation)] = "E";
            fromClassMappings[typeof(MeasurementRepresentation)] = "M";
            fromClassMappings[typeof(OperationRepresentation)] = "D";
        }

        public BufferedRequest()
        {
        }

        private BufferedRequest(string method, string path, CumulocityMediaType mediaType,
            IResourceRepresentation representation)
        {
            this.method = method;
            this.path = path;
            this.mediaType = mediaType;
            this.representation = representation;
        }

        public virtual string Path => path;

        public virtual CumulocityMediaType MediaType => mediaType;

        public virtual IResourceRepresentation Representation => representation;

        public virtual string Method => method;

        public static BufferedRequest create(string method, string path, CumulocityMediaType mediaType,
            IResourceRepresentation representation)
        {
            if (!fromClassMappings.ContainsKey(representation.GetType()))
                throw new ArgumentException("Cannot create buffered request from class " + representation.GetType());
            return new BufferedRequest(method, path, mediaType, representation);
        }

        public static BufferedRequest parseCsvString(string content)
        {
            var parts = content.Split(new[] {SEPARATOR}, 5, StringSplitOptions.None);
            return new BufferedRequest(parts[0], parts[1], toMediaTypeMappings[parts[2]], null);
        }

        public virtual string toCsvString()
        {
            return method + SEPARATOR + path + SEPARATOR + fromMediaTypeMappings[mediaType] + SEPARATOR +
                   fromClassMappings[representation.GetType()] + SEPARATOR +
                   JsonConvert.SerializeObject(representation);
        }
    }
}