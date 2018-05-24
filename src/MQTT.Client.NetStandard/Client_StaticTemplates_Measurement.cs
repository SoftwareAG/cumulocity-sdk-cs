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
    public class MqttStaticMeasurementTemplates : IMqttStaticMeasurementTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticMeasurementTemplates(IMqttClient cl)
        {
            _mqttClient = cl;
        }

        /// <summary>
        ///     Creates the battery measurement asynchronous. Will create a measurement of type c8y_Battery
        /// </summary>
        /// <param name="batteryValue">The battery value.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">batteryValue</exception>
        public async Task<bool> CreateBatteryMeasurementAsync(string batteryValue, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(batteryValue)) throw new ArgumentNullException(nameof(batteryValue));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createBatteryMeasurementMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("212,{0},{1}", batteryValue, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createBatteryMeasurementMsg);
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
        ///     Creates the custom measurement asynchronous. Will create a measurement with given fragment and series.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="series">The series.</param>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment,series,value</exception>
        public async Task<bool> CreateCustomMeasurementAsync(string fragment, string series, string value, string unit,
            string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(fragment) || string.IsNullOrEmpty(series) || string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(fragment) + ' ' + nameof(series) + ' ' + nameof(value));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createCustomMeasurementMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("200,{0},{1},{2},{3},{4}", fragment, series, value,
                        unit, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createCustomMeasurementMsg);
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
        ///     Creates the signal strength measurement asynchronous. Will create a measurement of type c8y_SignalStrength
        /// </summary>
        /// <param name="rssiValue">The rssi value.</param>
        /// <param name="berValue">The ber value.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">rssiValue, berValue</exception>
        public async Task<bool> CreateSignalStrengthMeasurementAsync(string rssiValue, string berValue, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(rssiValue) && string.IsNullOrEmpty(berValue))
                throw new ArgumentNullException(nameof(rssiValue) + ' ' + nameof(berValue));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createSignalStrengthMeasurementMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("210,{0},{1},{2}", rssiValue, berValue, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createSignalStrengthMeasurementMsg);
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
        ///     Creates the temperature measurement asynchronous. Will create a measurement of type c8y_TemperatureMeasurement
        /// </summary>
        /// <param name="temperatureValue">The temperature value.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> CreateTemperatureMeasurementAsync(string temperatureValue, string time,
            Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (string.IsNullOrEmpty(temperatureValue)) throw new ArgumentNullException(nameof(temperatureValue));
            var stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createTemperatureMeasurementMsg = new MqttApplicationMessage
                {
                    Topic = string.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(string.Format("211,{0},{1}", temperatureValue, time)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createTemperatureMeasurementMsg);
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