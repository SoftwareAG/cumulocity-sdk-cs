using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Option;
using Cumulocity.SDK.Client.Rest.Model.Option;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Option
{
	public class TenantOptionIT : IClassFixture<TenantOptionFixture>, IDisposable
	{
		private const string CATEGORY = "test.category";
		private const string KEY = "test.key";

		private readonly ITenantOptionApi tenantOptionApi;
		private readonly PlatformImpl _fixture;
		public TenantOptionIT(TenantOptionFixture fixture)
		{
			_fixture = fixture.platform;
			this.tenantOptionApi = fixture.platform.TenantOptionApi;
		}

		public void Dispose()
		{
			foreach (var option in tenantOptionApi.GetAllOptionsForCategory(CATEGORY))
			{
				tenantOptionApi.Delete(new OptionPK(option.Category, option.Key));
			}
			tenantOptionApi.GetAllOptionsForCategory(CATEGORY);
			_fixture?.Dispose();
		}

		[Fact]
		public void ShouldCreateOption()
		{
			OptionRepresentation expectedOption = OptionRepresentation.asOptionRepresetation(CATEGORY, KEY, "value");
			OptionRepresentation option = tenantOptionApi.Save(expectedOption);
			OptionRepresentation savedOption = tenantOptionApi.GetOption(new OptionPK(CATEGORY, KEY));

			Assert.Equal(expectedOption, option);
			Assert.Equal(expectedOption, savedOption);
		}

		[Fact]
		public void ShouldUpdateOption()
		{
			OptionRepresentation expectedOption = OptionRepresentation.asOptionRepresetation(CATEGORY, KEY, "value");
			OptionRepresentation option = tenantOptionApi.Save(expectedOption);

			OptionRepresentation savedOption = tenantOptionApi.GetOption(new OptionPK(CATEGORY, KEY));
			Assert.Equal(expectedOption, option);
			Assert.Equal(expectedOption, savedOption);

			expectedOption = OptionRepresentation.asOptionRepresetation(CATEGORY, KEY, "value3");
			option = tenantOptionApi.Save(expectedOption);
			savedOption = tenantOptionApi.GetOption(new OptionPK(CATEGORY, KEY));
			Assert.Equal(expectedOption, option);
			Assert.Equal(expectedOption, savedOption);
		}

		[Fact]
		public void ShouldGetAllOptions()
		{
			List<OptionRepresentation> expectedOptions = sampleOptions(CATEGORY, KEY, 10);
			saveOptions(expectedOptions);
			ITenantOptionCollection options = tenantOptionApi.Options;
			var optionCollection = options.GetFirstPage(1000);

			assertExpectedOptionsFound(optionCollection.AllPages(), expectedOptions);
		}



		[Fact]
		public void ShouldGetOptionsForSingleCategory()
		{
			int expectedCount = 5;
			List<OptionRepresentation> expectedOptions = sampleOptions(CATEGORY, KEY, expectedCount);
			saveOptions(expectedOptions);
			var options = tenantOptionApi.GetAllOptionsForCategory(CATEGORY);

			Assert.Equal(expectedCount, options.Count);
			assertExpectedOptionsFound(options, expectedOptions);
		}

		[Fact]
		public void ShouldDeleteOption()
		{
			OptionPK optionPK = new OptionPK(CATEGORY, KEY);

			tenantOptionApi.Save(OptionRepresentation.asOptionRepresetation(CATEGORY, KEY, "value"));
			OptionRepresentation savedOption = tenantOptionApi.GetOption(optionPK);
			Assert.NotNull(savedOption);

			tenantOptionApi.Delete(new OptionPK(CATEGORY, KEY));
			try
			{
				tenantOptionApi.GetOption(optionPK);
			}
			catch (SDKException e)
			{
				Assert.Equal( 404, e.HttpStatus);
				return;
			}
		}

		private List<OptionRepresentation> sampleOptions(String category, String keyPrefix, int count)
		{
			List<OptionRepresentation> options = new List<OptionRepresentation>();
			for (int i = 0; i < count; i++)
			{
				options.Add(OptionRepresentation.asOptionRepresetation(category, keyPrefix + ".v" + i, "" + i));
			}
			return options;
		}

		private void saveOptions(List<OptionRepresentation> options)
		{
			foreach (var option in options)
			{
				tenantOptionApi.Save(option);
			}
		}

		private void assertExpectedOptionsFound(IEnumerable<OptionRepresentation> iterator, List<OptionRepresentation> expectedOptions)
		{
			int expectedCount = expectedOptions.Count;
			foreach (var option in iterator)
			{
				foreach (OptionRepresentation expectedOption in expectedOptions)
				{
					if (expectedOption.Equals(option))
					{
						expectedCount--;
						if (expectedCount == 0)
						{
							break;
						}
					}
				}
			}
			Assert.Equal(0, expectedCount);
		}
	}
}
