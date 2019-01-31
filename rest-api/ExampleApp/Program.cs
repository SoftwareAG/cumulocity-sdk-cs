using Cumulocity.SDK.Client.HelperTest;
using System;
using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace ExampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("REST API client!");

			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			//
			//Connecting to Cumulocity
			//

			IPlatform platform = new PlatformImpl("https://piotr.staging.c8y.io",
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));
			IInventoryApi inventory = platform.InventoryApi;

			//Accessing the inventory
			var mo_hw = new ManagedObjectRepresentation();
			mo_hw.Name = "Hello, world!";
			mo_hw.Set(new IsDevice());
			mo_hw.Set(new Position() { Alt = 0.1m, Lat = 40.0m, Lng = 50m });
			var mo_hw1 = inventory.Create(mo_hw);
			Console.WriteLine($"Url: {mo_hw.Self}");



			InventoryFilter inventoryFilter = new InventoryFilter();
			inventoryFilter.ByFragmentType(typeof(Position));

			IManagedObjectCollection moc = inventory.GetManagedObjectsByFilter(inventoryFilter);

			foreach (ManagedObjectRepresentation mor in moc.GetFirstPage().AllPages())
			{
				Console.WriteLine(mor.Id);
			}
		}
	}
}
