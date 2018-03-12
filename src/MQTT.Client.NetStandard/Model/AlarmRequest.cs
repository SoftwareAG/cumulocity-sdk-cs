using Cumulocity.MQTT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Model
{
    public class AlarmRequest : Request
    {
        private readonly string _type;
        private readonly string _text;
        private readonly string _status;
        private readonly string _severity;
        private readonly string _time;
        private readonly IList<CustomValue> _customValues;

        private readonly bool? _response;

        public AlarmRequest(string messageId, bool? response, string type, string text, string status, string severity, string time, IList<CustomValue> customValues, HttpMethods method) : base(messageId, ValidApis.Alarm, method)
        {
            _type = type;
            _text = text;
            _status = status;
            _severity = severity;
            _time = time;
            _customValues = customValues;
            _response = response;
        }

        public override string RequestTemplate()
        {
            string method = base._httpMethods.ToString();
            return String.Concat(PreTemplate(method), PostTemplate());
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

        private string PreTemplate(string method)
        {
            return String.Format("10,{0},{1},ALARM,{2},{3},{4},{5},{6},{7}", _messageId, method, _response == null ? String.Empty : _response.ToString(), _type, _text, _status, _severity, _time);
        }
    }
}