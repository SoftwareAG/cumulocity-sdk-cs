using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Measurement
{
	public class MeasurementIT : IClassFixture<MeasurementFixture>, IDisposable
	{
		private static List<ManagedObjectRepresentation> managedObjects = new List<ManagedObjectRepresentation>();
		private PlatformImpl _fixture;

		public MeasurementIT(MeasurementFixture fixture)
		{
			_fixture = fixture.platform;
			Input = new List<MeasurementRepresentation>();
			MeasurementApi = fixture.platform.MeasurementApi;
			Result1 = new List<MeasurementRepresentation>();
			Result2 = new List<MeasurementRepresentation>();
			Status = 200;
			createManagedObject();
		}

		public List<MeasurementRepresentation> Input { get; set; }

		public IMeasurementApi MeasurementApi { get; set; }

		public int Status { get; set; }

		public List<MeasurementRepresentation> Result2 { get; set; }

		public List<MeasurementRepresentation> Result1 { get; set; }

		public void createManagedObject()
		{
			//    Background:
			//    Given I have a platform and a tenant
			//    And I have '3' managed objects
			//    And I create all
			for (int i = 0; i < 3; ++i)
			{
				var mo = _fixture.InventoryApi.create(aSampleMo().withName("MO" + i).build());
				managedObjects.Add(mo);
			}
		}

		public void deleteManagedObjects()
		{
			IList<MeasurementRepresentation> measOn1stPage = getMeasurementsFrom1stPage();

			while (measOn1stPage.Count > 0)
			{
				deleteMeasurements(measOn1stPage);
				measOn1stPage = getMeasurementsFrom1stPage();
			}
		}

		private IList<MeasurementRepresentation> getMeasurementsFrom1stPage()
		{
			return MeasurementApi.Measurements.get().Measurements;
		}

		private void deleteMeasurements(IList<MeasurementRepresentation> measOn1stPage)
		{
			foreach (MeasurementRepresentation m in measOn1stPage)
			{
				MeasurementApi.deleteMeasurement(m);
			}
		}

		private static ManagedObjectRepresentationBuilder aSampleMo()
		{
			return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
				.MO_REPRESENTATION);
		}

		public void Dispose()
		{
			//deleteManagedObjects();
		}

		//
		//    Scenario: Create measurements
		[Fact]
		public void createMeasurements()
		{
			//    Given I have '2' measurements of type 'com.type1' for the managed object
			iHaveMeasurements(2, "com.type1");
			//    When I create all measurements
			iCreateAll();
			//    Then All measurements should be created
			allShouldBeCreated();
		}

		[Fact]
		public void createMeasurementsWithoutTime()
		{
			//    Given I have a measurement of type 'com.type1' and no time value for the managed object
			iHaveAMeasurementWithNoTime("com.type1");
			//    When I create all measurements
			iCreateAll();
			//    Then Measurement response should be unprocessable
			shouldBeBadRequest();
		}

		// ------------------------------------------------------------------------
		// Given
		// ------------------------------------------------------------------------

		//@Given("I have '(\\d+)' measurements of type '([^']*)' for the managed object")

		public void iHaveMeasurements(int n, String type)
		{
			for (int i = 0; i < n; i++)
			{
				MeasurementRepresentation rep = new MeasurementRepresentation();
				rep.Type = type;
				rep.DateTime = DateTime.UtcNow;
				rep.Source = managedObjects[0];
				Input.Add(rep);
			}
		}

		//@Given("I have '(\\d+)' measurements with fragment type '([^']*)' for the managed object")

		public void iHaveMeasurementsWithFragments(int n, String fragmentType)
		{
			for (int i = 0; i < n; i++)
			{
				MeasurementRepresentation rep = new MeasurementRepresentation();
				rep.DateTime = DateTime.UtcNow;
				rep.Type = "com.type1";
				rep.Source = managedObjects[0];

				// Set fragment
				Object fragment = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				rep.set(fragment);

				Input.Add(rep);
			}
		}

		//@Given("I have a measurement of type '([^']*)' and no time value for the managed object")

		public void iHaveAMeasurementWithNoTime(String type)
		{
			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.Type = type;
			rep.Source = managedObjects[0];
			Input.Add(rep);
		}

		// ------------------------------------------------------------------------
		// When
		// ------------------------------------------------------------------------

		//@When("I create all measurements")
		public void iCreateAll()
		{
			try
			{
				foreach (MeasurementRepresentation rep in Input)
				{
					Result1.Add(MeasurementApi.create(rep));
				}
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I create all measurements as bulk")
		public void iCreateAllBulk()
		{
			try
			{
				MeasurementCollectionRepresentation collection = new MeasurementCollectionRepresentation();
				collection.Measurements = Input;
				Result1.AddRange(MeasurementApi.createBulk(collection).Measurements);
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		// ------------------------------------------------------------------------
		// Then
		// ------------------------------------------------------------------------

		//@Then("All measurements should be created")
		public void allShouldBeCreated()
		{
			Assert.Equal(Input.Count, Result1.Count);
			foreach (MeasurementRepresentation rep in Result1)
			{
				Assert.NotNull(rep.Id);
			}
		}

		//@Then("Measurement response should be unprocessable")
		public void shouldBeBadRequest()
		{
			Assert.Equal(422, Status);
		}
	}
}