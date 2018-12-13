using System;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;

namespace Cumulocity.SDK.Client.IntegrationTest.Identity
{
	public class IdentityFixture: IDisposable
	{
		public PlatformImpl platform;

		public IdentityFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			PlatformImpl platform = new PlatformImpl("https://piotr.staging.c8y.io",
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));

			platform.ProxyHost = "127.0.0.1";
			platform.ProxyPort = 8888;

			this.platform = platform;
		}

		public void Dispose()
		{
			//platform?.Dispose();
		}
	}
}