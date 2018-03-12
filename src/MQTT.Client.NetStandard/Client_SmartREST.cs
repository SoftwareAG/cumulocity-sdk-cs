using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Interfaces;
using Cumulocity.MQTT.Model;
using Cumulocity.MQTT.Interfaces;

using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Client;

namespace Cumulocity.MQTT
{
    /// <summary>
    /// Lightweight client for talking to an MQTT server
    /// </summary>
    /// <seealso cref="Cumulocity.MQTT.Interfaces.IClient" />
    public class MqttCustomSmartRest : IMqttCustomSmartRest
    {
        private readonly IMqttClient _mqttClient;

        public MqttCustomSmartRest(IMqttClient cl)
        {
            this._mqttClient = cl;
        }
        /// <summary>
        /// Checks the template collection exists.
        /// </summary>
        /// <param name="templateCollectionName">Name of the template collection.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">templateCollectionName</exception>
        public async Task<bool> CheckTemplateCollectionExists(string templateCollectionName, Func<Exception, Task<bool>> errorHandlerAsync)
        {
            ExceptionDispatchInfo capturedException = null;

            if (String.IsNullOrEmpty(templateCollectionName))
            {
                throw new ArgumentException(nameof(templateCollectionName));
            }

            try
            {
                var checkTemplateCollectionExistsMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("s/ut/{0}", templateCollectionName),
                    Payload = new byte[0],
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(checkTemplateCollectionExistsMsg);
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
        /// Creates the get inventory data asynchronous.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="requests">The requests.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="processingMode">The processing mode.</param>
        /// <returns></returns>
        public async Task<bool> CreateGetInventoryDataAsync(string collectionName, IEnumerable<Request> requests, IEnumerable<Response> responses, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            try
            {
                string stringProcessingMode = GetProcessingMode(processingMode);
                string messageBody = GetMessageBody(requests, responses);

                var createInventoryDataMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/ut/{1}", stringProcessingMode, collectionName),
                    Payload = Encoding.UTF8.GetBytes(messageBody),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createInventoryDataMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }
            if (capturedException != null)
            {
                Log.Fatal(capturedException.SourceException, "CreateGetInventoryDataAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the template data asynchronous.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="requests">The requests.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="processingMode">The processing mode.</param>
        /// <returns></returns>
        public async Task<bool> CreateTemplateDataAsync(string collectionName, IEnumerable<Request> requests, IEnumerable<Response> responses, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            try
            {
                string stringProcessingMode = GetProcessingMode(processingMode);
                string messageBody = GetMessageBody(requests, responses);

                var createPostDataMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/ut/{1}", stringProcessingMode, collectionName),
                    Payload = Encoding.UTF8.GetBytes(messageBody),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(createPostDataMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }
            if (capturedException != null)
            {
                Log.Fatal(capturedException.SourceException, "CreateTemplateDataAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Sends the request data asynchronous.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="msgId">The MSG identifier.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="processingMode">The processing mode.</param>
        /// <returns></returns>
        public async Task<bool> SendRequestDataAsync(string collectionName, string msgId, IEnumerable<string> parameters, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            try
            {
                string stringProcessingMode = GetProcessingMode(processingMode);

                var msg = String.Format("{0},{1}", msgId, String.Join(",", parameters.ToArray()));

                var getInventoryDataMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/ut/{1}", stringProcessingMode, collectionName),
                    Payload = Encoding.UTF8.GetBytes(msg),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(getInventoryDataMsg);
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }
            if (capturedException != null)
            {
                Log.Fatal(capturedException.SourceException, "SendRequestDataAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Subscribes the smart rest asynchronous.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        public async Task<bool> SubscribeSmartRestAsync(string collectionName)
        {
            ExceptionDispatchInfo capturedException = null;
            try
            {
                await _mqttClient.SubscribeAsync(new List<TopicFilter> { new TopicFilter(String.Format("s/dc/{0}", collectionName), MqttQualityOfServiceLevel.AtMostOnce) });
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }
            if (capturedException != null)
            {
                Log.Fatal(capturedException.SourceException, "SubscribeSmartRestAsync terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private static string GetMessageBody(IEnumerable<Request> requests, IEnumerable<Response> responses)
        {
            StringBuilder requestsMsg = new StringBuilder();
            if (requests != null && requests.Any())
            {
                foreach (var req in requests)
                {
                    requestsMsg.Append(String.Concat("\r\n", req.RequestTemplate()));
                }
            }

            StringBuilder responsesMsg = new StringBuilder();
            if (responses != null && responses.Any())
            {
                foreach (var resp in responses)
                {
                    responsesMsg.Append(String.Concat("\r\n", "11,", resp.Id, ",", resp.Base, ",", resp.Cond, ",", String.Join(", ", resp.Values.ToArray())));
                }
            }
            var messageBody = String.Concat(requestsMsg.ToString(), responsesMsg.ToString());
            return messageBody;
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