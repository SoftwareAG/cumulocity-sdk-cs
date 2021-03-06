using System;
using System.Collections.Generic;
using Cometd.Bayeux;
using Cometd.Common;
using Cometd.cometd.common;

namespace Cometd.Client.Transport
{
    /// <version> $Revision: 902 $ $Date: 2011-03-10 15:02:59 +0100 (Thu, 10 Mar 2011) $
    /// </version>
    public abstract class ClientTransport : AbstractTransport
    {
        public const String TIMEOUT_OPTION = "timeout";
        public const String INTERVAL_OPTION = "interval";
        public const String MAX_NETWORK_DELAY_OPTION = "maxNetworkDelay";
        public const String JSON_CONTEXT = "jsonContext";
        private IClient jsonContext;
        public ClientTransport(String name, IDictionary<String, Object> options)
            : base(name, options)
        {
        }


        public virtual void init()
        {
            jsonContext = (IClient)getOption(JSON_CONTEXT);
            //TODO: JettyJSONContextClient
            //if (jsonContext == null)
            //    jsonContext = new JettyJSONContextClient();
        }

        public abstract void abort();

        public abstract void reset();

        public abstract bool accept(String version);

        public abstract void send(ITransportListener listener, IList<IMutableMessage> messages);

        public abstract bool isSending { get; }
    }
}
