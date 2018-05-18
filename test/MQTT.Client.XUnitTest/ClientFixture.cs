using System;
using Cumulocity.MQTT.Model;

namespace Cumulocity.MQTT.XUnitTest
{
	public class ClientFixture : IDisposable
	{
		
		public ClientFixture()
		{
		
			var cnf = new Configuration()
			{
				Server = ConfigData.Instance.WsServer,
				UserName = ConfigData.Instance.UserName,
				Password = ConfigData.Instance.Password,
				ClientId = ConfigData.Instance.ClientId,
				Port = ConfigData.Instance.WsPort,
				ConnectionType = "WS"
			};

			cl = new Client(cnf);
			var item = cl.ConnectAsync().Result;
		}

		public void Dispose()
		{
			cl.DisconnectAsync();
		}

		public Client cl { get; set; }
	}
}