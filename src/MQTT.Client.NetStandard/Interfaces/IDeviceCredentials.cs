using System;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IDeviceCredentials
    {
        Task<bool> RequestDeviceCredentials(Func<Exception, Task<bool>> errorHandlerAsync);
    }
}