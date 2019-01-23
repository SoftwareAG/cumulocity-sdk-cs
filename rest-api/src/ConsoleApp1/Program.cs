using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

namespace ConsoleApp1
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("REST API client!");

			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			IPlatform platform = new PlatformImpl("https://piotr.staging.c8y.io",
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));
			IInventoryApi inventory = platform.InventoryApi;

			//Hello, world
			var mo_hw = new ManagedObjectRepresentation();
			mo_hw.Name = "Hello, world!";
			mo_hw.set(new IsDevice());
			//mo_hw.set(new Firmware() { Name = "A", Url = "U" });
			var mo_hw3 = inventory.create(mo_hw);
			Console.WriteLine($"Url: {mo_hw.Self}");

			////New electricity meter with a relay
			//ManagedObjectRepresentation mo_mm = new ManagedObjectRepresentation();
			//mo_mm.Name = "MyMeter-1";
			//Relay relay = new Relay();
			//mo_mm.set(new IsDevice());
			//mo_mm.set(relay);
			//SinglePhaseElectricitySensor meter = new SinglePhaseElectricitySensor();
			//mo_mm.set(meter);
			//inventory.create(mo_mm);

			//InventoryFilter inventoryFilter = new InventoryFilter();
			//inventoryFilter.byFragmentType(typeof(Position));

			//IManagedObjectCollection moc = inventory.getManagedObjectsByFilter(inventoryFilter);

			//foreach (ManagedObjectRepresentation mo in moc.get().allPages())
			//{
			//	Console.WriteLine(mo.Id);
			//}

			Console.ReadKey();
		}
	}
}