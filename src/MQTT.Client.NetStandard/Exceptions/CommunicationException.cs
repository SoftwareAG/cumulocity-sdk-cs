using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.MQTT.Exceptions
{
    public class CommunicationException : Exception
    {
        protected CommunicationException()
        {
        }

        public CommunicationException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        public CommunicationException(string message)
            : base(message)
        {
        }

        public CommunicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
