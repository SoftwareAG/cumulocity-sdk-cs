using Cumulocity.MQTT.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticInventoryTemplates
    {
        //INVENTORY TEMPLATES(1XX)
        //100
        Task<bool> DeviceCreation(string deviceName, string deviceType, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //101
        Task<bool> ChildDeviceCreationAsync(string uniqueChildID, string deviceName, string deviceType, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //105
        Task<bool> GetChildDevices(Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //110
        Task<bool> ConfigureHardware(string serialNumber, string model, string revision, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //111
        Task<bool> ConfigureMobile(string imei, string iccid, string imsi, string mcc, string mnc, string lac, string cellId, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //112
        Task<bool> ConfigurePosition(string latitude, string longitude, string altitude, string accuracy, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //113
        Task<bool> SetConfiguration(string configuration, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //114
        Task<bool> SetSupportedOperations(IList<string> supportedOperations, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //115
        Task<bool> SetFirmware(string name, string version, string url, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //116
        Task<bool> SetSoftwareList(IList<Software> installedSoftware, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //117
        Task<bool> SetRequiredAvailability(int requiredInterval, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);
    }
}