using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Globalization;
using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace ConsoleApp1
{
	internal class Program
	{
		private static void Main(string[] args)
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
			mo_hw.Set(new Position(){Alt = 0.1m, Lat = 40.0m , Lng = 50m } );
			var mo_hw1 = inventory.Create(mo_hw);
			Console.WriteLine($"Url: {mo_hw.Self}");



			InventoryFilter inventoryFilter = new InventoryFilter();
			inventoryFilter.ByFragmentType(typeof(Position));

			IManagedObjectCollection moc = inventory.GetManagedObjectsByFilter(inventoryFilter);

			foreach (ManagedObjectRepresentation mor in moc.GetFirstPage().AllPages())
			{
				Console.WriteLine(mor.Id);
			}

			////New electricity meter with a relay
			//ManagedObjectRepresentation mo_mm = new ManagedObjectRepresentation();
			//mo_mm.Name = "MyMeter-1";
			//Relay relay = new Relay();
			//mo_mm.Set(new IsDevice());
			//mo_mm.Set(relay);
			//SinglePhaseElectricitySensor meter = new SinglePhaseElectricitySensor();
			//mo_mm.Set(meter);
			//inventory.Create(mo_mm);

			//InventoryFilter inventoryFilter = new InventoryFilter();
			//inventoryFilter.ByFragmentType(typeof(Position));

			//IManagedObjectCollection moc = inventory.GetManagedObjectsByFilter(inventoryFilter);

			//foreach (ManagedObjectRepresentation mo in moc.GetFirstPage().AllPages())
			//{
			//	Console.WriteLine(mo.Id);
			//}

			ManagedObjectRepresentation mo = new ManagedObjectRepresentation();
			mo.Name = "MyMeter-1";
			Relay relay = new Relay();
			mo.Set(relay);
			SinglePhaseElectricitySensor meter = new SinglePhaseElectricitySensor();
			mo.Set(meter);
			// Set additional properties, e.g., tariff tables, ...
			mo = inventory.Create(mo);
			Console.WriteLine(mo.Id);
			Console.ReadKey();

			IMeasurementApi measurementApi = platform.MeasurementApi;
			MeasurementFilter measurementFilter = new MeasurementFilter();

			var toDate = DateTime.Now;
			var fromDate = DateTime.Now.AddDays(-14);
			measurementFilter.ByDate(fromDate, toDate);
			measurementFilter.ByFragmentType(typeof(SignalStrength));
			IMeasurementCollection mc = measurementApi.GetMeasurementsByFilter(measurementFilter);

			MeasurementCollectionRepresentation measurements = mc.GetFirstPage();

			foreach (var measurement in mc.GetFirstPage().AllPages())
			{
				SignalStrength signal = measurement.Get<SignalStrength>();
				Console.WriteLine(measurement.Source.Id + " " + measurement.DateTime + " " + signal.RssiValue + " " + signal.BerValue);
			}

			IDeviceControlApi control = platform.DeviceControlApi;
			OperationRepresentation operation = new OperationRepresentation();
			operation.DeviceId= mo.Id;
			relay.SetRelayState(Relay.RelayState.OPEN);
			operation.Set(relay);
			control.Create(operation);
		}
	}
}