using Cumulocity.SDK.Client.Rest.Model.Idtype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public class NotificationSubscriptionFilter : Filter
    {
        [ParamSource]
        private string source;

        [ParamSource]
        private string context;

        public NotificationSubscriptionFilter bySource(GId source)
        {
            this.source = source.Value;
            return this;
        }

        public NotificationSubscriptionFilter byContext(String context)
        {
            this.context = context;
            return this;
        }

        public string getSource()
        {
            return source;
        }

        public string getContext()
        {
            return context;
        }
    }
}
