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

namespace Cumulocity.MQTT.Model
{
    public class Response
    {
        /// https://www.cumulocity.com/guides/reference/smartrest/
        /// <summary>
        ///     The base is the base JSON path pointing to an object or object list from which the values are extracted.
        /// </summary>
        private readonly string _base;

        /// <summary>
        ///     The cond is a conditional JSON path which gets checked for existance. Only if the path exists, values are
        ///     extracted.
        /// </summary>
        private readonly string _cond;

        /// <summary>
        ///     ID is the message identifier of the response template.
        /// </summary>
        private readonly string _id;

        /// <summary>
        ///     The values is a JSON path pointing to a value to extract within the base object or object in the base object list.
        ///     An unlimited number of VALUEs can be specified.
        /// </summary>
        private readonly IList<string> _values;

        public Response(string messageId, string baseObject, string condition, IList<string> values)
        {
            _id = messageId;
            _base = baseObject;
            _cond = condition;
            _values = values;
        }

        /// <summary>
        ///     The base is the base JSON path pointing to an object or object list from which the values are extracted.
        /// </summary>
        public string Base => _base;

        /// <summary>
        ///     The cond is a conditional JSON path which gets checked for existance. Only if the path exists, values are
        ///     extracted.
        /// </summary>
        public string Cond => _cond;

        /// <summary>
        ///     ID is the message identifier of the response template.
        /// </summary>
        public string Id => _id;

        /// <summary>
        ///     The values is a JSON path pointing to a value to extract within the base object or object in the base object list.
        ///     An unlimited number of VALUEs can be specified.
        /// </summary>
        public IList<string> Values => _values;
    }
}