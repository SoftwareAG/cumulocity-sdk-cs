using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using System;

namespace Cumulocity.SDK.Client.IntegrationTest.Cep
{
	public class CepApiFixture
	{
		public PlatformImpl platform;

		public CepApiFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			PlatformImpl platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));


			this.platform = platform;
		}

		public void Dispose()
		{
			platform?.Dispose();
		}
	}
}