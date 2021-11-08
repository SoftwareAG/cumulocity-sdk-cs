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

        /// <summary>
        /// Specifies the <code>source</code> query parameter
        /// </summary>
        /// <param name="source">the managed object that has been subscribed to
        /// </param>
        /// <returns>the filter with <code>source</code> set</returns>
        public NotificationSubscriptionFilter bySource(GId source)
        {
            this.source = source.Value;
            return this;
        }

        /// <summary>
        /// Specifies the <code>context</code> query parameter
        /// </summary>
        /// <param name="context">the context to which the subscription is associated with.
        /// </param>
        /// <returns>the filter with <code>context</code> set</returns>
        public NotificationSubscriptionFilter byContext(String context)
        {
            this.context = context;
            return this;
        }
        /// <summary>
        /// </summary>
        /// <returns><code>source</code> parameter of the query</returns>
        public string getSource()
        {
            return source;
        }
        /// <summary>
        /// </summary>
        /// <returns><code>context</code> parameter of the query</returns>
        public string getContext()
        {
            return context;
        }
    }
}
