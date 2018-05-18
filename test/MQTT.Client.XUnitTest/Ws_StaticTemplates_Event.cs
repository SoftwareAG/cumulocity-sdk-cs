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
