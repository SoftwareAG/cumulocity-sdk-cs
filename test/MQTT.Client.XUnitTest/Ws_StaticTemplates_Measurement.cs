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
