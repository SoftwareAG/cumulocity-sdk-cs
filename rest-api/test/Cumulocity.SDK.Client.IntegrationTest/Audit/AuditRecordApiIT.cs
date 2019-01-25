using Cumulocity.SDK.Client.IntegrationTest.Common;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Audit;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Audit;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Cumulocity.SDK.Client.IntegrationTest.Audit
{
#pragma warning disable xUnit1013
	public class AuditRecordApiIT : IClassFixture<AuditRecordApiFixture>, IDisposable
	{
		private readonly AuditRecordApiFixture _fixture;
		private readonly ITestOutputHelper _output;
		private static List<ManagedObjectRepresentation> managedObjects = new List<ManagedObjectRepresentation>();
		private readonly IAuditRecordApi auditRecordsApi;
		private int status;
		private readonly List<AuditRecordRepresentation> input;
		private readonly List<AuditRecordRepresentation> result1;
		private readonly List<AuditRecordRepresentation> result2;
		private readonly TenantCreator tenantCreator;
		private AuditRecordCollectionRepresentation collection1;
		protected  PlatformImpl platform;



		public AuditRecordApiIT(AuditRecordApiFixture fixture, ITestOutputHelper output)
		{
			_fixture = fixture;
			this.platform = createPlatform();
			this.tenantCreator = new TenantCreator(platform);
			tenantCreator.createTenantAsync().Wait(11000);

			_output = output;

			this.status = 200;
			this.input = new List<AuditRecordRepresentation>();
			this.result1 = new List<AuditRecordRepresentation>();
			this.result2 = new List<AuditRecordRepresentation>();
			this.auditRecordsApi = platform.AuditRecordApi;
			//    Given I have '3' managed objects
			//    And I Create all
			for (int i = 0; i < 3; ++i)
			{
				ManagedObjectRepresentation mo = platform.InventoryApi.Create(aSampleMo().WithName("MO" + i).build());
				managedObjects.Add(mo);
			}
		}

		//    Scenario: Create and GetFirstPage audit records
		[Fact]
		public void createAndGetAuditRecords()
		{
			//    Given I have '3' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
			IHaveAuditRecord(3, "com.type1", "app1", "user1");
			//    When I Create all audit records
			ICreateAll();
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I GetFirstPage all audit records
			IGetAllAuditRecords();
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I should I GetFirstPage all the audit records
			ShouldGetAllMeasurements();
		}

		//
		//
		//    Scenario: Create and GetFirstPage audit record

		[Fact]
		public void createAndGetAuditRecord()
		{
			//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
			IHaveAuditRecord(1, "com.type1", "app1", "user1");
			//    When I Create all audit records
			ICreateAll();
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I GetFirstPage the audit record with the created id
			IGetWithCreatedId();
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I should GetFirstPage the audit record
			ShouldGetTheMeasurement();
		}

		//    Scenario: Query by user

		[Fact]
		public void queryByUser()
		{
			//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
			IHaveAuditRecord(1, "com.type1", "app1", "user1");
			//    Given I have '3' audit records of type 'com.type1' and application 'app1' and user 'user2' for the managed object
			IHaveAuditRecord(3, "com.type1", "app1", "user2");
			//    When I Create all audit records
			ICreateAll();
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I query the audit record collection by user 'user2'
			IQueryByUser("user2");
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I should GetFirstPage '3' audit records
			IShouldGetNumberOfMeasurements(3);
			//    And I query the audit record collection by user 'user1'
			IQueryByUser("user1");
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I should GetFirstPage '1' audit records
			IShouldGetNumberOfMeasurements(1);
			//    And I query the audit record collection by user 'user3'
			IQueryByUser("user3");
			//    Then Audit record response status should be success
			ShouldBeSuccess();
			//    And I should GetFirstPage '0' audit records
			IShouldGetNumberOfMeasurements(0);
		}

		//
		//    Scenario: Query by type

		[Fact]
		public void queryByType() 
		{
			//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
			IHaveAuditRecord(1, "com.type1", "app1", "user1");
//    Given I have '3' audit records of type 'com.type2' and application 'app1' and user 'user1' for the managed object
        IHaveAuditRecord(3, "com.type2", "app1", "user1");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I query the audit record collection by type 'com.type2'
		IQueryByType("com.type2");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '3' audit records
		IShouldGetNumberOfMeasurements(3);
		//    And I query the audit record collection by type 'com.type1'
		IQueryByType("com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '1' audit records
		IShouldGetNumberOfMeasurements(1);
		//    And I query the audit record collection by type 'com.type3'
		IQueryByType("com.type3");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '0' audit records
		IShouldGetNumberOfMeasurements(0);
	}

	//
	//
	//    Scenario: Query by application

	[Fact]
	public void queryByApplication() 
	{
		//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
		IHaveAuditRecord(1, "com.type1", "app1", "user1");
		//    Given I have '3' audit records of type 'com.type1' and application 'app2' and user 'user1' for the managed object
		IHaveAuditRecord(3, "com.type1", "app2", "user1");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I query the audit record collection by application 'app2'
		IQueryByApp("app2");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '3' audit records
		IShouldGetNumberOfMeasurements(3);
		//    And I query the audit record collection by application 'app1'
		IQueryByApp("app1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '1' audit records
		IShouldGetNumberOfMeasurements(1);
		//    And I query the audit record collection by application 'app3'
		IQueryByApp("app3");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '0' audit records
		IShouldGetNumberOfMeasurements(0);
	}

	//
	//    Scenario: Query by user and type

	[Fact]
	public void queryByUserAndType() 
	{
		//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
		IHaveAuditRecord(1, "com.type1", "app1", "user1");
		//    Given I have '3' audit records of type 'com.type1' and application 'app1' and user 'user2' for the managed object
		IHaveAuditRecord(3, "com.type1", "app1", "user2");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I query the audit record collection by user 'user2' and type 'com.type1'
		IQueryByUserAndType("user2", "com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '3' audit records
		IShouldGetNumberOfMeasurements(3);
		//    And I query the audit record collection by user 'user1' and type 'com.type1'
		IQueryByUserAndType("user1", "com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '1' audit records
		IShouldGetNumberOfMeasurements(1);
		//    And I query the audit record collection by user 'user3' and type 'com.type1'
		IQueryByUserAndType("user3", "com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '0' audit records
		IShouldGetNumberOfMeasurements(0);
	}

	//
	//    Scenario: Query by user and application

	[Fact]
	public void queryByUserAndApplication() 
	{
		//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
		IHaveAuditRecord(1, "com.type1", "app1", "user1");
		//    Given I have '3' audit records of type 'com.type1' and application 'app1' and user 'user2' for the managed object
		IHaveAuditRecord(3, "com.type1", "app1", "user2");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I query the audit record collection by user 'user2' and application 'app1'
		IQueryByUserAndApp("user2", "app1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '3' audit records
		IShouldGetNumberOfMeasurements(3);
		//    And I query the audit record collection by user 'user1' and application 'app1'
		IQueryByUserAndApp("user1", "app1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '1' audit records
		IShouldGetNumberOfMeasurements(1);
		//    And I query the audit record collection by user 'user3' and application 'app1'
		IQueryByUserAndApp("user3", "app1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '0' audit records
		IShouldGetNumberOfMeasurements(0);
	}

	//
	//
	//    Scenario: Query by user, application and type

	[Fact]
	public void queryByUserApplicationAndType() 
	{
		//    Given I have '1' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
		IHaveAuditRecord(1, "com.type1", "app1", "user1");
		//    Given I have '3' audit records of type 'com.type1' and application 'app1' and user 'user2' for the managed object
		IHaveAuditRecord(3, "com.type1", "app1", "user2");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I query the audit record collection by user 'user2' and application 'app1' and type 'com.type1'
		IQueryByUserAndAppAndType("user2", "app1", "com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '3' audit records
		IShouldGetNumberOfMeasurements(3);
		//    And I query the audit record collection by user 'user1' and application 'app1' and type 'com.type1'
		IQueryByUserAndAppAndType("user1", "app1", "com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '1' audit records
		IShouldGetNumberOfMeasurements(1);
		//    And I query the audit record collection by user 'user3' and application 'app1' and type 'com.type1'
		IQueryByUserAndAppAndType("user3", "app1", "com.type1");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I should GetFirstPage '0' audit records
		IShouldGetNumberOfMeasurements(0);
	}

	//
	//    Scenario: Query to test the paging with user

	[Fact]
	public void queryToTestThePagingWithUser() 
	{
		//    Given I have '10' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
		IHaveAuditRecord(10, "com.type1", "app1", "user1");
		//    Given I have '10' audit records of type 'com.type1' and application 'app1' and user 'user2' for the managed object
		IHaveAuditRecord(10, "com.type1", "app1", "user2");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I query the audit record collection by user 'user2'
		IQueryByUser("user2");
		//    Then Audit record response status should be success
		ShouldBeSuccess();
	}

	//
	//    Scenario: Query to test the paging to GetFirstPage all AuditRecords

	[Fact]
	public void queryToTestThePagingToGetAllAuditRecords()
	{
		//    Given I have '10' audit records of type 'com.type1' and application 'app1' and user 'user1' for the managed object
		IHaveAuditRecord(10, "com.type1", "app1", "user1");
		//    Given I have '10' audit records of type 'com.type1' and application 'app1' and user 'user2' for the managed object
		IHaveAuditRecord(10, "com.type1", "app1", "user2");
		//    When I Create all audit records
		ICreateAll();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
		//    And I GetFirstPage all audit records
		IGetAllAuditRecords();
		//    Then Audit record response status should be success
		ShouldBeSuccess();
	}

	// ------------------------------------------------------------------------
	// Given
	// ------------------------------------------------------------------------

	//@Given("I have '(\\d+)' audit records of type '([^']*)' and application '([^']*)' and user '([^']*)' for the managed object$")
	public void IHaveAuditRecord(int n, String type, String application, String user)
		{
			for (int i = 0; i < n; i++)
			{
				AuditRecordRepresentation rep = new AuditRecordRepresentation();
				rep.Type = type;
				rep.DateTime = DateTime.UtcNow;
				rep.Source = managedObjects[0];
				rep.Activity = "Some Activity";
				rep.Application = application;
				rep.User = user;
				rep.Severity = "major";
				rep.Text = "text";
				input.Add(rep);
			}
		}

		// ------------------------------------------------------------------------
		// When
		// ------------------------------------------------------------------------
		//@When("I Create all audit records$")

		public void ICreateAll()
		{
			try
			{
				foreach (var rep in input)
				{
					result1.Add(auditRecordsApi.Create(rep));
				}
			}
			catch (SDKException ex)
			{
				Debug.WriteLine(ex.ToString());
				status = ex.HttpStatus;
			}
		}

		//@When("I GetFirstPage all audit records$")
		public void IGetAllAuditRecords()
		{
			try
			{
				collection1 = auditRecordsApi.AuditRecords.GetFirstPage();
			}
			catch (SDKException ex)
			{
				Debug.WriteLine(ex.Message);
				_output.WriteLine("SDKException: {0}", ex.Message);
				status = ex.HttpStatus;
			}
		}

		//@When("I GetFirstPage the audit record with the created id$")

		public void IGetWithCreatedId()
		{
			try
			{
				AuditRecordRepresentation auditRecordRepresentation = auditRecordsApi.GetAuditRecord(result1[0].Id);
				Assert.NotNull(auditRecordRepresentation);
				result2.Add(auditRecordRepresentation);
			}
			catch (SDKException ex)
			{
				Debug.WriteLine(ex.Message);
				_output.WriteLine("SDKException: {0}", ex.Message);
				status = ex.HttpStatus;
			}
		}

		//@When("I query the audit record collection by user '([^']*)'$")
		public void IQueryByUser(String user)
		{
			try
			{
				AuditRecordFilter filter = new AuditRecordFilter().ByUser(user);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Debug.WriteLine(ex.Message);
				_output.WriteLine("SDKException: {0}", ex.Message);
				status = ex.HttpStatus;
			}
		}
		//@When("I query the audit record collection by type '([^']*)'$")
		public virtual void IQueryByType(string type)
		{
			try
			{
				AuditRecordFilter filter = (new AuditRecordFilter()).ByType(type);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
				status = ex.HttpStatus;
			}
		}
		//@When("I query the audit record collection by application '([^']*)'$")
		public virtual void IQueryByApp(string app)
		{
			try
			{
				AuditRecordFilter filter = (new AuditRecordFilter()).ByApplication(app);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
				status = ex.HttpStatus;
			}
		}

		//@When("I query the audit record collection by user '([^']*)' and type '([^']*)'$")
		public virtual void IQueryByUserAndType(string user, string type)
		{
			try
			{
				AuditRecordFilter filter = (new AuditRecordFilter()).ByUser(user).ByType(type);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
				status = ex.HttpStatus;
			}
		}
		//@When("I query the audit record collection by user '([^']*)' and application '([^']*)'$")
		public virtual void IQueryByUserAndApp(string user, string application)
		{
			try
			{
				AuditRecordFilter filter = (new AuditRecordFilter()).ByUser(user).ByApplication(application);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
				status = ex.HttpStatus;
			}
		}
		//@When("I query the audit record collection by user '([^']*)' and application '([^']*)' and type '([^']*)'$")
		public virtual void IQueryByUserAndAppAndType(string user, string application, string type)
		{
			try
			{
				AuditRecordFilter filter = (new AuditRecordFilter()).ByUser(user).ByType(type).ByApplication(application);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
				status = ex.HttpStatus;
			}
		}
		//   @When("I query the audit record collection by type '([^']*)' and application '([^']*)'$")
		public virtual void IQueryByTypeAndApp(string type, string application)
		{
			try
			{
				AuditRecordFilter filter = (new AuditRecordFilter()).ByType(type).ByApplication(application);
				collection1 = auditRecordsApi.GetAuditRecordsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
				status = ex.HttpStatus;
			}
		}




		// ------------------------------------------------------------------------
		// Then
		// ------------------------------------------------------------------------
		//@Then("Audit record response status should be success$")

		public void ShouldBeSuccess()
		{
			Assert.True(status < 300);
		}

		//@Then("I should GetFirstPage the audit record$")

		public void ShouldGetTheMeasurement()
		{
			Assert.Equal(result2[0].Id, result1[0].Id);
		}

		//@Then("I should GetFirstPage '(\\d+)' audit records$")

		public void IShouldGetNumberOfMeasurements(int count)
		{
			Assert.Equal(count, collection1.AuditRecords.Count);
		}

		//ORIGINAL LINE: @Then("I should I GetFirstPage all the audit records$") public void shouldGetAllMeasurements()
		public virtual void ShouldGetAllMeasurements()
		{
			Assert.Equal(result1.Count, collection1.AuditRecords.Count);

			IDictionary<GId, AuditRecordRepresentation> map = new Dictionary<GId, AuditRecordRepresentation>();

			foreach (AuditRecordRepresentation rep in result1)
			{
				map[rep.Id] = rep;
			}

			foreach (AuditRecordRepresentation rep in collection1.AuditRecords)
			{
				AuditRecordRepresentation orig = map[rep.Id];
				Assert.NotNull(orig);
			}
		}

		private static ManagedObjectRepresentationBuilder aSampleMo()
		{
			return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
				.MO_REPRESENTATION);
		}

		private  PlatformImpl createPlatform()
		{
			var p = readSystemPropertie();

			return new PlatformImpl(
				p["cumulocity.host"],
				new CumulocityCredentials(p["cumulocity.tenant"],
					p["cumulocity.user"],
					p["cumulocity.password"],
					null),
				5);
		}

		private  Dictionary<string, string> readSystemPropertie()
		{
			var result = new Dictionary<string, string>();

			var location = typeof(AuditRecordApiIT).GetTypeInfo().Assembly.Location;
			var dirPath = Path.GetDirectoryName(location);
			var appConfigPath = Path.Combine(dirPath, "Resources/cumulocity-test.properties");
			var readText = File.ReadAllLines(appConfigPath, Encoding.UTF8);
			foreach (var item in readText)
			{
				var arr = item.Split('=');
				if (arr != null && arr.Length == 2)
				{
					if (arr[0].StartsWith("#") || result.ContainsKey(arr[0]))
						continue;
					result.Add(arr[0], arr[1]);
				}
			}
			return result;
		}

		public void Dispose()
		{
			tenantCreator.removeTenant();
		}
	}
}