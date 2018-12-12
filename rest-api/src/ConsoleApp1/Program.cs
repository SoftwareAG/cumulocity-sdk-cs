using System;
using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    internal class Program
    {
	    public static IConfigurationRoot Configuration { get; set; }
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
            mo_hw.set(new Firmware(){Name = "A",Url = "U"});
            var mo_hw3 = inventory.create(mo_hw);
            
            //New electricity meter with a relay
            ManagedObjectRepresentation mo_mm = new ManagedObjectRepresentation();
            mo_mm.Name = "MyMeter-1";
            Relay relay = new Relay();
            mo_mm.set(new IsDevice());
            mo_mm.set(relay);
            SinglePhaseElectricitySensor meter = new SinglePhaseElectricitySensor();
            mo_mm.set(meter);
            mo_mm = inventory.create(mo_mm);            
            
            //Inventory Filter          
            InventoryFilter inventoryFilter = new InventoryFilter();
            inventoryFilter.byFragmentType(typeof(Position));
            
            IManagedObjectCollection moc = inventory.getManagedObjectsByFilter(inventoryFilter);
            
            foreach (ManagedObjectRepresentation mo in moc.get().allPages())
            {
                Console.WriteLine(mo.Id);
            }
            
            Console.ReadKey();
        }

	    private static ServiceProvider ServiceProvider()
	    {
		    var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

		    var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
		                        devEnvironmentVariable.ToLower() == "development";

		    var builder = new ConfigurationBuilder();
		    builder
			    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

		    if (isDevelopment)
		    {
			    builder.AddUserSecrets<Program>();
		    }

		    Configuration = builder.Build();

		    IServiceCollection services = new ServiceCollection();

		    services
			    .Configure<SecretStuff>(Configuration.GetSection(nameof(SecretStuff)))
			    .AddOptions()
			    .AddSingleton<ISecretRevealer, SecretRevealer>()
			    .BuildServiceProvider();

		    var serviceProvider = services.BuildServiceProvider();
		    return serviceProvider;
	    }
    }
}