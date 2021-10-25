using Cumulocity.SDK.Client.Rest.Representation.Messaging.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Messaging.Notifications
{
    public interface ITokenApi
    {
        Token create(NotificationTokenRequestRepresentation tokenRequest);

        TokenClaims verify(Token token);

        Token refresh(Token token);
    }
}
