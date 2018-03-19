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
    public class MqttStaticEventTemplates : IMqttStaticEventTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticEventTemplates(IMqttClient cl)
        {
            this._mqttClient = cl;
        }

        /// <summary>
        /// Creates the basic event asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateBasicEventAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            //string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                var createBasicEventMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", "s"),
                    Payload = Encoding.UTF8.GetBytes(String.Format("400,{0},{1},{2}", type, text, time)),
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
        /// Creates the location update event asynchronous.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> CreateLocationUpdateEventAsync(string latitude, string longitude, string altitude, string accuracy, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createLocationUpdateEventMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("401,{0},{1},{2},{3},{4}", latitude, longitude, altitude, accuracy, time)),
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
        /// Creates the location update event with device update asynchronous.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="time">The time.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> CreateLocationUpdateEventWithDeviceUpdateAsync(string latitude, string longitude, string altitude, string accuracy, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var createLocationUpdateEventWithDeviceUpdateMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("402,{0},{1},{2},{3},{4}", latitude, longitude, altitude, accuracy, time)),
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