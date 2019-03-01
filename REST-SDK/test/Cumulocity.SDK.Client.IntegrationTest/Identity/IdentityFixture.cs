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

			PlatformImpl platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));


			this.platform = platform;
		}

		public void Dispose()
		{
			//platform?.Dispose();
		}
	}
}