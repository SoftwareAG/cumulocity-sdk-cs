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
	public class Ws_StaticTemplates_Operations
    {
	    readonly Client cl;
		public Ws_StaticTemplates_Operations(ClientFixture fixture)
	    {
		    this.cl = fixture.cl;
	    }

	    [Fact]
	    public void ClientTest_WsConnection_GetPendingOperations()
	    {
		    //Will trigger the sending of all PENDING operations for the agent.
		    var res2 = Task.Run(() => cl.StaticOperationTemplates.GetPendingOperationsAsync((e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }

	    [Fact]
	    public void ClientTest_WsConnection_GetExecutingOperations()
	    {
		    //Will set the oldest EXECUTING operation with given fragment to FAILED
		    var res2 = Task.Run(() => cl.StaticOperationTemplates.SetExecutingOperationsAsync("c8y_Restart", (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }

	    [Fact]
	    public void ClientTest_WsConnection_SetOperationToFailed()
	    {
		    //Will set the oldest EXECUTING operation with given fragment to SUCCESSFUL.
		    //It enables the device to send additional parameters that trigger additional steps based on the type of operation send as fragment (see section Updating operations).
		    var res2 = Task.Run(() => cl.StaticOperationTemplates.SetOperationToFailedAsync("c8y_Restart", "Could not restart", (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }

	    [Fact]
	    public void ClientTest_WsConnection_SetOperationToSuccessful()
	    {
		    //
		    var res2 = Task.Run(() => cl.StaticOperationTemplates.SetOperationToSuccessfulAsync("c8y_Restart", string.Empty, (e) => { return Task.FromResult(false); })).Result;
		    Assert.True(res2);
	    }
	}
}

