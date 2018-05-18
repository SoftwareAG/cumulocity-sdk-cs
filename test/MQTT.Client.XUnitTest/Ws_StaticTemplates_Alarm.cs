using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cumulocity.MQTT.XUnitTest
{
	[Collection("Client collection")]
	public class Ws_StaticTemplates_Alarm
	{
		readonly Client cl;
		public Ws_StaticTemplates_Alarm(ClientFixture fixture)
		{
			this.cl = fixture.cl;
		}

		//Create CRITICAL alarm (301)
		[Fact]
		public void ClientTest_WsConnection_CreateCriticalAlarm()
		{
			var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateCriticalAlarmAsync("c8y_TemperatureAlarm", "Alarm of type c8y_TemperatureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
		//Create MAJOR alarm (302)
		[Fact]
		public void ClientTest_WsConnection_CreateMajorAlarm()
		{
			var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateMajorAlarmAsync("c8y_BatteryAlarm", " Major Alarm of type c8y_BatteryAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
		//Create MINOR alarm (303)
		[Fact]
		public void ClientTest_WsConnection_CreateMinorAlarm()
		{
			var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateMinorAlarmAsync("c8y_WaterAlarm", "Alarm of type c8y_WaterAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
		//Create WARNING alarm (304)
		[Fact]
		public void ClientTest_WsConnection_CreateWarningAlarm()
		{
			var res2 = Task.Run(() => cl.StaticAlarmTemplates.CreateWarningAlarmAsync("c8y_AirPressureAlarm", "Warning of type c8y_AirPressureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
		//Update severity of existing alarm (305)
		[Fact]
		public void ClientTest_WsConnection_UpdateSeverityOfExistingAlarm()
		{
			var res2 = Task.Run(() => cl.StaticAlarmTemplates.UpdateSeverityOfExistingAlarmAsync("c8y_AirPressureAlarm", "CRITICAL", (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
		//Clear existing alarm (306)
		[Fact]
		public void ClientTest_WsConnection_ClearExistingAlarm()
		{
			var res2 = Task.Run(() => cl.StaticAlarmTemplates.ClearExistingAlarmAsync("c8y_TemperatureAlarm", (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
	}
}
