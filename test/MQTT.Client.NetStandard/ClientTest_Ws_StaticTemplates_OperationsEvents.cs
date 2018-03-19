using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using Cumulocity.MQTT.Interfaces;
using MQTT.Test;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Cumulocity.MQTT.Test
{
    [TestFixture]
    internal class ClientTest_Ws_StaticTemplates_OperationsEvents
    {

        private Client cl;
        private string clientId;
        private Mock<IMqttClient> mqttClient;
        [SetUp]
        public void SetUp()
        {
            var cnf = ConfigData.Instance;
            mqttClient = new Mock<IMqttClient>();
            var config = new Mock<IConfiguration>();
            config.Setup(c => c.Server).Returns(cnf.WsServer);
            config.Setup(c => c.UserName).Returns(cnf.UserName);
            config.Setup(c => c.Password).Returns(cnf.Password);
            config.Setup(c => c.Port).Returns(cnf.WsPort);
            config.Setup(c => c.ConnectionType).Returns("WS");
            config.Setup(c => c.ClientId).Returns(cnf.ClientId);
            clientId = cnf.ClientId;
            cl = new Client(config.Object, mqttClient.Object);

        }

        //Restart (510)
        [Test, MaxTime(2000)]
        public void ClientTest_WsConnection_Restart()
        {
            cl.RestartEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("510,{0}", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }

        //Command (511)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_Command()
        {
            cl.CommandEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("511,{0},execute this", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Configuration (513)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_Configuration()
        {
            cl.ConfigurationEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("513,{0},\"val1 = 1\nval2 = 2\"", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Firmware (515)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_Firmware()
        {
            cl.FirmwareEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("515,{0},myFimrware,1.0,http://www.my.url", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Software list (516)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_SoftwareList()
        {
            cl.SoftwareListEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("516,{0},softwareA,1.0,url1,softwareB,2.0,url2", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Measurement request operation (517)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_MeasurementRequestOperation()
        {
            cl.MeasurementRequestOperationEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("517,{0},LOGA", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Relay (518)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_Relay()
        {
            cl.RelayEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("518,{0},OPEN", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //RelayArray (519)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_RelayArray()
        {
            cl.RelayArrayEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("519,{0},OPEN,CLOSE,CLOSE,OPEN", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };
            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Upload configuration file (520)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_UploadConfigurationFile()
        {
            cl.UploadConfigurationFileEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("520,{0}", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Download configuration file (521)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_DownloadConfigurationFile()
        {
            cl.DownloadConfigurationFileEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("521,{0},http://www.my.url", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Logfile request (522)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_LogfileRequest()
        {
            cl.LogfileRequestEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("522,{0},logfileA,2013-06-22T17:03:14.000+02:00,2013-06-22T18:03:14.000+02:00,ERROR,1000", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }
        //Communication mode (523)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_CommunicationMode()
        {
            cl.CommunicationModeEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("523,{0},SMS", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }

        //Get children of device (106)
        [Test, MaxTime(1000)]
        public void ClientTest_WsConnection_ChildrenOfDevice()
        {
            cl.ChildrenOfDeviceEvt += (s, e) => {
                Assert.AreEqual(clientId, s.ToString());
            };

            var applicationMessage = new MqttApplicationMessage()
            {
                Topic = "s/ds",
                Payload = Encoding.UTF8.GetBytes(String.Format("106,{0}", clientId)),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false
            };

            mqttClient.Raise(e => e.ApplicationMessageReceived += null, new MqttApplicationMessageReceivedEventArgs(clientId, applicationMessage));
        }

    }
}
