using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;

namespace Cumulocity.SDK.Client.IntegrationTest.Inventory
{
	public class BinariesApiFixture
	{
		public PlatformImpl platform;

		public BinariesApiFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			PlatformImpl platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));


			this.platform = platform;
		}
	}
}
