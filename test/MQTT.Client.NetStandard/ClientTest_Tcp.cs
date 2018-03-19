using Moq;
using Cumulocity.MQTT;
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
    [Ignore("Firewall")]
    public class ClientTest_Tcp
    {
        private Mock<IConfiguration> ini;
        private Client cl;

        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;

            ini = new Mock<IConfiguration>();
            ini.Setup(i => i.Server).Returns(cnf.TcpServer);
            ini.Setup(i => i.UserName).Returns(cnf.UserName);
            ini.Setup(i => i.Password).Returns(cnf.Password);
            ini.Setup(i => i.Port).Returns(cnf.TcpPort);
            ini.Setup(i => i.ConnectionType).Returns("TCP");
            ini.Setup(i => i.ClientId).Returns(cnf.ClientId);

            cl = new Client(ini.Object);
        }

        [Test]
        public void ClientTest_TcpConnection_Connect()
        {
            var res = Task.Run(() => cl.ConnectAsync()).Result;
            TestContext.WriteLine(res.ToString());
            Assert.IsTrue(cl.IsConnected);
        }
    }
}
