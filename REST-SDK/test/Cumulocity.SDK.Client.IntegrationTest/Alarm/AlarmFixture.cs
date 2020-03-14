using System;
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

			Console.WriteLine(" USER-SECRETS in AlarmFixture " + secretRevealer.Reveal());

			PlatformImpl platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));

			
			this.platform = platform;
		}

		public void Dispose()
		{
			var inventory = platform.InventoryApi;
			List<GId> lst = new List<GId>();
			foreach (var item in inventory.ManagedObjects.GetFirstPage().AllPages())
			{
				lst.Add(item.Id);
			}

			foreach (var id in lst)
			{
#pragma warning disable 0612
				var mo = inventory.GetManagedObject(id);
				mo.Delete();
			}
		}
	}
}
