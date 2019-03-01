using System;
using System.Collections.Generic;

namespace Cometd.Client.Transport
{
    /// <version>  $Revision$ $Date: 2010-01-20 08:02:44 +0100 (Wed, 20 Jan 2010) $
    /// </version>
    [Serializable]
    public class TransportException : SystemException
    {
        private IDictionary<string, object> failure;

        public TransportException()
        {
        }

        public TransportException(String message)
            : base(message)
        {
        }

        public TransportException(IDictionary<string, object> failure)
        {
            this.failure = failure;
        }

        public TransportException(String message, Exception cause)
            : base(message, cause)
        {
        }

        /*public TransportException(Exception cause): base(cause)
        {
        }*/
    }
}