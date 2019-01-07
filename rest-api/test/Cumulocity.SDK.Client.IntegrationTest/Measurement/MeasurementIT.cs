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

		private MeasurementRepresentation aSampleMeasurement(ManagedObjectRepresentation source)
		{
			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.Type = "com.type1";
			rep.DateTime = DateTime.UtcNow;
			rep.Source = source;
			Input.Add(rep);

			rep.set(new FragmentOne());
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

		//
		//    Scenario: Get measurement collection by fragment type

		[Fact]
		public void getMeasurementCollectionByFragmentType()
		{
			//    Given I have '2' measurements with fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' for the managed object
			iHaveMeasurementsWithFragments(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    And I have '3' measurements with fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' for the managed object
			iHaveMeasurementsWithFragments(3, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne'
			iQueryAllByFragmentType("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    Then I should get '2' measurements
			iShouldGetNumberOfMeasurements(2);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo'
			iQueryAllByFragmentType("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    Then I should get '3' measurements
			iShouldGetNumberOfMeasurements(3);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentThree'
			iQueryAllByFragmentType("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentThree");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//
		//    Scenario: Get measurement collection by source

		[Fact]
		public void getMeasurementCollectionBySource()
		{
			//    Given I have '1' measurements for the source '0' the managed object
			iHaveMeasurementsForSource(1, 0);
			//    And I have '2' measurements for the source '1' the managed object
			iHaveMeasurementsForSource(2, 1);
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by source '0'
			iQueryAllBySource(0);
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1'
			iQueryAllBySource(1);
			//    Then I should get '2' measurements
			iShouldGetNumberOfMeasurements(2);
			//    And I query all measurements by source '2'
			iQueryAllBySource(2);
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement collection by time

		[Fact]
		public void getMeasurementCollectionByTime()
		{
			var a = getMeasurementsFrom1stPage();
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-18T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-18T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by time from '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllByTime("2018-12-18T10:00:14.9072393Z", "2018-12-18T10:10:14.9072393Z");
			//    Then I should get '2' measurements
			iShouldGetNumberOfMeasurements(2);

			//    And I query all measurements by time from '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllByTime("2018-12-18T09:00:14.9072393Z", "2018-12-18T09:10:14.9072393Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement collection by source and time

		[Fact]
		public void getMeasurementCollectionBySourceAndTime()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by source '0' and time from '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceAndTime(0, "2018-12-19T10:00:14.9072393Z", "2018-12-19T10:10:14.9072393Z");
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1' and time from '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceAndTime(1, "2018-12-19T10:00:14.9072393Z", "2018-12-19T10:10:14.9072393Z");
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '0' and time from '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceAndTime(0, "2018-12-19T09:00:14.9072393Z", "2018-12-19T09:10:14.9072393Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and time from '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceAndTime(1, "2018-12-19T09:00:14.9072393Z", "2018-12-19T09:10:14.9072393Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement collection by source and fragment type
		[Fact]
		public void getMeasurementCollectionBySourceAndFragmentType()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne'
			iQueryAllBySourceAndType(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne'
			iQueryAllBySourceAndType(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne");
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo'
			iQueryAllBySourceAndType(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo'
			iQueryAllBySourceAndType(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//
		//    Scenario: Get measurement collection by fragment type and time

		[Fact]
		public void getMeasurementCollectionByFragmentTypeAndTime()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from '2011-11-03T11:00:00
			// .000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllByTypeAndTime("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:01:14.9072393Z",
					"2018-12-19T10:10:14.9072393Z");
			//    Then I should get '2' measurements
			iShouldGetNumberOfMeasurements(2);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from '2011-11-03T11:00:00
			// .000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllByTypeAndTime("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:00:14.9072393Z",
					"2018-12-19T10:10:14.9072393Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from '2011-11-03T10:00:00
			// .000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllByTypeAndTime("Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:14.9072393Z",
					"2018-12-19T10:00:14.9072393Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//
		//    Scenario: Get measurement collection by source and fragment type and time

		[Fact]
		public void getMeasurementCollectionBySourceAndFragmentTypeAndTime()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    And I have a measurement with time '2011-11-03T11:05:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '1' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:05:14.9072393Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 1);
			//    When I create all measurements
			iCreateAll();
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:01:14.9072393Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:01:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '0' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceTypeAndTime(0, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '1' measurements
			iShouldGetNumberOfMeasurements(1);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '1' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceTypeAndTime(1, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T11:00:00.000+05:30' and time to '2011-11-03T11:10:00.000+05:30'
			iQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T10:00:00.0000000Z",
					"2018-12-19T10:10:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentOne' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
			//    And I query all measurements by source '2' and fragment type 'com.cumulocity.sdk.client.measurement.FragmentTwo' and time from
			// '2011-11-03T10:00:00.000+05:30' and time to '2011-11-03T11:00:00.000+05:30'
			iQueryAllBySourceTypeAndTime(2, "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentTwo", "2018-12-19T09:00:00.0000000Z",
					"2018-12-19T10:00:00.0000000Z");
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Get measurement
		//.......
		[Fact]
		public void getMeasurement()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:00.0000000Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    When I create all measurements
			iCreateAll();
			//    And I get the measurement with the created id
			iGetMeasurementWithCreatedId();
			//    Then I should get the measurement
			shouldGetTheMeasurement();
		}

		//
		//    Scenario: Delete measurement

		[Fact]
		public void deleteMeasurement()
		{
			//    Given I have a measurement with time '2011-11-03T11:01:00.000+05:30' with fragment type 'com.cumulocity.sdk.client.measurement
			// .FragmentOne' and for '0' managed object
			iHaveAMeasurementWithTypeAndTime("2018-12-19T10:01:00.0000000Z", "Cumulocity.SDK.Client.IntegrationTest.Measurement.FragmentOne", 0);
			//    When I create all measurements
			iCreateAll();
			//    And I delete the measurement with the created id
			iDeleteMeasurementWithCreatedId();
			//    And I get the measurement with the created id
			iGetMeasurementWithCreatedId();
			//    Then Measurement should not be found
			shouldNotBeFound();
		}

		//
		// Scenario: Delete all measurement collection by an empty filter
		[Fact]
		public void deleteMeasurementCollectionByEmptyFilter()
		{
			//    Given I have '3' measurements of type 'com.type1' for the managed object
			iHaveMeasurements(3, "com.type1");
			//    And I have '2' measurements of type 'com.type2' for the managed object
			iHaveMeasurements(2, "com.type2");
			//    When I create all measurements
			iCreateAll();
			//    Then All measurements should be created
			allShouldBeCreated();
			//    When I query all measurements
			iQueryAll();
			//    Then I should get '5' measurements
			iShouldGetNumberOfMeasurements(5);
			//    When I delete all measurement collection
			iDeleteMeasurementCollection();
			//    And I query all measurements
			iQueryAll();
			//    Then I should get '0' measurements
			iShouldGetNumberOfMeasurements(0);
		}


	//
	// Scenario: Delete measurements by filter
	//......
	[Fact]
	public void deleteMeasurementsByTypeFilter() 
		{
		//    Given I have '3' measurements of type 'com.type1' for the managed object
		iHaveMeasurements(3, "com.type1");
        //    And I have '2' measurements of type 'com.type2' for the managed object
        iHaveMeasurements(2, "com.type2");
		//    When I create all measurements
		iCreateAll();
		//    Then All measurements should be created
		allShouldBeCreated();
		//    When I query all measurements
		iQueryAll();
		//    Then I should get '5' measurements
		iShouldGetNumberOfMeasurements(5);
		//    When I delete all measurements by type 'com.type2'
		iDeleteMeasurementsByType("com.type2");
		//    And I query all measurements
		iQueryAll();
		//    Then I should get '3' measurements
		iShouldGetNumberOfMeasurements(3);
		//    When I query all measurements by type 'com.type1'
		iQueryAllByType("com.type1");
		//    Then I should get '3' measurements
		iShouldGetNumberOfMeasurements(3);
		//    When I query all measurements by type 'com.type2'
		iQueryAllByType("com.type2");
		//    Then I should get '0' measurements
		iShouldGetNumberOfMeasurements(0);
	}

	//    Scenario: Create measurement in bulk

	[Fact]
	public void createMeasurementsInBulk()
	{
		//    Given I have '2' measurements of type 'com.type1' for the managed object
		iHaveMeasurements(3, "com.type2");
		//    When I create all measurements in bulk
		iCreateAllBulk();
		//    Then All measurements should be created
		allShouldBeCreated();
	}

	//
	//    Scenario: Get measurements collection by default page settings

	[Fact]
	public void getMeasurementCollectionByDefaultPageSettings() 
	{
        // Given
        for (int i = 0; i < 12; i++) {
			MeasurementRepresentation rep = aSampleMeasurement(managedObjects[0]);
			MeasurementApi.create(rep);
		}

		// When
		MeasurementCollectionRepresentation measurements = MeasurementApi.Measurements.get();

		// Then
		Assert.Equal(5,measurements.Measurements.Count);

		// When
		MeasurementCollectionRepresentation page1st = MeasurementApi.Measurements.getPage(measurements, 1);

		// Then
		Assert.Equal(5, page1st.Measurements.Count);

		// When
		MeasurementCollectionRepresentation page2nd = MeasurementApi.Measurements.getPage(measurements, 2);

		// Then
		Assert.Equal(5, page2nd.Measurements.Count);
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

		//@Given("I have '(\\d+)' measurements for the source '(\\d+)' the managed object")
		public void iHaveMeasurementsForSource(int n, int index)
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

		//@When("I query all measurements by fragment type '([^']*)'")
		public void iQueryAllByFragmentType(String fragmentType)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				MeasurementFilter filter = new MeasurementFilter().byFragmentType(fragmentClass.GetType());
				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by source '(\\d+)'")
		public void iQueryAllBySource(int index)
		{
			try
			{
				ManagedObjectRepresentation source = managedObjects[index];
				MeasurementFilter filter = new MeasurementFilter().bySource(source);
				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@Given("I have a measurement with time '([^']*)' with fragment type '([^']*)' and for '(\\d+)' managed object")

		public void iHaveAMeasurementWithTypeAndTime(String time, String fragmentType, int index)
		{
			MeasurementRepresentation rep = new MeasurementRepresentation();
			rep.DateTime = DateTime.ParseExact(time, "o",
				System.Globalization.CultureInfo.InvariantCulture);
			rep.Type = "com.type1";
			rep.Source = managedObjects[index];

			// Set fragment
			var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
			rep.set(fragmentClass);
			Input.Add(rep);
		}

		//@When("I query all measurements by source '(\\d+)' and time from '([^']*)' and time to '([^']*)'")
		public void iQueryAllBySourceAndTime(int index, String from, String to)
		{
			try
			{
				ManagedObjectRepresentation source = managedObjects[index];
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().byDate(fromDate, toDate).bySource(source);
				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by time from '([^']*)' and time to '([^']*)'")
		public void iQueryAllByTime(String from, String to)
		{
			try
			{
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().byDate(fromDate, toDate);
				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by source '(\\d+)' and fragment type '([^']*)'")
		public void iQueryAllBySourceAndType(int index, String fragmentType)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);

				ManagedObjectRepresentation source = managedObjects[index];
				MeasurementFilter filter = new MeasurementFilter().byFragmentType(fragmentClass.GetType()).bySource(source);

				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by fragment type '([^']*)' and time from '([^']*)' and time to '([^']*)'")

		public void iQueryAllByTypeAndTime(String fragmentType, String from, String to)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().byDate(fromDate, toDate).byFragmentType(fragmentClass.GetType());
				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by source '(\\d+)' and fragment type '([^']*)' and time from '([^']*)' and time to '([^']*)'")
		public void iQueryAllBySourceTypeAndTime(int index, String fragmentType, String from, String to)
		{
			try
			{
				var fragmentClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fragmentType);
				ManagedObjectRepresentation source = managedObjects[index];
				var fromDate = DateTime.ParseExact(from, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				var toDate = DateTime.ParseExact(to, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				MeasurementFilter filter = new MeasurementFilter().bySource(source).byDate(fromDate, toDate).byFragmentType(fragmentClass.GetType());
				Collection1 = MeasurementApi.getMeasurementsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I get the measurement with the created id")

		public void iGetMeasurementWithCreatedId()
		{
			try
			{
				Result2.Add(MeasurementApi.getMeasurement(Result1[0].Id));
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I delete the measurement with the created id")

		public void iDeleteMeasurementWithCreatedId()
		{
			try
			{
				MeasurementApi.deleteMeasurement(Result1[0]);
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I delete all measurement collection")

		public void iDeleteMeasurementCollection()
		{
			try
			{
				MeasurementApi.deleteMeasurementsByFilter(new MeasurementFilter());
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I delete all measurements by type '([^']*)'")

		public void iDeleteMeasurementsByType(String type)
		{
			try
			{
				MeasurementFilter typeFilter = new MeasurementFilter().byType(type);
				MeasurementApi.deleteMeasurementsByFilter(typeFilter);
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements")

		public void iQueryAll()
		{
			try
			{
				Collection1 = MeasurementApi.Measurements.get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all measurements by type '([^']*)'")
		public void iQueryAllByType(String type) 
		{
			try {
			MeasurementFilter typeFilter = new MeasurementFilter().byType(type);
			Collection1 = MeasurementApi.getMeasurementsByFilter(typeFilter).get();
		} catch (SDKException ex) {
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

		//@Then("I should get '(\\d+)' measurements")
		public void iShouldGetNumberOfMeasurements(int count)
		{
			Assert.Equal(count, Collection1.Measurements.Count());
		}

		//@Then("Measurement should not be found")

		public void shouldNotBeFound()
		{
			Assert.Equal(404,Status);
		}

		//@Then("I should get the measurement")

		public void shouldGetTheMeasurement()
		{
			Assert.Equal( Result2[0].Id, Result1[0].Id);
		}
	}
}