#region Cumulocity GmbH

// /*
//  * Copyright (C) 2015-2018
//  *
//  * Permission is hereby granted, free of charge, to any person obtaining a copy of
//  * this software and associated documentation files (the "Software"),
//  * to deal in the Software without restriction, including without limitation the rights to use,
//  * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
//  * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  *
//  * The above copyright notice and this permission notice shall be
//  * included in all copies or substantial portions of the Software.
//  *
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  */

#endregion

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
