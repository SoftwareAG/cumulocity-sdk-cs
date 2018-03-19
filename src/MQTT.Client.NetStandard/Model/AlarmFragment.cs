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

using System;
using Cumulocity.MQTT.Enums;

namespace Cumulocity.MQTT.Model
{
    public class AlarmFragment
    {
        private readonly AlarmStatus? _alarmStatus;
        private readonly string _key;

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
                return string.Format(",{0},ALARMSTATUS,{1}", _key, _alarmStatus);
            return string.Empty;
        }
    }
}