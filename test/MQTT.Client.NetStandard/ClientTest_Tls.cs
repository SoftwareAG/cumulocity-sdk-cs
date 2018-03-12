using Moq;
using Cumulocity.MQTT.Utils;
using NUnit.Framework;
using System.Threading.Tasks;
using Cumulocity.MQTT.Interfaces;
using MQTT.Test;

namespace Cumulocity.MQTT.Test
{
    [TestFixture]
    public class ClientTest_Tls
    {
        private Mock<IConfiguration> ini;
        private Client cl;

        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;

            ini = new Mock<IConfiguration>();
            ini.Setup(i => i.Server).Returns(cnf.TlsServer);
            ini.Setup(i => i.UserName).Returns(cnf.UserName);
            ini.Setup(i => i.Password).Returns(cnf.Password);
            ini.Setup(i => i.Port).Returns(cnf.TlsPort);
            ini.Setup(i => i.ConnectionType).Returns("TLS");
            ini.Setup(i => i.ClientId).Returns(cnf.ClientId);

            cl = new Client(ini.Object);
        }

        [Test]
        public void ClientTest_TlsConnection_Connect()
        {
            var res = Task.Run(() => cl.ConnectAsync()).Result;

            Assert.IsTrue(cl.IsConnected);
        }
    }
}