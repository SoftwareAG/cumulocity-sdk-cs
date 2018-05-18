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

