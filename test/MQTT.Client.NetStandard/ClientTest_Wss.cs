using Cumulocity.MQTT.Interfaces;
using Cumulocity.MQTT.Utils;
using Moq;
using MQTT.Test;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Test
{
    [TestFixture]
    public class ClientTest_Wss
    {
        private Client cl;

        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;

            var config = new Mock<IConfiguration>();
            config.Setup(c => c.Server).Returns(cnf.WssServer);
            config.Setup(c => c.UserName).Returns(cnf.UserName);
            config.Setup(c => c.Password).Returns(cnf.Password);
            config.Setup(c => c.Port).Returns(cnf.WssPort);
            config.Setup(c => c.ConnectionType).Returns("WSS");
            config.Setup(c => c.ClientId).Returns(cnf.ClientId);

            cl = new Client(config.Object);
        }

        [Test]
        [Ignore("Firewall")]
        public void ClientTest_TlsConnection_Connect()
        {
            var res = Task.Run(() => cl.ConnectAsync()).Result;

            Assert.IsTrue(cl.IsConnected);
        }
    }
}