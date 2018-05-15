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
    internal class ClientTest_Ws_StaticTemplates_Event
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
            TestContext.WriteLine(res1);
            Assert.IsTrue(cl.IsConnected);
        }

        //Create basic event (400)
        [Test]
        public void ClientTest_WsConnection_CreateBasicEvent()
        {
            var res2 = Task.Run(() => cl.StaticEventTemplates.CreateBasicEventAsync("c8y_MyEvent", "Something was triggered", string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Create location update event (401)
        [Test]
        public void ClientTest_WsConnection_CreateLocationUpdateEvent()
        {
            var res2 = Task.Run(() => cl.StaticEventTemplates.CreateLocationUpdateEventAsync(
                 "52.209538",
                 "16.831992",
                 "76",
                 "134",
                 string.Empty,
                 (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
        //Create location update event with device update (402)
        [Test]
        public void ClientTest_WsConnection_CreateLocationUpdateEventWithDeviceUpdate()
        {
            var res2 = Task.Run(() => cl.StaticEventTemplates.CreateLocationUpdateEventWithDeviceUpdateAsync(
                                 "52.209538",
                                 "16.831992",
                                 "76",
                                 "134",
                                 string.Empty,
                                 (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
    }
}
