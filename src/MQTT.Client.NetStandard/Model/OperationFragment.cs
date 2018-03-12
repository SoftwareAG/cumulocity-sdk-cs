using Cumulocity.MQTT.Enums;
using System;

namespace Cumulocity.MQTT.Model
{
    public class OperationFragment
    {
        private readonly string _key;
        private readonly OperationStatus? _operation;

        public OperationFragment(string key, OperationStatus? operation)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("The field can not be empty!", nameof(key));

            if (operation == null)
                throw new ArgumentException("The field can not be null!", nameof(operation));

            _key = key;
            _operation = operation;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_key) && _operation != null)
            {
                return string.Format(",{0},OPERATIONSTATUS,{1}", _key, _operation);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}