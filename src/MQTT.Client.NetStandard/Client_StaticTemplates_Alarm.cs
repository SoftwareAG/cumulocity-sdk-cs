using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Serilog;
using System;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.MQTT
{
    public class MqttStaticAlarmTemplates : IMqttStaticAlarmTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticAlarmTemplates(IMqttClient cl)
        {
            this._mqttClient = cl;
        }
        /// <summary>
        /// Clears the existing alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> ClearExistingAlarmAsync(string type, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                var clearExistingAlarmMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("306,{0}", type)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                Log.Fatal(capturedException.SourceException, "ClearExistingAlarmAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the critical alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateCriticalAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                var createCriticalAlarmMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("301,{0},{1},{2}", type, text, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                Log.Fatal(capturedException.SourceException, "CreateCriticalAlarmAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the major alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateMajorAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                var createMajorAlarmMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("302,{0},{1},{2}", type, text, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                Log.Fatal(capturedException.SourceException, "CreateMajorAlarmAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the minor alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <param name="processingMode">The processing mode.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateMinorAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                var createMinorAlarmMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("303,{0},{1},{2}", type, text, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                Log.Fatal(capturedException.SourceException, "CreateMinorAlarmAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the warning alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text. Alarm of type (alarmType) raised</param>
        /// <param name="time">The time. Current server time</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> CreateWarningAlarmAsync(string type, string text, string time, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {

                var createWarningAlarmMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("304,{0},{1},{2}", type, text, time)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                Log.Fatal(capturedException.SourceException, "CreateWarningAlarmAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Updates the severity of existing alarm asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public async Task<bool> UpdateSeverityOfExistingAlarmAsync(string type, string severity, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(severity))
            {
                throw new ArgumentNullException(nameof(type) + ' ' + nameof(severity));
            }

            try
            {
                var updateSeverityOfExistingAlarmMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("305,{0},{1}", type, severity)),
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
                bool needsThrow = await errorHandlerAsync(capturedException.SourceException).ConfigureAwait(false);
                if (needsThrow)
                {
                    capturedException.Throw();
                }
                Log.Fatal(capturedException.SourceException, "UpdateSeverityOfExistingAlarmAsync terminated unexpectedly.");
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