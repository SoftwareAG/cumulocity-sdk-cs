using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public enum QoS
    {
        AT_MOST_ONCE = 0,
        AT_LEAST_ONCE = 1,
        EXACTLY_ONCE = 2
    }
}
