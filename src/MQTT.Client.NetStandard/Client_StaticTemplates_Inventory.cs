using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.MQTT
{
    public class MqttStaticInventoryTemplates : IMqttStaticInventoryTemplates
    {
        private readonly IMqttClient _mqttClient;

        public MqttStaticInventoryTemplates(IMqttClient cl)
        {
            this._mqttClient = cl;
        }

        /// <summary>
        /// Childs the device creation. Will create a new child device for the current device.
        /// The newly created object will be added as child device. Additionally an externaId
        /// for the child will be created with type “c8y_Serial” and the value a combination of the serial of the root device
        /// and the unique child ID.
        /// </summary>
        /// <param name="uniqueChildID">The unique child identifier. Mandatory parameter.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="deviceType">Type of the device.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> ChildDeviceCreationAsync(string uniqueChildID, string deviceName, string deviceType, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var childDeviceCreationMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("101,{0},{1},{2}", uniqueChildID, deviceName, deviceType)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(childDeviceCreationMsg);
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
        /// Configures the hardware. Will update the Hardware properties of the device.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="model">The model.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> ConfigureHardware(string serialNumber, string model, string revision, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var configureHardwareMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("110,{0},{1},{2}", serialNumber, model, revision)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(configureHardwareMsg);
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
        /// Configures the mobile. Will update the Mobile properties of the device.
        /// </summary>
        /// <param name="imei">The imei.</param>
        /// <param name="iccid">The iccid.</param>
        /// <param name="imsi">The imsi.</param>
        /// <param name="mcc">The MCC.</param>
        /// <param name="mnc">The MNC.</param>
        /// <param name="lac">The lac.</param>
        /// <param name="cellId">The cell identifier.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> ConfigureMobile(string imei, string iccid, string imsi, string mcc, string mnc, string lac, string cellId, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var configureMobileMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("111,{0},{1},{2},{3},{4},{5},{6}", imei, iccid, imsi, mcc, mnc, lac, cellId)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(configureMobileMsg);
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
        /// Configures the position.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> ConfigurePosition(string latitude, string longitude, string altitude, string accuracy, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var configurePositionMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("112,{0},{1},{2},{3}", latitude, longitude, altitude, accuracy)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(configurePositionMsg);
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
        /// Devices the creation. Will create a new device for the serial number in the inventory if not yet existing.
        /// An externalId for the device with type c8y_Serial and the device identifier of the MQTT clientId as value will be created
        /// </summary>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="deviceType">Type of the device.</param>
        public async Task<bool> DeviceCreation(string deviceName, string deviceType, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var deviceCreationMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("100,{0},{1}", deviceName, deviceType)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(deviceCreationMsg);
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
        /// Gets the child devices. Will trigger the sending of child devices of the device.
        /// </summary>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> GetChildDevices(Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var getChildDevicesMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes("105"),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };
                await _mqttClient.PublishAsync(getChildDevicesMsg);
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
        /// Sets the configuration. Will update the Position properties of the device.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> SetConfiguration(string configuration, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setConfigurationMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("113,{0}", configuration)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(setConfigurationMsg);
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
        /// Sets the firmware. Will set the firmware installed on the device.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        /// <param name="url">The URL.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> SetFirmware(string name, string version, string url, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setFirmwareMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("115,{0},{1},{2}", name, version, url)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(setFirmwareMsg);
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
        /// Sets the required availability. Will set the required interval for availability monitoring. It will only set the value if does not exist. Values entered e.g. through UI are not overwritten.
        /// </summary>
        /// <param name="requiredInterval">The required interval.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        public async Task<bool> SetRequiredAvailability(int requiredInterval, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setRequiredAvailabilityMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("117,{0}", requiredInterval)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };
                await _mqttClient.PublishAsync(setRequiredAvailabilityMsg);
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
        /// Sets the software. Will set the list of software installed on the device. List of 3 values per software
        /// </summary>
        /// <param name="software1">The software1.</param>
        /// <param name="version1">The version1.</param>
        /// <param name="url1">The url1.</param>
        /// <param name="software2">The software2.</param>
        /// <param name="version2">The version2.</param>
        /// <param name="url2">The url2.</param>
        /// <param name="software3">The software3.</param>
        /// <param name="version3">The version3.</param>
        /// <param name="url3">The url3.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>

        public async Task<bool> SetSoftwareList(IList<Software> installedSoftware, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            ExceptionDispatchInfo capturedException = null;

            if (installedSoftware == null)
            {
                throw new ArgumentNullException(nameof(installedSoftware));
            }
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var softwares = String.Join(",", installedSoftware.Select(o => o.Name + "," + o.Version + "," + o.Url));


                var setSoftwareList = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Format("116,{0}", softwares)),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };

                await _mqttClient.PublishAsync(setSoftwareList);
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
        /// Sets the supported operations.
        /// </summary>
        /// <param name="supportedOperations">The supported operations.</param>
        /// <param name="errorHandlerAsync">The error handler asynchronous.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">supportedOperations</exception>
        public async Task<bool> SetSupportedOperations(IList<string> supportedOperations, Func<Exception, Task<bool>> errorHandlerAsync, ProcessingMode? processingMode = null)
        {
            if (supportedOperations == null)
            {
                throw new ArgumentNullException(nameof(supportedOperations));
            }

            ExceptionDispatchInfo capturedException = null;
            string stringProcessingMode = GetProcessingMode(processingMode);
            try
            {
                var setSupportedOperationsMsg = new MqttApplicationMessage()
                {
                    Topic = String.Format("{0}/us", stringProcessingMode),
                    Payload = Encoding.UTF8.GetBytes(String.Concat("114,", String.Join(", ", supportedOperations.ToArray()))),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };
                await _mqttClient.PublishAsync(setSupportedOperationsMsg);
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