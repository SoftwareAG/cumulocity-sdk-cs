using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;
using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.Model.Event;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace ExampleApp
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

			IPlatform platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));
			IInventoryApi inventory = platform.InventoryApi;

			//Accessing the inventory
			var mo_hw = new ManagedObjectRepresentation();
			mo_hw.Name = "Hello, world!";
			mo_hw.Set(new IsDevice());
			mo_hw.Set(new Position() { Alt = 0.1m, Lat = 40.0m, Lng = 50m });
			var mo_hw1 = inventory.Create(mo_hw);
			Console.WriteLine($"Url: {mo_hw1.Self}");

			InventoryFilter inventoryFilter = new InventoryFilter();
			inventoryFilter.ByFragmentType(typeof(Position));

			IManagedObjectCollection moc = inventory.GetManagedObjectsByFilter(inventoryFilter);

			//This returns a query to get the objects
			foreach (ManagedObjectRepresentation mor in moc.GetFirstPage().AllPages())
			{
				Console.WriteLine(mor.Id);
			}

			//
			ManagedObjectRepresentation mo = new ManagedObjectRepresentation();
			mo.Name = "MyMeter-1";
			mo.Set(new IsDevice());
			Relay relay = new Relay();
			mo.Set(relay);
			SinglePhaseElectricitySensor meter = new SinglePhaseElectricitySensor();
			mo.Set(meter);
			// Set additional properties, e.g., tariff tables, ...
			Tariff tariff = new Tariff();
			mo.Set(tariff);
			mo = inventory.Create(mo);
			Console.WriteLine(mo.Id);

			//Accessing the identity service
			const string ASSET_TYPE = "com_cumulocity_idtype_AssetTag";
			const string deviceUrl = "SAMPLE-A-239239232";

			ExternalIDRepresentation externalIDGid = new ExternalIDRepresentation();
			externalIDGid.Type = ASSET_TYPE;
			externalIDGid.ExternalId = deviceUrl;
			externalIDGid.ManagedObject = mo;
			IIdentityApi identityApi = platform.IdentityApi;
			identityApi.Create(externalIDGid);

			//Now, if you need the association back,
			//you can just query the identity service as follows:
			ID id = new ID();
			id.Type = ASSET_TYPE;
			id.Value = deviceUrl;
			var getExternalIDGid = identityApi.GetExternalId(id);

			//
			//Accessing events and measurements
			//

			IMeasurementApi measurementApi = platform.MeasurementApi;

			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.DateTime = DateTime.UtcNow;
			rep.Source = mo_hw1;
			rep.Type = "com.signalstrength";
			rep.Set(new SignalStrength() { Rssi = new MeasurementValue() { Unit = "dBm", Value = -63m } });
			measurementApi.Create(rep);

			MeasurementRepresentation rep2 = new MeasurementRepresentation();
			rep2.DateTime = DateTime.UtcNow;
			rep2.Source = mo_hw1;
			rep2.Type = "c8y_PTCMeasurement";
			rep2.Set(new TemperatureMeasurement() { T = new MeasurementValue() { Unit = "C", Value = 23.1m } });
			measurementApi.Create(rep2);

			MeasurementFilter measurementFilter = new MeasurementFilter();

			var toDate = DateTime.Now;
			var fromDate = DateTime.Now.AddDays(-14);
			measurementFilter.ByDate(fromDate, toDate);
			measurementFilter.ByFragmentType(typeof(SignalStrength));
			IMeasurementCollection mc = measurementApi.GetMeasurementsByFilter(measurementFilter);

			foreach (var measurement in mc.GetFirstPage().AllPages())
			{
				SignalStrength signal = measurement.Get<SignalStrength>();
				Console.WriteLine(measurement.Source.Id + " " + measurement.DateTime + " " + signal.RssiValue + " " + signal.BerValue);
			}
			//
			//Alarm
			//
			IAlarmApi alarmApi = platform.AlarmApi;
			alarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
				.withType("com_nsn_bts_TrxFaulty")
				.withStatus("ACTIVE")
				.withSeverity("CRITICAL")
				.withSource(mo_hw1).build());

			AlarmFilter acknowledgedFilter = new AlarmFilter().ByStatus(CumulocityAlarmStatuses.valueOf("ACTIVE"));

			foreach (AlarmRepresentation result in alarmApi.GetAlarmsByFilter(acknowledgedFilter).GetFirstPage().AllPages())
			{
				Console.WriteLine(result.Status);

			}

			//Controlling devices
			IDeviceControlApi deviceControlApi = platform.DeviceControlApi;
			ManagedObjectRepresentation agent = new ManagedObjectRepresentation();
			agent.Set(new Agent()); // agents must include this fragment
									// ... Create agent in inventory
			var agentMor =inventory.Create(agent);
			ManagedObjectRepresentation device = new ManagedObjectRepresentation();
			device.Name = "Device!";
			device.Set(new IsDevice());
			// ... Create device in inventory
			var deviceMor = inventory.Create(device);
			ManagedObjectReferenceRepresentation child2Ref = new ManagedObjectReferenceRepresentation();
			child2Ref.ManagedObject = deviceMor;
			inventory.GetManagedObject(agentMor.Id).AddChildDevice(child2Ref);

			IDeviceControlApi control = platform.DeviceControlApi;
			OperationRepresentation operation = new OperationRepresentation
			{
				DeviceId = deviceMor.Id
			};
			relay.SetRelayState(Relay.RelayState.OPEN);
			operation.Set(relay);
			control.Create(operation);
			//Now, if you would like to query the pending operations from an agent, the following code would need to be executed:
			OperationFilter operationFilter = new OperationFilter();
			operationFilter.ByAgent(agentMor.Id.Value);
			operationFilter.ByStatus(OperationStatus.PENDING);
			IOperationCollection oc = deviceControlApi.GetOperationsByFilter(operationFilter);

			//Again, the returned result may come in several pages due to its potential size.

			foreach (OperationRepresentation op in oc.GetFirstPage().AllPages())
			{
				Console.WriteLine(op.Status);
			}
		}
	}

	[PackageName("tariff")]
	public class Tariff
	{
		[JsonProperty("nightTariffStart")]
		public int NightTariffStart
		{
			get
			{
				return nightTariffStart;
			}
			set
			{
				this.nightTariffStart = value;
			}
		}

		[JsonProperty("nightTariffEnd")]
		public int NightTariffEnd
		{
			get
			{
				return nightTariffEnd;
			}
			set
			{
				this.nightTariffEnd = value;
			}
		}

		private int nightTariffStart = 22;
		private int nightTariffEnd = 6;
	}
}