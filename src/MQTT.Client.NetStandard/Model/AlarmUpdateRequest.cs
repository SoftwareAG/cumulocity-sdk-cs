using Cumulocity.MQTT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Model
{
    public class AlarmUpdateRequest : Request
    {
        private readonly AlarmFragment _alarmFragment;
        private readonly IList<CustomValue> _customValues;
        private readonly bool? _response;
        private readonly string _type;

        public AlarmUpdateRequest(string messageId, bool? response, string type, AlarmFragment alarmFragment, IList<CustomValue> customValues) : base(messageId, ValidApis.Alarm, HttpMethods.PUT)
        {
            _type = type;
            _customValues = customValues;
            _response = response;
            _alarmFragment = alarmFragment;
        }

        public override string RequestTemplate()
        {
            string method = base._httpMethods.ToString();
            return String.Concat(PreTemplate(method), AlarmFragmentTemplate(), PostTemplate());
        }

        private string AlarmFragmentTemplate()
        {
            return _alarmFragment.ToString();
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
            return String.Format("10,{0},{1},ALARM,{2},{3}", _messageId, method, _response == null ? String.Empty : _response.ToString(), _type);
        }
    }
}