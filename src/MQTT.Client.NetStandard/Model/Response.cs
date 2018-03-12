using System.Collections.Generic;

namespace Cumulocity.MQTT.Model
{
    public class Response
    {
        ///https://www.cumulocity.com/guides/reference/smartrest/

        /// <summary>
        /// The base is the base JSON path pointing to an object or object list from which the values are extracted.
        /// </summary>
        private readonly string _base;

        /// <summary>
        /// The cond is a conditional JSON path which gets checked for existance. Only if the path exists, values are extracted.
        /// </summary>
        private readonly string _cond;

        /// <summary>
        /// ID is the message identifier of the response template.
        /// </summary>
        private readonly string _id;

        /// <summary>
        /// The values is a JSON path pointing to a value to extract within the base object or object in the base object list. An unlimited number of VALUEs can be specified.
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
        /// The base is the base JSON path pointing to an object or object list from which the values are extracted.
        /// </summary>
        public string Base => _base;

        /// <summary>
        /// The cond is a conditional JSON path which gets checked for existance. Only if the path exists, values are extracted.
        /// </summary>
        public string Cond => _cond;

        /// <summary>
        /// ID is the message identifier of the response template.
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// The values is a JSON path pointing to a value to extract within the base object or object in the base object list. An unlimited number of VALUEs can be specified.
        /// </summary>
        public IList<string> Values => _values;
    }
}