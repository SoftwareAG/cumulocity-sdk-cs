using Cumulocity.MQTT.Enums;
using System;

namespace Cumulocity.MQTT.Model
{
    public class AlarmFragment
    {
        private readonly string _key;
        private readonly AlarmStatus? _alarmStatus;

        public AlarmFragment(string key, AlarmStatus? alarmStatus)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("The field can not be empty!", nameof(key));

            _key = key;
            _alarmStatus = alarmStatus;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_key) && _alarmStatus != null)
            {
                return string.Format(",{0},ALARMSTATUS,{1}", _key, _alarmStatus);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}