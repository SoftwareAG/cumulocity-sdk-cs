using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest
{
	[CollectionDefinition("Platform collection")]
	public class PlatformCollection : ICollectionFixture<PlatformFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
		// to be the place to Apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}
}
