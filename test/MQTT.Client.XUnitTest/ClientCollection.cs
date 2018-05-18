using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cumulocity.MQTT.XUnitTest
{


	[CollectionDefinition("Client collection")]
	public class ClientCollection : ICollectionFixture<ClientFixture>
	{
		// Its purpose is simply
		// to be the place to apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}

}
