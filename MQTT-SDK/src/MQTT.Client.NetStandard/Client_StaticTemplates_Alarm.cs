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
using Serilog;

namespace Cumulocity.MQTT
{
    public class MqttStaticAlarmTemplates : IMqttStaticAlarmTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticAlarmTemplates(IMqttClient cl)
        {
            _mqttClient = cl;
        }

        /// <summary>
        ///     Clears the existing alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> ClearExistingAlarmAsync(string type, Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));

            try
            {
                var clearExistingAlarmMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("306,{0}", type)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(clearExistingAlarmMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException, "ClearExistingAlarmAsync terminated unexpectedly.");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates the critical alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateCriticalAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));

            try
            {
                var createCriticalAlarmMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("301,{0},{1},{2}", type, text, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createCriticalAlarmMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException, "CreateCriticalAlarmAsync terminated unexpectedly.");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates the major alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateMajorAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));

            try
            {
                var createMajorAlarmMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("302,{0},{1},{2}", type, text, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createMajorAlarmMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException, "CreateMajorAlarmAsync terminated unexpectedly.");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates the minor alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <param name="processingMode">The processing mode.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateMinorAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));

            try
            {
                var createMinorAlarmMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("303,{0},{1},{2}", type, text, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createMinorAlarmMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException, "CreateMinorAlarmAsync terminated unexpectedly.");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates the warning alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateWarningAlarmAsync(string type, string text, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));

            try
            {
                var createWarningAlarmMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("304,{0},{1},{2}", type, text, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createWarningAlarmMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException, "CreateWarningAlarmAsync terminated unexpectedly.");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updates the severity of existing alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> UpdateSeverityOfExistingAlarmAsync(string type, string severity,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(severity))
                throw new ArgumentNullException(nameof(type) + ' ' + nameof(severity));

            try
            {
                var updateSeverityOfExistingAlarmMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("305,{0},{1}", type, severity)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(updateSeverityOfExistingAlarmMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                var needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow) capturedException.Throw();
                Log.Fatal(capturedException.SourceException,
                    "UpdateSeverityOfExistingAlarmAsync terminated unexpectedly.");
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