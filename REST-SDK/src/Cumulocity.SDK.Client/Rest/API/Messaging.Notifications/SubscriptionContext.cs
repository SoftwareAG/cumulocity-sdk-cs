using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public enum SubscriptionContext
    {
        [Description("mo")]
        MANAGED_OBJECT,
        [Description("tenant")]
        TENANT
    }
}
