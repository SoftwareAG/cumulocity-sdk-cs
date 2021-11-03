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
        /// <summary>
        /// Creates new access Token.
        /// </summary>
        /// <param name="tokenRequest"> containing claim - subscriber, subscription and desired validity duration for the Token.
        /// </param>
        /// <returns>generated Token with JWT Token string</returns>
        /// <exception cref="SDKException"> if the Token could not be created </exception>
        /// <exception cref="ArgumentException">if the tokenClaim is null</exception>
        Token create(NotificationTokenRequestRepresentation tokenRequest);

        /// <summary>
        /// Verifies supplied Token.
        /// </summary>
        /// <param name="token"> to be verified
        /// </param>
        /// <returns>TokenClaim if the supplied Token was successfully verified</returns>
        /// <exception cref="SDKException">if the Token failed verification or could not be verified </exception>
        TokenClaims verify(Token token);

        /// <summary>
        /// Refreshes an expired Token.
        /// </summary>
        /// <param name="token"> to be refreshed
        /// </param>
        /// <returns>refreshed Token, valid for the same duration of time as the original Token</returns>
        /// <exception cref="SDKException"> if the Token wasn't valid or the operation fails </exception>
        /// <exception cref="ArgumentException">if the supplied Token is null</exception>
        Token refresh(Token token);
    }
}
