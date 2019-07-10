using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public ClientConnectedEventArgs(bool isSessionPresent)
        {
            IsSessionPresent = isSessionPresent;
        }

        public bool IsSessionPresent { get; }
    }
}
