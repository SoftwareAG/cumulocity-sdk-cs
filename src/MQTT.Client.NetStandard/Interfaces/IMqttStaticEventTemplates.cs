using Cumulocity.MQTT.Enums;
using System;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticEventTemplates
    {
        //Create basic event (400)
        Task<bool> CreateBasicEventAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create location update event (401)
        Task<bool> CreateLocationUpdateEventAsync(string latitude, string longitude, string altitude, string accuracy, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create location update event with device update (402)
        Task<bool> CreateLocationUpdateEventWithDeviceUpdateAsync(string latitude, string longitude, string altitude, string accuracy, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);
    }
}