using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model.Authentication;

namespace Cumulocity.SDK.Client.IntegrationTest
{
	public class PlatformFixture : IDisposable
	{
		public PlatformFixture()
		{
			 Platform = new PlatformImpl("https://piotr.staging.c8y.io",
				new CumulocityCredentials("piotr/admin", "test1234"));
		}

		public IPlatform Platform { get; set; }

		public void Dispose()
		{
			
		}
	}
}
