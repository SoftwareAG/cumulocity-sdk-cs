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
    public class MqttStaticOperationTemplates : IMqttStaticOperationTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticOperationTemplates(IMqttClient cl)
        {
            _mqttClient = cl;
        }

        /// <summary>
        ///     Gets the pending operations asynchronous.
        /// </summary>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> GetPendingOperationsAsync(Func<Exception, Task<bool>> errorHandlerAsync,
            ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var getPendingOperationsMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes("500"),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(getPendingOperationsMsg);
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
        ///     Gets the executing operations asynchronous.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment</exception>
        public async Task<bool> SetExecutingOperationsAsync(string fragment,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(fragment)) throw new ArgumentNullException(nameof(fragment));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var getExecutingOperationsMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("501,{0}", fragment)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(getExecutingOperationsMsg);
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
        ///     Sets the operation to failed asynchronous.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="failureReason">The failure reason.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment</exception>
        public async Task<bool> SetOperationToFailedAsync(string fragment, string failureReason,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(fragment)) throw new ArgumentNullException(nameof(fragment));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setOperationToFailedMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("502,{0},{1}", fragment, failureReason)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(setOperationToFailedMsg);
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
        ///     Sets the operation to successful asynchronous.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment</exception>
        public async Task<bool> SetOperationToSuccessfulAsync(string fragment, string parameters,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(fragment)) throw new ArgumentNullException(nameof(fragment));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setOperationToSuccessfulMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("503,{0}", fragment)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(setOperationToSuccessfulMsg);
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