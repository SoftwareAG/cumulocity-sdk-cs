using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.IntegrationTest.Alarm;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.DeviceControl
{
	public class DeviceControlIT : IClassFixture<DeviceControlFixture>, IDisposable
	{
		public DeviceControlIT(DeviceControlFixture fixture)
		{
			//createManagedObjects();
			//deviceControlResource = fixture.platform.DeviceControlApi();
			//operation1 = null;
			//allOperations = null;
		}
		public void Dispose()
		{
			
		}
	}
}
