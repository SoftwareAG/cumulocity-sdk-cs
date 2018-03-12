using Cumulocity.MQTT.Enums;
using System;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticAlarmTemplates
    {
        //Create CRITICAL alarm (301)
        Task<bool> CreateCriticalAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create MAJOR alarm (302)
        Task<bool> CreateMajorAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create MINOR alarm (303)
        Task<bool> CreateMinorAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create WARNING alarm (304)
        Task<bool> CreateWarningAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Update severity of existing alarm (305)
        Task<bool> UpdateSeverityOfExistingAlarmAsync(string type, string severity, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Clear existing alarm (306)
        Task<bool> ClearExistingAlarmAsync(string type, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);
    }
}