using Cumulocity.MQTT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Model
{
    public class InventoryGetRequest : Request
    {
        private readonly IList<CustomValue> _customValues = new List<CustomValue>();
        private readonly bool _byId;
        private readonly string _externalIdType;
        private readonly bool? _response;

        public InventoryGetRequest(string messageId, bool? response, string externalIdType, bool byId) : base(messageId, ValidApis.Inventory, HttpMethods.GET)
        {
            if (!byId && String.IsNullOrEmpty(externalIdType))
                throw new ArgumentException(nameof(externalIdType));
            _externalIdType = externalIdType;
            _byId = byId;
            _response = response;
        }

        private string PreTemplate(string method)
        {
            return String.Format("10,{0},{1},INVENTORY,{2}", _messageId, method, _response == null ? String.Empty : _response.ToString());
        }

        private string GetExternalIdType()
        {
            if (_byId)
            {
                return ",true";
            }
            else
            {
                if (!String.IsNullOrEmpty(_externalIdType))
                {
                    return String.Format(",false,{0}", _externalIdType);
                }
                else
                {
                    return ",false";
                }
            }
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
            if (method.ToLower().Equals("get"))
            {
                return String.Concat(PreTemplate(method), GetExternalIdType());
            }
            else if (method.ToLower().Equals("put"))
            {
                return String.Concat(PreTemplate(method), GetExternalIdType(), PostTemplate());
            }
            return String.Empty;
        }
    }
}