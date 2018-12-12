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

        /// <param name="relayState"> the relayState to set </param>
        public void setRelayState(RelayState relayState)
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

            var relay = (Relay) o;

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