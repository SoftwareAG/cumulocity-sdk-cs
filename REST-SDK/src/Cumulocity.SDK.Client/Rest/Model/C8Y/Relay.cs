/*
 * Copyright (C) 2019 Cumulocity GmbH
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Relay")]
    public class Relay
    {
        public enum RelayState
        {
            OPEN,
            CLOSED
        }

        private RelayState _relayState;

        /// <returns> the relayState </returns>
        public RelayState getRelayState()
        {
            return _relayState;
        }

        /// <param name="relayState"> the relayState to Set </param>
        public void SetRelayState(RelayState relayState)
        {
            _relayState = relayState;
        }

        public override int GetHashCode()
        {
            return _relayState != null ? _relayState.GetHashCode() : 0;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is Relay)) return false;

            var relay = (Relay)o;

            if (_relayState != relay._relayState) return false;

            return true;
        }

        public override string ToString()
        {
            return "Relay{" +
                   "relayState=" + _relayState +
                   '}';
        }
    }
}