using Moq;
using Cumulocity.MQTT.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.MQTT.Interfaces;
using MQTT.Test;

namespace Cumulocity.MQTT.Test
{
    [TestFixture]
    public class ClientTest_Ws
    {
        private Mock<IConfiguration> config;
        private Client cl;

        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;

            config = new Mock<IConfiguration>();
            config.Setup(c => c.Server).Returns(cnf.WsServer);
            config.Setup(c => c.UserName).Returns(cnf.UserName);
            config.Setup(c => c.Password).Returns(cnf.Password);
            config.Setup(c => c.Port).Returns(cnf.WsPort);
            config.Setup(c => c.ConnectionType).Returns("WS");
            config.Setup(c => c.ClientId).Returns(cnf.ClientId);

            cl = new Client(config.Object);

        }

        [Test]
        public void ClientTest_WsConnection_Connect()
        {
            var res = Task.Run(() => cl.ConnectAsync()).Result;

            Assert.IsTrue(cl.IsConnected);
        }
    }
}
