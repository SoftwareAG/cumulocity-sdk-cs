using Cumulocity.MQTT.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Serilog;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Cumulocity.MQTT
{
    /// <summary>
    /// Lightweight client for talking to an MQTT server
    /// </summary>
    /// <seealso cref="Cumulocity.MQTT.NetStandard.Interfaces.IClient" />
    public class DeviceCredentials : IDeviceCredentials
    {
        private readonly IMqttClient cl;

        public DeviceCredentials(IMqttClient cl)
        {
            this.cl = cl;
        }
        public async Task<bool> RequestDeviceCredentials(Func<Exception, Task<bool>> errorHandlerAsync)
        {
            ExceptionDispatchInfo capturedException = null;

            try
            {
                var commandMsg = new MqttApplicationMessage(
                    "s/ucr",
                    new byte[0],
                    MqttQualityOfServiceLevel.AtLeastOnce,
                    false
                );

                await cl.PublishAsync(commandMsg);
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
                Log.Fatal(capturedException.SourceException, "RequestDeviceCredentials terminated unexpectedly.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}