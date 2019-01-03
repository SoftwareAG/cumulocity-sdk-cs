using Cumulocity.SDK.Client.Rest.API.Cep;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.Representation.Cep;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Cep
{
	public class CepApiIT : IClassFixture<CepApiFixture>, IDisposable
	{
		private CepApiFixture fixture;
		private ICepApi cepApi;

		public CepApiIT(CepApiFixture fixture)
		{
			this.fixture = fixture;
			this.cepApi = fixture.platform.CepApi;
		}

		public void Dispose()
		{
			PagedCepModuleCollectionRepresentation<CepModuleCollectionRepresentation> cep = cepApi.Modules.get();
			IEnumerable<CepModuleRepresentation> it = cep.allPages();

			foreach (CepModuleRepresentation cepModule in it)
			{
				cepApi.delete(cepModule);
			}
		}

		[Fact]
		public void shouldCreateCepModule()
		{
			//Given
			string cepModuleFile = readCepModuleFile("cep/test-module.epl");
			//When
			CepModuleRepresentation cepModule = cepApi.create(cepModuleFile);
			//Then
			Assert.NotNull(cepModule);
			Assert.NotNull(cepModule.Id);
			Assert.NotEmpty(cepModule.Id);
		}

		[Fact]
		public void shouldDeleteCepModule()
		{
			//Given
			CepModuleRepresentation cepModule = cepApi.create(readCepModuleFile("cep/test-module.epl"));
			//When
			cepApi.delete(cepModule);
			//Then
			Assert.NotNull(cepModule);
			Assert.NotNull(cepModule.Id);
			Assert.NotEmpty(cepModule.Id);
		}

		private string readCepModuleFile(string module)
		{
			var location = typeof(CepApiIT).GetTypeInfo().Assembly.Location;
			var dirPath = Path.GetDirectoryName(location);
			var appConfigPath = Path.Combine(dirPath, "Resources/Cep/test-module.epl");
			string readText = File.ReadAllText(appConfigPath, Encoding.UTF8);
			return readText;
		}

		public class Handler : ISubscriptionListener<string, object>
		{
			public Handler()
			{
			}

			public void onError(ISubscription<string> subscription, Exception ex)
			{
			}

			public void onNotification(ISubscription<string> subscription, object notification)
			{
			}
		}
	}
}