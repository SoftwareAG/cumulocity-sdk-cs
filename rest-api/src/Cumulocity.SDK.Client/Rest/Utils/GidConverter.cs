using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cumulocity.SDK.Client.Rest.Utils
{
    public class GidConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var id = (GId) value;
            var strVal = id.toJSON();
            writer.WriteValue(strVal);         
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {			
            var id = (string)reader.Value;
            GId g = new GId(id);
            return g;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}