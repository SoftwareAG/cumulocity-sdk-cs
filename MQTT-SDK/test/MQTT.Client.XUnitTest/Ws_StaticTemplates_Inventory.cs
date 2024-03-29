﻿#region Cumulocity GmbH

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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cumulocity.MQTT.XUnitTest
{
	[Collection("Client collection")]
	public class Ws_StaticTemplates_Inventory
    {
	    private static Random random = new Random();

	    readonly Client cl;

		public Ws_StaticTemplates_Inventory(ClientFixture fixture)
	    {
		    this.cl = fixture.cl;
	    }

	    public static string RandomString(int length)
	    {
		    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		    return new string(Enumerable.Repeat(chars, length)
			    .Select(s => s[random.Next(s.Length)]).ToArray());
	    }

		[Fact]
		public void ClientTest_WsConnection_DeviceCreation()
		{
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.DeviceCreation("TEST", "c8y_MQTTDevice", (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_ConfigureHardware()
		{
			Thread.Sleep(1000);
			//Will update the Hardware properties of the device.
			//If the device does not exist then create a new one
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.ConfigureHardware(RandomString(8), "model", "1.0", (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_ChildDeviceCreation()
		{
			//Will create a new child device for the current device.
			//The newly created object will be added as child device.Additionally an externaId for the child will be created with type “c8y_Serial” and the value a combination of the serial of the root device and the unique child ID.
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.ChildDeviceCreationAsync(RandomString(4), "Device Name", "c8y_MQTTDevice", (e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_GetChildDevices()
		{
			//Will trigger the sending of child devices of the device.
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.GetChildDevices((e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		//ConfigureMobile

		[Fact]
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
			Assert.True(res2);
		}

		[Fact]
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
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_SetConfiguration()
		{
			//Will update the Position properties of the device.
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetConfiguration(
										"val1 = 1\nval2 = 2",
										(e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_SetSupportedOperations()
		{
			IList<string> supportedOperations = new List<string>();
			supportedOperations.Add("c8y_Restart");
			supportedOperations.Add("c8y_Configuration");

			//Will set the supported operations of the device
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetSupportedOperations(
										supportedOperations,
										(e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_SetFirmware()
		{
			//Will set the firmware installed on the device
			var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetFirmware(
										"Extreme",
										"Ultra 1.0",
										@"http://sth.url",
										(e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_SetSoftwareList()
		{
			//Will set the list of software installed on the device
			List<Software> list = new List<Software>();
			list.Add(new Software() { Name = "Software01", Url = "url1", Version = "1.0" });
			list.Add(new Software() { Name = "Software02", Url = "url2", Version = "2.1" });

			var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetSoftwareList(list,
										(e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_SetRequiredAvailability()
		{
			//Will set the required interval for availability monitoring.
			//It will only set the value if does not exist. Values entered e.g. through UI are not overwritten.

			var res2 = Task.Run(() => cl.StaticInventoryTemplates.SetRequiredAvailability(60,
										(e) => { return Task.FromResult(false); })).Result;
			Assert.True(res2);
		}
	}
}
