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
    internal class ClientTest_Ws_StaticTemplates_Operations
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
            Assert.IsTrue(cl.IsConnected);
        }

        [Test]
        public void ClientTest_WsConnection_GetPendingOperations()
        {
            //Will trigger the sending of all PENDING operations for the agent.
            var res2 = Task.Run(() => cl.StaticOperationTemplates.GetPendingOperationsAsync((e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_GetExecutingOperations()
        {
            //Will set the oldest EXECUTING operation with given fragment to FAILED
            var res2 = Task.Run(() => cl.StaticOperationTemplates.SetExecutingOperationsAsync("c8y_Restart", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_SetOperationToFailed()
        {
            //Will set the oldest EXECUTING operation with given fragment to SUCCESSFUL.
            //It enables the device to send additional parameters that trigger additional steps based on the type of operation send as fragment (see section Updating operations).
            var res2 = Task.Run(() => cl.StaticOperationTemplates.SetOperationToFailedAsync("c8y_Restart", "Could not restart", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_SetOperationToSuccessful()
        {
            //
            var res2 = Task.Run(() => cl.StaticOperationTemplates.SetOperationToSuccessfulAsync("c8y_Restart", string.Empty, (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
    }
}
