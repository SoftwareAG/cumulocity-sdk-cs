using Moq;
using Cumulocity.MQTT.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cumulocity.MQTT.Interfaces;
using MQTT.Test;

namespace Cumulocity.MQTT.Test
{
    [TestFixture]
    internal class ClientTest_Ws_StaticTemplates_Inventory
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
        public void ClientTest_WsConnection_DeviceCreation()
        {
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.DeviceCreation("TEST", "c8y_MQTTDevice", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_ConfigureHardware()
        {
            Thread.Sleep(1000);
            //Will update the Hardware properties of the device.
            //If the device does not exist then create a new one
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.ConfigureHardware(RandomString(8), "model", "1.0", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_ChildDeviceCreation()
        {
            //Will create a new child device for the current device.
            //The newly created object will be added as child device.Additionally an externaId for the child will be created with type “c8y_Serial” and the value a combination of the serial of the root device and the unique child ID.
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.ChildDeviceCreationAsync(RandomString(4), "Device Name", "c8y_MQTTDevice", (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        [Ignore("Firewall")]
        public void ClientTest_WsConnection_GetChildDevices()
        {
            //Will trigger the sending of child devices of the device.
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.GetChildDevices((e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        //ConfigureMobile

        [Test]
        public void ClientTest_WsConnection_ConfigureMobile()
        {
            //Will update the Mobile properties of the device.
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.ConfigureMobile(
                                        "356938035643809",
                                        "8991101200003204510",
                                        "410-07-4777770001",
                                        "410",
                                        "07",
                                        "477",
                                        "0001",
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        [Ignore("Firewall")]
        public void ClientTest_WsConnection_ConfigurePosition()
        {
            Thread.Sleep(1000);
            //Will update the Position properties of the device.
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.ConfigurePosition(
                                        "52.409538",
                                        "16.931992",
                                        "76",
                                        "134",
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        [Ignore("Firewall")]
        public void ClientTest_WsConnection_SetConfiguration()
        {
            //Will update the Position properties of the device.
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetConfiguration(
                                        "val1 = 1\nval2 = 2",
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        [Ignore("Firewall")]
        public void ClientTest_WsConnection_SetSupportedOperations()
        {
            IList<string> supportedOperations = new List<string>();
            supportedOperations.Add("c8y_Restart");
            supportedOperations.Add("c8y_Configuration");

            //Will set the supported operations of the device
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetSupportedOperations(
                                        supportedOperations,
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        [Ignore("Firewall")]
        public void ClientTest_WsConnection_SetFirmware()
        {
            //Will set the firmware installed on the device
            var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetFirmware(
                                        "Extreme",
                                        "Ultra 1.0",
                                        @"http://sth.url",
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_SetSoftwareList()
        {
            //Will set the list of software installed on the device
            List<Software> list = new List<Software>();
            list.Add(new Software() { Name = "Software01", Url = "url1", Version = "1.0" });
            list.Add(new Software() { Name = "Software02", Url = "url2", Version = "2.1" });

            var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetSoftwareList(list,
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }

        [Test]
        public void ClientTest_WsConnection_SetRequiredAvailability()
        {
            //Will set the required interval for availability monitoring.
            //It will only set the value if does not exist. Values entered e.g. through UI are not overwritten.

            var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetRequiredAvailability(60,
                                        (e) => { return Task.FromResult(false); })).Result;
            Assert.IsTrue(res2);
        }
    }
}