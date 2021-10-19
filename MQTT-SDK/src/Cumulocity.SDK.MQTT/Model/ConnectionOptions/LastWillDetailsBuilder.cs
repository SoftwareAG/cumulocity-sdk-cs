using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.MQTT.Model.ConnectionOptions
{
    public class LastWillDetailsBuilder
    {
        private readonly LastWillDetails _lastWillDetails = new LastWillDetails();

        public LastWillDetailsBuilder WithTopic(string topic)
        {
            _lastWillDetails.SetTopic(topic);
            return this;
        }

        public LastWillDetailsBuilder WithQoS(QoS qos)
        {
            _lastWillDetails.SetQoS(qos);
            return this;
        }

        public LastWillDetailsBuilder WithMessage(string message)
        {
            _lastWillDetails.SetMessage(message);
            return this;
        }

        public LastWillDetailsBuilder WithRetained(bool retained)
        {
            _lastWillDetails.SetRetained(retained);
            return this;
        }

        public ILastWillDetails Build()
        {
            return _lastWillDetails;
        }
    }
}
