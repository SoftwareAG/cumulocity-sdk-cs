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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cumulocity.MQTT.XUnitTest
{
	[Collection("Client collection")]
	public class Ws_StaticTemplates_Measurement
    {
	    readonly Client cl;
		public Ws_StaticTemplates_Measurement(ClientFixture fixture)
	    {
			this.cl = fixture.cl;
		}

	    [Fact]
	    public void ClientTest_WsConnection_CreateCustomMeasurement()
	    {
		    var res2 = Task.Run(() => cl.StaticMeasurementTemplates.CreateCustomMeasurementAsync("c8y_Temperature", "T", "25", string.Empty, string.Empty, (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }
	    [Fact]
	    public void ClientTest_WsConnection_CreateSignalStrengthMeasurement()
	    {
		    var res2 = Task.Run(() => cl.StaticMeasurementTemplates.CreateSignalStrengthMeasurementAsync("-90", "23", "2017-09-13T14:00:14.000+02:00", (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }
	    [Fact]
	    public void ClientTest_WsConnection_CreateTemperatureMeasurement()
	    {
		    var res2 = Task.Run(() => cl.StaticMeasurementTemplates.CreateTemperatureMeasurementAsync("25", "2017-09-13T15:01:14.000+02:00", (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }

	    [Fact]
	    public void ClientTest_WsConnection_CreateBatteryMeasurement()
	    {
		    var res2 = Task.Run(() => cl.StaticMeasurementTemplates.CreateBatteryMeasurementAsync("95", "2017-09-13T15:01:14.000+02:00", (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }

	}
}
