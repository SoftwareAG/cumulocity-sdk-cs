using Cumulocity.MQTT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Model
{
    public class MeasurementRequest : Request
    {
        private readonly string _type;
        private readonly string _time;
        private readonly bool? _response;

        private readonly IList<CustomValue> _customValues;

        public MeasurementRequest(string messageId, bool? response, string type, string time, IList<CustomValue> customValues, HttpMethods method) : base(messageId, ValidApis.Inventory, method)
        {
            _type = type;
            _time = time;
            _customValues = customValues;
            _response = response;
        }

        private string PreTemplate(string method)
        {
            return String.Format("10,{0},{1},MEASUREMENT,{2},{3},{4}", _messageId, method, _response == null ? String.Empty : _response.ToString(), _type, _time);
        }

        private string PostTemplate()
        {
            StringBuilder result = new StringBuilder();
            if (_customValues != null && _customValues.Any())
            {
                foreach (var item in _customValues)
                {
                    result.Append(item.CustomValueAsString);
                }
            }
            return result.ToString();
        }

        public override string RequestTemplate()
        {
            string method = base._httpMethods.ToString();
            return String.Concat(PreTemplate(method), PostTemplate());
        }
    }
}