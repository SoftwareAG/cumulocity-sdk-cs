using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cumulocity.MQTT.XUnitTest
{
	[Collection("Client collection")]
	public class Ws_DeviceCredentials
    {
	    readonly Client cl;

	    public Ws_DeviceCredentials(ClientFixture fixture)
	    {
		    this.cl = fixture.cl;
	    }

		[Fact]
	    public void ClientTest_Ws_RequestDeviceCredential()
	    {
		    cl.RequestDeviceCredentialEvt += (s, e) =>
		    {
			    var isTenant = e.Tenant.Length > 0;
			    Assert.True(isTenant);
		    };

		    bool res2 = Task.Run(() => cl.DeviceCredentials.RequestDeviceCredentials((e) => { return Task.FromResult(false); })).Result;

		    Assert.True( res2);

	    }
	}
}
