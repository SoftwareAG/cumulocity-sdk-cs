﻿using Cumulocity.SDK.MQTT.Model;
using Cumulocity.SDK.MQTT.Model.MqttMessage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.MQTT.Operations
{
    public interface IOperationsProvider
    {

        Task CreateConnectionAsync(IConnectionDetails connectionDetails);

        Task PublishAsync(IMqttMessageRequest message);

        Task SubscribeAsync(IMqttMessageRequest message);

        Task UnsubscribeAsync(string topic);

        Task Disconnect();

        bool ConnectionEstablished { get; }

        event EventHandler<IMqttMessageResponse> MessageReceived;
        event EventHandler<ProcessFailedEventArgs> ConnectionFailed;
        event EventHandler<ClientConnectedEventArgs> Connected;
    }

}
