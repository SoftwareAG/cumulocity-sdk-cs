using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cumulocity.SDK.Client.HelperTest
{
	public class TestHelper
	{
		public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
		{
			Console.WriteLine("Value of outputPath " + outputPath );


			return new ConfigurationBuilder()
				.SetBasePath(outputPath)
				.AddJsonFile("appsettings.json", optional: true)
				.AddUserSecrets("7DE4638C-7004-43A9-87D4-E808F2ED24AB")
				.Build();
		}

		public static ISecretRevealer GetApplicationConfiguration(string outputPath)
		{
			Console.WriteLine("Value of outputPath " + outputPath);
			var iConfig = GetIConfigurationRoot(outputPath);

			IServiceCollection services = new ServiceCollection();

			services
				.Configure<SecretPlatform>(iConfig.GetSection(nameof(SecretPlatform)))
				.AddOptions()
				.AddSingleton<ISecretRevealer, SecretRevealer>()
				.BuildServiceProvider();

			var serviceProvider = services.BuildServiceProvider();

			return serviceProvider.GetService<ISecretRevealer>();
		}
	}
}
