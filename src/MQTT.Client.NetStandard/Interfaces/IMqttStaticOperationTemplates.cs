using Cumulocity.MQTT.Enums;
using System;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticOperationTemplates
    {
        Task<bool> GetPendingOperationsAsync(Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        Task<bool> SetExecutingOperationsAsync(string fragment, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        Task<bool> SetOperationToFailedAsync(string fragment, string failureReason, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        Task<bool> SetOperationToSuccessfulAsync(string fragment, string parameters, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);
    }
}