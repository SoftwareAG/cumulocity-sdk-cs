using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.MQTT
{
    public class MqttStaticMeasurementTemplates : IMqttStaticMeasurementTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticMeasurementTemplates(IMqttClient cl)
        {
            this._mqttClient = cl;
        }

        /// <summary>
        /// Creates the battery measurement asynchronous. Will create a measurement of type c8y_Battery
        /// </summary>
        /// <param name="batteryValue">The battery value.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">batteryValue</exception>
        public async Task<bool> CreateBatteryMeasurementAsync(string batteryValue, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(batteryValue))
            {
                throw new ArgumentNullException(nameof(batteryValue));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createBatteryMeasurementMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("212,{0},{1}", batteryValue, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the custom measurement asynchronous. Will create a measurement with given fragment and series.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="series">The series.</param>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment,series,value</exception>
        public async Task<bool> CreateCustomMeasurementAsync(string fragment, string series, string value, string unit, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(fragment) || String.IsNullOrEmpty(series) || String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(fragment) + ' ' + nameof(series) + ' ' + nameof(value));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createCustomMeasurementMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("200,{0},{1},{2},{3},{4}", fragment, series, value, unit, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the signal strength measurement asynchronous. Will create a measurement of type c8y_SignalStrength
        /// </summary>
        /// <param name="rssiValue">The rssi value.</param>
        /// <param name="berValue">The ber value.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">rssiValue, berValue</exception>
        public async Task<bool> CreateSignalStrengthMeasurementAsync(string rssiValue, string berValue, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(rssiValue) && String.IsNullOrEmpty(berValue))
            {
                throw new ArgumentNullException(nameof(rssiValue) + ' ' + nameof(berValue));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createSignalStrengthMeasurementMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("210,{0},{1},{2}", rssiValue, berValue, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the temperature measurement asynchronous. Will create a measurement of type c8y_TemperatureMeasurement
        /// </summary>
        /// <param name="temperatureValue">The temperature value.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> CreateTemperatureMeasurementAsync(string temperatureValue, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(temperatureValue))
            {
                throw new ArgumentNullException(nameof(temperatureValue));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createTemperatureMeasurementMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("211,{0},{1}", temperatureValue, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private static string GetProcessingMode(ProcessingMode? processingMode)
        {
            var stringProcessingMode = "s";
            if (processingMode.HasValue && processingMode.Value.Equals(ProcessingMode.TRANSIENT))
            {
                stringProcessingMode = "t";
            }

            return stringProcessingMode;
        }
    }
}