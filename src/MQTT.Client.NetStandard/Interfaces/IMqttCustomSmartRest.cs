using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Model;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttCustomSmartRest
    {
        Task<bool> CheckTemplateCollectionExists(string templateCollectionName, Func<Exception, Task<bool>> errorHandlerAsync);
        Task<bool> CreateTemplateDataAsync(string collectionName, IEnumerable<Request> requests, IEnumerable<Response> responses, ProcessingMode? processingMode = null);
        Task<bool> SendRequestDataAsync(string collectionName, string msgId, IEnumerable<string> parameters, ProcessingMode? processingMode = null);
        Task<bool> SubscribeSmartRestAsync(string collectionName);
    }
}