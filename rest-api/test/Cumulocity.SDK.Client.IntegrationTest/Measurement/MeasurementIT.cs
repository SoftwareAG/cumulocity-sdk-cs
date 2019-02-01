using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Measurement
{
#pragma warning disable xUnit1013
	public class MeasurementIT : IClassFixture<MeasurementFixture>, IDisposable
	{
		private static List<ManagedObjectRepresentation> managedObjects = new List<ManagedObjectRepresentation>();
		private readonly PlatformImpl _fixture;

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

		private MeasurementCollectionRepresentation Collection1;

		public List<MeasurementRepresentation> Result2 { get; set; }

		public List<MeasurementRepresentation> Result1 { get; set; }

		public void createManagedObject()
		{
			//    Background:
			//    Given I have a platform and a tenant
			//    And I have '3' managed objects
			//    And I Create all
			for (int i = 0; i < 3; ++i)
			{
				var mo = _fixture.InventoryApi.Create(aSampleMo().WithName("MO" + i).build());
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
			return MeasurementApi.Measurements.GetFirstPage().Measurements;
		}

		private void deleteMeasurements(IList<MeasurementRepresentation> measOn1stPage)
		{
			foreach (MeasurementRepresentation m in measOn1stPage)
			{
#pragma warning disable 0612
				MeasurementApi.DeleteMeasurement(m);
			}
		}

		private MeasurementRepresentation aSampleMeasurement(ManagedObjectRepresentation source)
		{
			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.Type = "com.type1";
			rep.DateTime = DateTime.UtcNow;
			rep.Source = source;
			Input.Add(rep);

			rep.Set(new FragmentOne());
			return rep;
		}

		private static ManagedObjectRepresentationBuilder aSampleMo()
		{
			return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
				.MO_REPRESENTATION);
		}

		public void Dispose()
		{
			deleteManagedObjects();
		}

		//
		//    Scenario: Create measurements
		[Fact]
		public void CreateMeasurements()
		{
			//    Given I have '2' measurements of type 'com.type1' for the managed object
			IHaveMeasurements(2, "com.type1");
			//    When I Create all measurements
			ICreateAll();
			//    Then All measurements should be created
			AllShouldBeCreated();
		}

		[Fact]
		public void CreateMeasurementsWithoutTime()
		{
			//    Given I have a measurement of type 'com.type1' and no time value for the managed object
			IHaveAMeasurementWithNoTime("com.type1");
			//    When I Create all measurements
			ICreateAll();
			//    Then Measurement response should be unprocessable
			ShouldBeBadRequest();
		}

		//
		//    Scenario: Get measurement collection by fragment type

		[Fact]
		public void GetMeasurementCollectionByFragmentType()
		{
			//    Given I have '2' measurements with fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' for the managed object
			IHaveMeasurementsWithFragments(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    And I have '3' measurements with fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' for the managed object
			IHaveMeasurementsWithFragments(3, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne'
			IQueryAllByFragmentType("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    Then I should GetFirstPage '2' measurements
			IShouldGetNumberOfMeasurements(2);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo'
			IQueryAllByFragmentType("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    Then I should GetFirstPage '3' measurements
			IShouldGetNumberOfMeasurements(3);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentThree'
			IQueryAllByFragmentType("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentThree");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//
		//    Scenario: Get measurement collection by source

		[Fact]
		public void GetMeasurementCollectionBySource()
		{
			//    Given I have '1' measurements for the source '0' the managed object
			IHaveMeasurementsForSource(1, 0);
			//    And I have '2' measurements for the source '1' the managed object
			IHaveMeasurementsForSource(2, 1);
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by source '0'
			IQueryAllBySource(0);
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1'
			IQueryAllBySource(1);
			//    Then I should GetFirstPage '2' measurements
			IShouldGetNumberOfMeasurements(2);
			//    And I query all measurements by source '2'
			IQueryAllBySource(2);
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement collection by time

		[Fact]
		public void GetMeasurementCollectionByTime()
		{
			var a = getMeasurementsFrom1stPage();
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-18T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-18T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by time from '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllByTime("2018-12-18T10:00:14.9072393Z", "2018-12-18T10:10:14.9072393Z");
			//    Then I should GetFirstPage '2' measurements
			IShouldGetNumberOfMeasurements(2);

			//    And I query all measurements by time from '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllByTime("2018-12-18T09:00:14.9072393Z", "2018-12-18T09:10:14.9072393Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement collection by source and time

		[Fact]
		public void GetMeasurementCollectionBySourceAndTime()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by source '0' and time from '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceAndTime(0, "2018-12-19T10:00:14.9072393Z", "2018-12-19T10:10:14.9072393Z");
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1' and time from '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceAndTime(1, "2018-12-19T10:00:14.9072393Z", "2018-12-19T10:10:14.9072393Z");
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '0' and time from '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceAndTime(0, "2018-12-19T09:00:14.9072393Z", "2018-12-19T09:10:14.9072393Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and time from '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceAndTime(1, "2018-12-19T09:00:14.9072393Z", "2018-12-19T09:10:14.9072393Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement collection by source and fragment type
		[Fact]
		public void GetMeasurementCollectionBySourceAndFragmentType()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne'
			IQueryAllBySourceAndType(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne'
			IQueryAllBySourceAndType(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo'
			IQueryAllBySourceAndType(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo'
			IQueryAllBySourceAndType(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//
		//    Scenario: Get measurement collection by fragment type and time

		[Fact]
		public void GetMeasurementCollectionByFragmentTypeAndTime()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from '2011-11-03T11:00:00
			// .000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllByTypeAndTime("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:01:14.9072393Z",
					"2018-12-19T10:10:14.9072393Z");
			//    Then I should GetFirstPage '2' measurements
			IShouldGetNumberOfMeasurements(2);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from '2011-11-03T11:00:00
			// .000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllByTypeAndTime("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:00:14.9072393Z",
					"2018-12-19T10:10:14.9072393Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from '2011-11-03T10:00:00
			// .000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllByTypeAndTime("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:14.9072393Z",
					"2018-12-19T10:00:14.9072393Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//
		//    Scenario: Get measurement collection by source and fragment type and time

		[Fact]
		public void GetMeasurementCollectionBySourceAndFragmentTypeAndTime()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I Create all measurements
			ICreateAll();
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:01:14.9072393Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:01:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '1' measurements
			IShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			IQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			IQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement
		//.......
		[Fact]
		public void GetMeasurement()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:00.0000000Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    When I Create all measurements
			ICreateAll();
			//    And I GetFirstPage the measurement with the created id
			IGetMeasurementWithCreatedId();
			//    Then I should GetFirstPage the measurement
			ShouldGetTheMeasurement();
		}

		//
		//    Scenario: Delete measurement

		[Fact]
		public void DeleteMeasurement()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			IHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:00.0000000Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    When I Create all measurements
			ICreateAll();
			//    And I Delete the measurement with the created id
			IDeleteMeasurementWithCreatedId();
			//    And I GetFirstPage the measurement with the created id
			IGetMeasurementWithCreatedId();
			//    Then Measurement should not be found
			ShouldNotBeFound();
		}

		//
		// Scenario: Delete all measurement collection by an empty filter
		[Fact]
		public void DeleteMeasurementCollectionByEmptyFilter()
		{
			//    Given I have '3' measurements of type 'com.type1' for the managed object
			IHaveMeasurements(3, "com.type1");
			//    And I have '2' measurements of type 'com.type2' for the managed object
			IHaveMeasurements(2, "com.type2");
			//    When I Create all measurements
			ICreateAll();
			//    Then All measurements should be created
			AllShouldBeCreated();
			//    When I query all measurements
			IQueryAll();
			//    Then I should GetFirstPage '5' measurements
			IShouldGetNumberOfMeasurements(5);
			//    When I Delete all measurement collection
			IDeleteMeasurementCollection();
			//    And I query all measurements
			IQueryAll();
			//    Then I should GetFirstPage '0' measurements
			IShouldGetNumberOfMeasurements(0);
		}


	//
	// Scenario: Delete measurements by filter
	//......
	[Fact]
	public void DeleteMeasurementsByTypeFilter() 
		{
		//    Given I have '3' measurements of type 'com.type1' for the managed object
		IHaveMeasurements(3, "com.type1");
        //    And I have '2' measurements of type 'com.type2' for the managed object
        IHaveMeasurements(2, "com.type2");
		//    When I Create all measurements
		ICreateAll();
		//    Then All measurements should be created
		AllShouldBeCreated();
		//    When I query all measurements
		IQueryAll();
		//    Then I should GetFirstPage '5' measurements
		IShouldGetNumberOfMeasurements(5);
		//    When I Delete all measurements by type 'com.type2'
		IDeleteMeasurementsByType("com.type2");
		//    And I query all measurements
		IQueryAll();
		//    Then I should GetFirstPage '3' measurements
		IShouldGetNumberOfMeasurements(3);
		//    When I query all measurements by type 'com.type1'
		IQueryAllByType("com.type1");
		//    Then I should GetFirstPage '3' measurements
		IShouldGetNumberOfMeasurements(3);
		//    When I query all measurements by type 'com.type2'
		IQueryAllByType("com.type2");
		//    Then I should GetFirstPage '0' measurements
		IShouldGetNumberOfMeasurements(0);
	}

	//    Scenario: Create measurement in bulk

	[Fact]
	public void CreateMeasurementsInBulk()
	{
		//    Given I have '2' measurements of type 'com.type1' for the managed object
		IHaveMeasurements(3, "com.type2");
		//    When I Create all measurements in bulk
		ICreateAllBulk();
		//    Then All measurements should be created
		AllShouldBeCreated();
	}

	//
	//    Scenario: Get measurements collection by default page settings

	[Fact]
	public void GetMeasurementCollectionByDefaultPageSettings() 
	{
        // Given
        for (int i = 0; i < 12; i++) {
			MeasurementRepresentation rep = aSampleMeasurement(managedObjects[0]);
			MeasurementApi.Create(rep);
		}

		// When
		MeasurementCollectionRepresentation measurements = MeasurementApi.Measurements.GetFirstPage();

		// Then
		Assert.Equal(5,measurements.Measurements.Count);

		// When
		MeasurementCollectionRepresentation page1st = MeasurementApi.Measurements.GetPage(measurements, 1);

		// Then
		Assert.Equal(5, page1st.Measurements.Count);

		// When
		MeasurementCollectionRepresentation page2nd = MeasurementApi.Measurements.GetPage(measurements, 2);

		// Then
		Assert.Equal(5, page2nd.Measurements.Count);
		}
	// ------------------------------------------------------------------------
	// Given
	// ------------------------------------------------------------------------


	//@Given("I have '(\\d+)' measurements of type '([^']*)' for the managed object")

	public void IHaveMeasurements(int n, String type)
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
		public void IHaveMeasurementsWithFragments(int n, String fragmentType)
		{
			for (int i = 0; i < n; i++)
			{
				MeasurementRepresentation rep = new MeasurementRepresentation();
				rep.DateTime = DateTime.UtcNow;
				rep.Type = "com.type1";
				rep.Source = managedObjects[0];

				// Set fragment
				Object fragment = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				rep.Set(fragment);

				Input.Add(rep);
			}
		}

		//@Given("I have a measurement of type '([^']*)' and no time value for the managed object")
		public void IHaveAMeasurementWithNoTime(String type)
		{
			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.Type = type;
			rep.Source = managedObjects[0];
			Input.Add(rep);
		}

		//@Given("I have '(\\d+)' measurements for the source '(\\d+)' the managed object")
		public void IHaveMeasurementsForSource(int n, int index)
		{
			for (int i = 0; i < n; i++)
			{
				MeasurementRepresentation rep = new MeasurementRepresentation();
				rep.DateTime = DateTime.UtcNow;
				rep.Type = "com.type1";
				rep.Source = managedObjects[index];
				Input.Add(rep);
			}
		}

		// ------------------------------------------------------------------------
		// When
		// ------------------------------------------------------------------------

		//@When("I Create all measurements")
		public void ICreateAll()
		{
			try
			{
				foreach (MeasurementRepresentation rep in Input)
				{
					Result1.Add(MeasurementApi.Create(rep));
				}
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I Create all measurements as bulk")
		public void ICreateAllBulk()
		{
			try
			{
				MeasurementCollectionRepresentation collection = new MeasurementCollectionRepresentation();
				collection.Measurements = Input;
				Result1.AddRange(MeasurementApi.CreateBulk(collection).Measurements);
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by fragment type '([^']*)'")
		public void IQueryAllByFragmentType(String fragmentType)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				MeasurementFilter filter = new MeasurementFilter().ByFragmentType(fragmentClass.GetType());
				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by source '(\\d+)'")
		public void IQueryAllBySource(int index)
		{
			try
			{
				ManagedObjectRepresentation source = managedObjects[index];
				MeasurementFilter filter = new MeasurementFilter().BySource(source);
				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@Given("I have a measurement with time '([^']*)' with fragment type '([^']*)' and for '(\\d+)' managed object")

		public void IHaveAMeasurementWithTypeAndTime(String time, String fragmentType, int index)
		{
			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.DateTime = DateTime.ParseExact(time, "o",
				System.Globalization.CultureInfo.InvariantCulture);
			rep.Type = "com.type1";
			rep.Source = managedObjects[index];

			// Set fragment
			var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
			rep.Set(fragmentClass);
			Input.Add(rep);
		}

		//@When("I query all measurements by source '(\\d+)' and time from '([^']*)' and time to '([^']*)'")
		public void IQueryAllBySourceAndTime(int index, String from, String to)
		{
			try
			{
				ManagedObjectRepresentation source = managedObjects[index];
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().ByDate(fromDate, toDate).BySource(source);
				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by time from '([^']*)' and time to '([^']*)'")
		public void IQueryAllByTime(String from, String to)
		{
			try
			{
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().ByDate(fromDate, toDate);
				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by source '(\\d+)' and fragment type '([^']*)'")
		public void IQueryAllBySourceAndType(int index, String fragmentType)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);

				ManagedObjectRepresentation source = managedObjects[index];
				MeasurementFilter filter = new MeasurementFilter().ByFragmentType(fragmentClass.GetType()).BySource(source);

				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by fragment type '([^']*)' and time from '([^']*)' and time to '([^']*)'")

		public void IQueryAllByTypeAndTime(String fragmentType, String from, String to)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().ByDate(fromDate, toDate).ByFragmentType(fragmentClass.GetType());
				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by source '(\\d+)' and fragment type '([^']*)' and time from '([^']*)' and time to '([^']*)'")
		public void IQueryAllBySourceTypeAndTime(int index, String fragmentType, String from, String to)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				ManagedObjectRepresentation source = managedObjects[index];
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().BySource(source).ByDate(fromDate, toDate).ByFragmentType(fragmentClass.GetType());
				Collection1 = MeasurementApi.GetMeasurementsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I GetFirstPage the measurement with the created id")

		public void IGetMeasurementWithCreatedId()
		{
			try
			{
				Result2.Add(MeasurementApi.GetMeasurement(Result1[0].Id));
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I Delete the measurement with the created id")

		public void IDeleteMeasurementWithCreatedId()
		{
			try
			{
				MeasurementApi.DeleteMeasurement(Result1[0]);
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I Delete all measurement collection")

		public void IDeleteMeasurementCollection()
		{
			try
			{
				MeasurementApi.DeleteMeasurementsByFilter(new MeasurementFilter());
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I Delete all measurements by type '([^']*)'")

		public void IDeleteMeasurementsByType(String type)
		{
			try
			{
				MeasurementFilter typeFilter = new MeasurementFilter().ByType(type);
				MeasurementApi.DeleteMeasurementsByFilter(typeFilter);
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements")

		public void IQueryAll()
		{
			try
			{
				Collection1 = MeasurementApi.Measurements.GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by type '([^']*)'")
		public void IQueryAllByType(String type) 
		{
			try {
			MeasurementFilter typeFilter = new MeasurementFilter().ByType(type);
			Collection1 = MeasurementApi.GetMeasurementsByFilter(typeFilter).GetFirstPage();
		} catch (SDKException ex) {
			Status = ex.HttpStatus;
		}
}

// ------------------------------------------------------------------------
// Then
// ------------------------------------------------------------------------

//@Then("All measurements should be created")
public void AllShouldBeCreated()
		{
			Assert.Equal(Input.Count, Result1.Count);
			foreach (MeasurementRepresentation rep in Result1)
			{
				Assert.NotNull(rep.Id);
			}
		}

		//@Then("Measurement response should be unprocessable")
		public void ShouldBeBadRequest()
		{
			Assert.Equal(422, Status);
		}

		//@Then("I should GetFirstPage '(\\d+)' measurements")
		public void IShouldGetNumberOfMeasurements(int count)
		{
			Assert.Equal(count, Collection1.Measurements.Count());
		}

		//@Then("Measurement should not be found")
		public void ShouldNotBeFound()
		{
			Assert.Equal(404,Status);
		}

		//@Then("I should GetFirstPage the measurement")

		public void ShouldGetTheMeasurement()
		{
			Assert.Equal( Result2[0].Id, Result1[0].Id);
		}
	}
}