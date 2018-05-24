#region Cumulocity GmbH

// /*
//  * Copyright (C) 2015-2018
//  *
//  * Permission is hereby granted, free of charge, to any person obtaining a copy of
//  * this software and associated documentation files (the "Software"),
//  * to deal in the Software without restriction, including without limitation the rights to use,
//  * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
//  * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  *
//  * The above copyright notice and this permission notice shall be
//  * included in all copies or substantial portions of the Software.
//  *
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  */

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cumulocity.MQTT.Enums;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Model
{
    public class InventoryRequest : Request
    {
        private readonly bool _byId;
        private readonly IList<CustomValue> _customValues;
        private readonly string _externalIdType;
        private readonly bool? _response;

        public InventoryRequest(string messageId, bool? response, string externalIdType,
            IList<CustomValue> customValues, HttpMethods method) : base(messageId, ValidApis.Inventory, method)
        {
            _externalIdType = externalIdType;
            _customValues = customValues;
            _response = response;
        }

        private string PreTemplate(string method)
        {
            return string.Format("10,{0},{1},INVENTORY,{2}", _messageId, method,
                _response == null ? string.Empty : _response.ToString());
        }

        private string GetExternalIdType()
        {
            if (_byId) return ",true";

            if (!string.IsNullOrEmpty(_externalIdType))
                return string.Format(",false,{0}", _externalIdType);
            return ",false";
        }

        private string PostExternalIdType()
        {
            return string.Concat(",", _externalIdType.Trim());
        }

        private string PostTemplate()
        {
            var result = new StringBuilder();
            if (_customValues != null && _customValues.Any())
                foreach (var item in _customValues)
                    result.Append(item.CustomValueAsString);
            return result.ToString();
        }

        public override string RequestTemplate()
        {
            var method = _httpMethods.ToString();

            if (method.ToLower().Equals("post"))
                return string.Concat(PreTemplate(method), PostExternalIdType(), PostTemplate());
            if (method.ToLower().Equals("put"))
                return string.Concat(PreTemplate(method), GetExternalIdType(), PostTemplate());
            return string.Empty;
        }
    }
}