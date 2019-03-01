using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;

namespace Cumulocity.SDK.Client.IntegrationTest.DeviceControl
{
	public class DeviceCredentialsFixture : IDisposable
	{
		public PlatformImpl platform;
		public PlatformImpl bootstrap;

		public DeviceCredentialsFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			PlatformImpl platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));

			PlatformImpl bootstrap = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().userbootstrap, secretRevealer.Reveal().passbootstrap))
			{
				ProxyHost = "127.0.0.1",
				ProxyPort = 8888
			};

			this.platform = platform;
			this.bootstrap = bootstrap;
		}

		public void Dispose()
		{
			platform?.Dispose();
		}
	}
}
