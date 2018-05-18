using MQTTnet;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Xunit;

namespace Cumulocity.MQTT.XUnitTest
{
	[Collection("Client collection")]
	public class Ws_StaticTemplates_OperationsEvents
    {
		readonly Client cl;
	    private string clientId;
	    private readonly Mock<IMqttClient> mqttClient;
		public Ws_StaticTemplates_OperationsEvents(ClientFixture fixture)
	    {
		    this.cl = fixture.cl;
		    mqttClient = new Mock<IMqttClient>();
		}

		//Restart (510)
		[Fact]
		public void ClientTest_WsConnection_Restart()
		{
			cl.RestartEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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

		////Command (511)
		[Fact]
		public void ClientTest_WsConnection_Command()
		{
			cl.CommandEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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


		////Firmware (515)
		[Fact]
		public void ClientTest_WsConnection_Firmware()
		{
			cl.FirmwareEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////Software list (516)
		[Fact]
		public void ClientTest_WsConnection_SoftwareList()
		{
			cl.SoftwareListEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////Measurement request operation (517)
		[Fact]
		public void ClientTest_WsConnection_MeasurementRequestOperation()
		{
			cl.MeasurementRequestOperationEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		[Fact]
		public void ClientTest_WsConnection_Relay()
		{
			cl.RelayEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////RelayArray (519)
		[Fact]
		public void ClientTest_WsConnection_RelayArray()
		{
			cl.RelayArrayEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////Upload configuration file (520)
		[Fact]
		public void ClientTest_WsConnection_UploadConfigurationFile()
		{
			cl.UploadConfigurationFileEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////Download configuration file (521)
		[Fact]
		public void ClientTest_WsConnection_DownloadConfigurationFile()
		{
			cl.DownloadConfigurationFileEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////Logfile request (522)
		[Fact]
		public void ClientTest_WsConnection_LogfileRequest()
		{
			cl.LogfileRequestEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
		////Communication mode (523)
		[Fact]
		public void ClientTest_WsConnection_CommunicationMode()
		{
			cl.CommunicationModeEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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

		////Get children of device (106)
		[Fact]
		public void ClientTest_WsConnection_ChildrenOfDevice()
		{
			cl.ChildrenOfDeviceEvt += (s, e) =>
			{
				Assert.Equal(clientId, s.ToString());
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
