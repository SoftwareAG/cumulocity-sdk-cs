using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model.Authentication;

namespace Cumulocity.SDK.Client.IntegrationTest
{
	public class PlatformFixture : IDisposable
	{
		ISecretRevealer secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);
		public PlatformFixture()
		{
			 Platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));
		}

		public IPlatform Platform { get; set; }

		public void Dispose()
		{
			
		}
	}
}
