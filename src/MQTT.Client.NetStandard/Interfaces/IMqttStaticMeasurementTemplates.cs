using Cumulocity.MQTT.Enums;
using System;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticMeasurementTemplates
    {
        //200
        Task<bool> CreateCustomMeasurementAsync(string fragment, string series, string value, string unit, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //210
        Task<bool> CreateSignalStrengthMeasurementAsync(string rssiValue, string berValue, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //211
        Task<bool> CreateTemperatureMeasurementAsync(string temperatureValue, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //212
        Task<bool> CreateBatteryMeasurementAsync(string batteryValue, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);
    }
}