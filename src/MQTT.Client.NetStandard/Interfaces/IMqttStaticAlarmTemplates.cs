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
using System.Threading.Tasks;
using Cumulocity.MQTT.Enums;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttStaticAlarmTemplates
    {
        //Create CRITICAL alarm (301)
        Task<bool> CreateCriticalAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create MAJOR alarm (302)
        Task<bool> CreateMajorAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create MINOR alarm (303)
        Task<bool> CreateMinorAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Create WARNING alarm (304)
        Task<bool> CreateWarningAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Update severity of existing alarm (305)
        Task<bool> UpdateSeverityOfExistingAlarmAsync(string type, string severity,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null);

        //Clear existing alarm (306)
        Task<bool> ClearExistingAlarmAsync(string type, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null);
    }
}