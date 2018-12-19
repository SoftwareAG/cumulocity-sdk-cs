﻿using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.Idtype;

namespace Cumulocity.SDK.Client.IntegrationTest.Alarm
{
	public class AlarmFixture : IDisposable
	{
		public PlatformImpl platform;

		public AlarmFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			PlatformImpl platform = new PlatformImpl("https://piotr.staging.c8y.io",
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass))
			{
				ProxyHost = "127.0.0.1",
				ProxyPort = 8888
			};

			this.platform = platform;
		}

		public void Dispose()
		{
			var inventory = platform.InventoryApi;
			List<GId> lst = new List<GId>();
			foreach (var item in inventory.ManagedObjects.get().allPages())
			{
				lst.Add(item.Id);
			}

			foreach (var id in lst)
			{
				var mo = inventory.getManagedObject(id);
				mo.delete();
			}
		}
	}
}
