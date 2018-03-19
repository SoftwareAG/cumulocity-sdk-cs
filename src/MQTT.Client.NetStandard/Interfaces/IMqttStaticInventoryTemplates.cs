#region Cumulocity GmbH

// /*
//  * Copyright (C) 2015-2018
//  *
//  * Permission is hereby granted, free of charge, to any person obtaining a copy of
//  * this software and associated documentation files (the "Software"),
//  * to deal in the Software without restriction, including without limitation the rights to use,
//  * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
//  * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  *
//  * The above copyright notice and this permission notice shall be
//  * included in all copies or substantial portions of the Software.
//  *
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  */

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cumulocity.MQTT.Enums;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticInventoryTemplates
    {
        //INVENTORY TEMPLATES(1XX)
        //100
        Task<bool> DeviceCreation(string deviceName, string deviceType, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);

        //101
        Task<bool> ChildDeviceCreationAsync(string uniqueChildID, string deviceName, string deviceType,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //105
        Task<bool> GetChildDevices(Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);

        //110
        Task<bool> ConfigureHardware(string serialNumber, string model, string revision,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //111
        Task<bool> ConfigureMobile(string imei, string iccid, string imsi, string mcc, string mnc, string lac,
            string cellId, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //112
        Task<bool> ConfigurePosition(string latitude, string longitude, string altitude, string accuracy,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //113
        Task<bool> SetConfiguration(string configuration, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);

        //114
        Task<bool> SetSupportedOperations(IList<string> supportedOperations,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //115
        Task<bool> SetFirmware(string name, string version, string url, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);

        //116
        Task<bool> SetSoftwareList(IList<Software> installedSoftware, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);

        //117
        Task<bool> SetRequiredAvailability(int requiredInterval, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);
    }
}