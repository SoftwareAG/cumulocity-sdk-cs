using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest
{
	public class InventoryFixture : IDisposable
	{
		public PlatformImpl platform;
		
		public InventoryFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);
			Console.Write("platformurl:" +  secretRevealer.Reveal().platformurl);
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
#pragma warning disable 0612
				mo.Delete();
			}
		}
	}
	
	[CollectionDefinition("Live tests")]
    public class IntegrationTestCollection : ICollectionFixture<InventoryFixture>
    {
        // Intentionally left blank.
        // This class only serves as an anchor for CollectionDefinition.
    }
}
