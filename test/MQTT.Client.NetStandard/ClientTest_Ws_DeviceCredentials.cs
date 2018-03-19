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
using MQTTnet.Client;
using MQTTnet;

namespace Cumulocity.MQTT.Test
{
    [TestFixture]
    internal class ClientTest_Ws_DeviceCredentials
    {

        private Client cl;
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

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
        }

        //[Test]
        //public void ClientTest_WsConnection_UpdateDataAsync_Operation()
        //{
        //    //var res1 = Task.Run(() => cl.SubscribeAsync()).Result;
        //    var res2 = Task.Run(() => cl.DeviceCredentials.RequestDeviceCredentials((e) => { return Task.FromResult(false); })).Result;
        //    Assert.IsTrue(res2);
        //}
        [Test, MaxTime(10000)]
        public void ClientTest_Ws_RequestDeviceCredential()
        {
            cl.RequestDeviceCredentialEvt += (s, e) =>
            {
                var isTenant = e.Tenant.Length > 0;
                Assert.AreEqual(true, isTenant);
            };

            var res2 = Task.Run(() => cl.DeviceCredentials.RequestDeviceCredentials((e) => { return Task.FromResult(false); })).Result;

        }

    }
}
