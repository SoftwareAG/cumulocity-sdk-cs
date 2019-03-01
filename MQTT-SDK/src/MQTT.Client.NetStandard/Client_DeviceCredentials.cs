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
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Cumulocity.MQTT.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Serilog;

namespace Cumulocity.MQTT
{
    /// <summary>
    ///     Lightweight client for talking to an MQTT server
    /// </summary>
    /// <seealso cref="Cumulocity.MQTT.NetStandard.Interfaces.IClient" />
    public class DeviceCredentials : IDeviceCredentials
    {
        private readonly IMqttClient cl;

        public DeviceCredentials(IMqttClient cl)
        {
            this.cl = cl;
        }

        public async Task<bool> RequestDeviceCredentials(Func<Exception, Task<bool>> errorHandlerAsync)
        {
            ExceptionDispatchInfo capturedException = null;

            try
            {
                var commandMsg = new MqttApplicationMessage
                {
                    Topic = "s/ucr",
                    Payload = new byte[0],
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await cl.PublishAsync(commandMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException, "RequestDeviceCredentials terminated unexpectedly.");
                return false;
            }

            return true;
        }
    }
}