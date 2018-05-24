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
	public class Ws_StaticTemplates_Event
    {
	    readonly Client cl;
		public Ws_StaticTemplates_Event(ClientFixture fixture)
	    {
		    this.cl = fixture.cl;
	    }

	    //Create basic event (400)
	    [Fact]
	    public void ClientTest_WsConnection_CreateBasicEvent()
	    {
		    var res2 = Task.Run(() => cl.StaticEventTemplates.CreateBasicEventAsync("c8y_MyEvent", "Something was triggered", string.Empty, (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }
	    //Create location update event (401)
	    [Fact]
	    public void ClientTest_WsConnection_CreateLocationUpdateEvent()
	    {
		    var res2 = Task.Run(() => cl.StaticEventTemplates.CreateLocationUpdateEventAsync(
			    "52.209538",
			    "16.831992",
			    "76",
			    "134",
			    string.Empty,
			    (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }
	    //Create location update event with device update (402)
	    [Fact]
	    public void ClientTest_WsConnection_CreateLocationUpdateEventWithDeviceUpdate()
	    {
		    var res2 = Task.Run(() => cl.StaticEventTemplates.CreateLocationUpdateEventWithDeviceUpdateAsync(
			    "52.209538",
			    "16.831992",
			    "76",
			    "134",
			    string.Empty,
			    (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }
	}
}
