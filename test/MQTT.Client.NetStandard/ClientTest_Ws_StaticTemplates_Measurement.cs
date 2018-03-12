using Moq;
using Cumulocity.MQTT.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.MQTT.Interfaces;
using MQTT.Test;

namespace Cumulocity.MQTT.Test
{
    internal class ClientTest_Ws_StaticTemplates_Measurement
    {
        private Client cl;


        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;

            var config = new Mock<IConfiguration>();
            config.Setup(c => c.Server).Returns(cnf.WsServer);
            config.Setup(c => c.UserName).Returns(cnf.UserName);
            config.Setup(c => c.Password).Returns(cnf.Password);
            config.Setup(c => c.Port).Returns(cnf.WsPort);
            config.Setup(c => c.ConnectionType).Returns("WS");
            config.Setup(c => c.ClientId).Returns(cnf.ClientId);

            cl = new Client(config.Object);

            var res1 = Task.Run(() => cl.ConnectAsync()).Result;
            Assert.IsTrue(cl.IsConnected);
        }
        [Test]
        public void ClientTest_WsConnection_CreateCustomMeasurement()
        {
            var res2 = Task.Run(() => cl.MqttStaticMeasurementTemplates.CreateCustomMeasurementAsync("c8y_Temperature", "T", "25", string.Empty, string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        [Test]
        public void ClientTest_WsConnection_CreateSignalStrengthMeasurement()
        {
            var res2 = Task.Run(() => cl.MqttStaticMeasurementTemplates.CreateSignalStrengthMeasurementAsync("-90", "23", "2017-09-13T14:00:14.000+02:00", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        [Test]
        public void ClientTest_WsConnection_CreateTemperatureMeasurement()
        {
            var res2 = Task.Run(() => cl.MqttStaticMeasurementTemplates.CreateTemperatureMeasurementAsync("25", "2017-09-13T15:01:14.000+02:00", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_CreateBatteryMeasurement()
        {
            var res2 = Task.Run(() => cl.MqttStaticMeasurementTemplates.CreateBatteryMeasurementAsync("95", "2017-09-13T15:01:14.000+02:00", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }


    }
}
