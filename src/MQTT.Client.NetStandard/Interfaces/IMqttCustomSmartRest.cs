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
using Cumulocity.MQTT.Model;

namespace Cumulocity.MQTT.Interfaces
{
    public interface IMqttCustomSmartRest
    {
        Task<bool> CheckTemplateCollectionExists(string templateCollectionName,
            Func<Exception, Task<bool>> errorHandlerAsync);

        Task<bool> CreateTemplateDataAsync(string collectionName, IEnumerable<Request> requests,
            IEnumerable<Response> responses, ProcessingMode? processingMode = null);

        Task<bool> SendRequestDataAsync(string collectionName, string msgId, IEnumerable<string> parameters,
            ProcessingMode? processingMode = null);

        Task<bool> SubscribeSmartRestAsync(string collectionName);
    }
}