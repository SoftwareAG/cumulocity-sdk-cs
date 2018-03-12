using Cumulocity.MQTT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Model
{
    public class OperationRequest : Request
    {
        private readonly IList<CustomValue> _customValues;
        private readonly OperationFragment _operationFragment;
        private readonly bool? _response;
        private readonly string _type;

        public OperationRequest(string messageId, bool? response, string type, OperationFragment operationFragment, IList<CustomValue> customValues) : base(messageId, ValidApis.Operation, HttpMethods.PUT)
        {
            _type = type;
            _operationFragment = operationFragment;
            _customValues = customValues;
            _response = response;
        }

        public override string RequestTemplate()
        {
            string method = base._httpMethods.ToString();
            return String.Concat(PreTemplate(method), OperationFragmentTemplate(), PostTemplate());
        }

        private string OperationFragmentTemplate()
        {
            return _operationFragment.ToString();
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
            return String.Format("10,{0},{1},OPERATION,{2},{3}", _messageId, method, _response == null ? String.Empty : _response.ToString(), _type);
        }
    }
}