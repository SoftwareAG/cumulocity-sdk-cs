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
using System.Text;
using System.Threading.Tasks;
using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Cumulocity.MQTT
{
    public class MqttStaticEventTemplates : IMqttStaticEventTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticEventTemplates(IMqttClient cl)
        {
            _mqttClient = cl;
        }

        /// <summary>
        ///     Creates the basic event asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateBasicEventAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));

            try
            {
                var createBasicEventMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", "s"),
                    Payload = Encoding.UTF8.GetBytes(string.Format("400,{0},{1},{2}", type, text, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createBasicEventMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates the location update event asynchronous.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> CreateLocationUpdateEventAsync(string latitude, string longitude, string altitude,
            string accuracy, string time, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createLocationUpdateEventMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("401,{0},{1},{2},{3},{4}", latitude, longitude,
                        altitude, accuracy, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createLocationUpdateEventMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates the location update event with device update asynchronous.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> CreateLocationUpdateEventWithDeviceUpdateAsync(string latitude, string longitude,
            string altitude, string accuracy, string time, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createLocationUpdateEventWithDeviceUpdateMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("402,{0},{1},{2},{3},{4}", latitude, longitude,
                        altitude, accuracy, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createLocationUpdateEventWithDeviceUpdateMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                return false;
            }

            return true;
        }

        private static string GetProcessingMode(ProcessingMode? processingMode)
        {
            var stringProcessingMode = "s";
            if (processingMode.HasValue && processingMode.Value.Equals(ProcessingMode.TRANSIENT))
                stringProcessingMode = "t";

            return stringProcessingMode;
        }
    }
}