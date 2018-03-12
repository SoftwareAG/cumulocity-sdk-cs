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
    public class MqttStaticOperationTemplates : IMqttStaticOperationTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticOperationTemplates(IMqttClient cl)
        {
            this._mqttClient = cl;
        }

        /// <summary>
        /// Gets the pending operations asynchronous.
        /// </summary>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> GetPendingOperationsAsync(Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var getPendingOperationsMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
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
        /// Gets the executing operations asynchronous.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment</exception>
        public async Task<bool> SetExecutingOperationsAsync(string fragment, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(fragment))
            {
                throw new ArgumentNullException(nameof(fragment));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var getExecutingOperationsMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("501,{0}", fragment)),
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
        /// Sets the operation to failed asynchronous.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="failureReason">The failure reason.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment</exception>
        public async Task<bool> SetOperationToFailedAsync(string fragment, string failureReason, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(fragment))
            {
                throw new ArgumentNullException(nameof(fragment));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setOperationToFailedMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("502,{0},{1}", fragment, failureReason)),
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
        /// Sets the operation to successful asynchronous.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">fragment</exception>
        public async Task<bool> SetOperationToSuccessfulAsync(string fragment, string parameters, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(fragment))
            {
                throw new ArgumentNullException(nameof(fragment));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setOperationToSuccessfulMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("503,{0}", fragment)),
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