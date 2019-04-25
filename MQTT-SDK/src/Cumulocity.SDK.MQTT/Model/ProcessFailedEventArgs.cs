using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public class ProcessFailedEventArgs: EventArgs
    {
        public ProcessFailedEventArgs(System.Exception exception)
        {
            Exception = exception;
        }

        public System.Exception Exception { get; }
    }
}
