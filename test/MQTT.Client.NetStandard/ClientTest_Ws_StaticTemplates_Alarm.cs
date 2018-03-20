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
    [TestFixture]
    internal class ClientTest_Ws_StaticTemplates_Alarm
    {
        private Client cl;
        private string clientId;

        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;

            clientId = "4927468bdd4b4171a23e31476ff82674";
            var config = new Mock<IConfiguration>();
            config.Setup(c => c.Server).Returns(cnf.WsServer);
            config.Setup(c => c.UserName).Returns(cnf.UserName);
            config.Setup(c => c.Password).Returns(cnf.Password);
            config.Setup(c => c.Port).Returns(cnf.WsPort);
            config.Setup(c => c.ConnectionType).Returns("WS");
            config.Setup(c => c.ClientId).Returns(cnf.ClientId);

            cl = new Client(config.Object);
            var res1 = Task.Run(() => cl.ConnectAsync()).Result;
            TestContext.WriteLine(res1);
            Assert.IsTrue(cl.IsConnected);
        }

        //Create CRITICAL alarm (301)
        [Test]
        public void ClientTest_WsConnection_CreateCriticalAlarm()
        {
            var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateCriticalAlarmAsync("c8y_TemperatureAlarm", "Alarm of type c8y_TemperatureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Create MAJOR alarm (302)
        [Test]
        public void ClientTest_WsConnection_CreateMajorAlarm()
        {
            var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateMajorAlarmAsync("c8y_BatteryAlarm", " Major Alarm of type c8y_BatteryAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Create MINOR alarm (303)
        [Test]
        public void ClientTest_WsConnection_CreateMinorAlarm()
        {
            var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateMinorAlarmAsync("c8y_WaterAlarm", "Alarm of type c8y_WaterAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Create WARNING alarm (304)
        [Test]
        public void ClientTest_WsConnection_CreateWarningAlarm()
        {
            var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateWarningAlarmAsync("c8y_AirPressureAlarm", "Warning of type c8y_AirPressureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Update severity of existing alarm (305)
        [Test]
        public void ClientTest_WsConnection_UpdateSeverityOfExistingAlarm()
        {
            var res2 = Task.Run(() => cl.StaticAlarmTemplates.UpdateSeverityOfExistingAlarmAsync("c8y_AirPressureAlarm", "CRITICAL", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Clear existing alarm (306)
        [Test]
        public void ClientTest_WsConnection_ClearExistingAlarm()
        {
            var res2 = Task.Run(() => cl.StaticAlarmTemplates.ClearExistingAlarmAsync("c8y_TemperatureAlarm", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
    }
}
