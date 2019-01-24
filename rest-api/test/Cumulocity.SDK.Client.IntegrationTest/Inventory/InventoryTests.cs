using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Inventory
{
    public class InventoryTests : IClassFixture<InventoryFixture>,IDisposable
    {
        public InventoryTests(InventoryFixture fixture)
        {
            this.fixture = fixture;
            inventory = fixture.platform.InventoryApi;
            fixture.platform.RequireResponseBody = true;
        }

        private readonly InventoryFixture fixture;
        private readonly IInventoryApi inventory;

        private static ManagedObjectRepresentationBuilder aSampleMo()
        {
            return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
                .MO_REPRESENTATION);
        }

        [Fact]
        public void createAndDeleteManagedObject()
        {
            // Given
            var rep = aSampleMo().build();
            var result = inventory.Create(rep);

            // When
            var mo = inventory.GetManagedObject(result.Id);
            mo.Delete();

            // Then
            var deletedMo = inventory.GetManagedObject(result.Id);
            var ex = Record.Exception(() => deletedMo.Get());
            Assert.NotNull(ex);
            Assert.IsType<SDKException>(ex);
            Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);
        }

        [Fact]
        public void createAndGetManagedObject()
        {
            // Given
            var rep = aSampleMo().build();
            var created = inventory.Create(rep);

            // When
            var result = inventory.GetManagedObject(created.Id).Get();

            // Then
            Assert.Equal(result.Id, created.Id);
            Assert.Equal(result.Name, created.Name);
            Assert.Equal(result.Type, created.Type);
        }

        [Fact]
        public void createAndUpdateManagedObject()
        {
            // Given
            var rep = aSampleMo().build();
            var result = inventory.Create(rep);

            // When
            var coordinate = new Position {Lat = 100.0m, Lng = 10.0m};
            result.set(coordinate);

            var id = result.Id;
            result.Id = null;
            result.LastUpdated = null;

            var updated = inventory.GetManagedObject(id).Update(result);
            Assert.NotNull(updated);
            Assert.Equal(coordinate, updated.get<Position>());
        }

        [Fact]
        public void createAndUpdateManagedObjectByRemovingFragment()
        {
            // Given
            var rep = aSampleMo().with(new Position()).build();
            var created = inventory.Create(rep);

            //When
            created.set(new object(), "c8y_Position");
            var id = created.Id;
            created.Id = null;
            created.LastUpdated = null;
            var updated = inventory.GetManagedObject(id).Update(created);

            // Then
            Assert.NotNull(updated.Id);
            Assert.NotNull(updated.get<Position>());
        }

        [Fact]
        public void CreateManagedObject()
        {
            // Given
            var rep = aSampleMo().build();

            // When
            var created = inventory.Create(rep);

            // Then
            Assert.NotNull(created.Id);
            Assert.NotNull(created.Self);
            Assert.NotSame(created, rep);
        }

        [Fact]
        public void createManagedObjectWith2ThreePhaseElectricityFragments()
        {
            // Given
            var rep = aSampleMo().with(new ThreePhaseElectricitySensor()).with(
                new ThreePhaseElectricitySensor()).build();

            // When
            var result = inventory.Create(rep);

            // Then
            Assert.NotNull(result.Id);
            Assert.NotNull(result.get<ThreePhaseElectricitySensor>());
        }

        [Fact]
        public void CreateManagedObjectWithCoordinateFragment()
        {
            // Given
            var coordinate = new Position();
            coordinate.Lat = 100.0m;
            coordinate.Lng = 10.0m;
            var rep = aSampleMo().with(coordinate).build();

            // When
            var result = inventory.Create(rep);

            // Then
            Assert.NotNull(result.Id);
            var fragment = result.get<Position>();
            Assert.IsType(coordinate.GetType(), fragment);
        }

        [Fact]
        public void CreateManagedObjectWithoutResponseBody()
        {
            // Given
            fixture.platform.RequireResponseBody = false;
            var rep = aSampleMo().build();

            // When
            var created = inventory.Create(rep);

            // Then
            Assert.NotNull(created);
//            Assert.NotNull(created.Self);
//            Assert.NotSame(created, rep);
        }

        [Fact]
        public void 
	        CreateManagedObjectWithThreePhaseElectricitySensor()
        {
            // Given
            var rep = aSampleMo().with(new ThreePhaseElectricitySensor()).build();

            // When
            var result = inventory.Create(rep);

            // Then
            Assert.NotNull(result.Id);
            Assert.NotNull(result.get<ThreePhaseElectricitySensor>());
        }
        
        [Fact]
        public  void updatingManagedObjectByMultipleThreads()
        {
            //
        }
        

     [Fact]
     public  void tryToGetNonExistentManagedObject()
     {
            // Given
            ManagedObjectRepresentation rep = aSampleMo().withID(new GId("1")).build();
    
            // Then
         var ex = Record.Exception(() => inventory.GetManagedObject(rep.Id).Get());
    
         Assert.NotNull(ex);
         Assert.IsType<SDKException>(ex);
         Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);		
     }

    [Fact]
	public void tryToDeleteNonExistentManagedObject()
	{
		// Given
		ManagedObjectRepresentation rep = aSampleMo().withID(new GId("1")).build();
	    
	    // When
	    var ex = Record.Exception(() => inventory.GetManagedObject(rep.Id).Delete());
	    
	    // Then
	    Assert.NotNull(ex);
	    Assert.IsType<SDKException>(ex);
	    Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);	
	}

	[Fact]
	public  void tryToUpdateNonExistentManagedObject()
	{
		// Given
		ManagedObjectRepresentation rep = aSampleMo().build();
	    
	    // When
	    var ex = Record.Exception(() => inventory.GetManagedObject(new GId("1")).Update(rep));
	    
	    // Then
	    Assert.NotNull(ex);
	    Assert.IsType<SDKException>(ex);
	    Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);	
	}

	[Fact]
	public  void getAllWhenNoManagedObjectPresent()
	{
		// When
		ManagedObjectCollectionRepresentation mos = inventory.GetManagedObjectsByFilter((new InventoryFilter()).ByType("not_existing_mo_type")).GetFirstPage();

		// Then
		Assert.Equal(0,mos.ManagedObjects.Count);
	}

	[Fact]
	public  void getAllWhen2ManagedObjectArePresent()
	{
		
		// Given
		ManagedObjectRepresentation rep1 = aSampleMo().withType("type1").build();
		ManagedObjectRepresentation rep2 = aSampleMo().withType("type1").build();

		// When
		var c1 = inventory.Create(rep1);
		var c2 = inventory.Create(rep2);

		// Then
		ManagedObjectCollectionRepresentation mos = inventory.GetManagedObjectsByFilter((new InventoryFilter()).ByType("type1")).GetFirstPage();
		Assert.Equal(2, mos.ManagedObjects.Count);
	}

	[Fact]
	public void addGetAndRemoveChildDevices()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().withName("parent1").build());
		ManagedObjectRepresentation child1 = inventory.Create(aSampleMo().withName("child11").build());
		ManagedObjectRepresentation child2 = inventory.Create(aSampleMo().withName("child21").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child1).build();

		// When
		var parentMo = inventory.GetManagedObject(parent.Id);
		parentMo.AddChildDevice(childRef1);
		parentMo.AddChildDevice(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.GetManagedObject(parent.Id).ChildDevices.GetFirstPage();

		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };
		//Assert.Equal(childDeviceIDs, new HashSet<GId>() {child1.Id, child2.Id });
		Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));
		// When
		parentMo.DeleteChildDevice(child1.Id);
		parentMo.DeleteChildDevice(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildDevices = inventory.GetManagedObject(parent.Id).ChildDevices.GetFirstPage();
		Assert.Equal(0,allChildDevices.References.Count);
	}
	        
	[Fact]
	public virtual void getPagedChildDevices()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().withName("parent").build());
		var parentMo = inventory.GetManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.Create(aSampleMo().withName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			parentMo.AddChildDevice(childRef);
		}

		// When
		var refCollection = inventory.GetManagedObject(parent.Id).ChildDevices;

		// Then
		assertCollectionPaged(refCollection);

	}

   //ORIGINAL LINE: private void assertCollectionPaged(ManagedObjectReferenceCollection refCollection) throws SDKException
	private void assertCollectionPaged(IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> refCollection)
	{
		var test = refCollection.GetFirstPage();
		Assert.Equal(refCollection.GetFirstPage().References.Count, this.fixture.platform.PageSize);
		Assert.True(refCollection.GetFirstPage().Self.Contains("pageSize=" + this.fixture.platform.PageSize + "&currentPage=1"));
		Assert.True(refCollection.GetFirstPage().Next.Contains("pageSize=" + this.fixture.platform.PageSize + "&currentPage=2"));
		Assert.Null(refCollection.GetFirstPage().Prev);
		
		Assert.Equal(refCollection.GetFirstPage().PageStatistics.CurrentPage, 1);

		ManagedObjectReferenceCollectionRepresentation secondPage = refCollection.GetPage(refCollection.GetFirstPage(), 2);
		Assert.Equal(secondPage.References.Count, 1);
	}

//ORIGINAL LINE: @Test public void addGetAndRemoveChildAssets() throws Exception 
	[Fact]
	public virtual void addGetAndRemoveChildAssets()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().withName("parent").build());
		ManagedObjectRepresentation child1 = inventory.Create(aSampleMo().withName("child1").build());
		ManagedObjectRepresentation child2 = inventory.Create(aSampleMo().withName("child2").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child1).build();

		// When
		var parentMo = inventory.GetManagedObject(parent.Id);
		parentMo.addChildAssets(childRef1);
		parentMo.AddChildAssets(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.GetManagedObject(parent.Id).ChildAssets.GetFirstPage();
		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };

		Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));

		// When
		parentMo.DeleteChildAsset(child1.Id);
		parentMo.DeleteChildAsset(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildDevices = inventory.GetManagedObject(parent.Id).ChildAssets.GetFirstPage();
		Assert.Equal(0,allChildDevices.References.Count);
	}

//ORIGINAL LINE: @Test public void addGetAndRemoveChildAdditions() throws Exception
[Fact]
public virtual void addGetAndRemoveChildAdditions()
{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().withName("parent").build());
		ManagedObjectRepresentation child1 = inventory.Create(aSampleMo().withName("child1").build());
		ManagedObjectRepresentation child2 = inventory.Create(aSampleMo().withName("child2").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child1).build();

		// When
		var parentMo = inventory.GetManagedObject(parent.Id);
		parentMo.AddChildAdditions(childRef1);
		parentMo.AddChildAdditions(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.GetManagedObject(parent.Id).ChildAdditions.GetFirstPage();
		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
	
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };
		//Assert.Equal(childDeviceIDs, new HashSet<GId>() {  child1.Id,child2.Id });
	    Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));
		// When
		parentMo.DeleteChildAddition(child1.Id);
		parentMo.DeleteChildAddition(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildAdditions = inventory.GetManagedObject(parent.Id).ChildAdditions.GetFirstPage();
		//assertEquals(0, allChildAdditions.References.size());
	    Assert.Equal(0,allChildAdditions.References.Count);
}


    //ORIGINAL LINE: @Test public void getPagedChildAssets() throws Exception
	[Fact]
	public virtual void getPagedChildAssets()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().withName("parent").build());
		var parentMo = inventory.GetManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.Create(aSampleMo().withName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			parentMo.addChildAssets(childRef);
		}

		// When
		var refCollection = inventory.GetManagedObject(parent.Id).ChildAssets;

		// Then
		assertCollectionPaged(refCollection);

	}


    //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
	[Fact]    
	public virtual void getPagedChildAdditions()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().withName("parent").build());
		var parentMo = inventory.GetManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.Create(aSampleMo().withName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			parentMo.AddChildAdditions(childRef);
		}

		// When
		var refCollection = inventory.GetManagedObject(parent.Id).ChildAdditions;

		// Then
		assertCollectionPaged(refCollection);
	}

    //ORIGINAL LINE: @Test public void queryWithManagedObjectType() throws Exception
	[Fact]  
	public virtual void queryWithManagedObjectType()
	{
		// Given
		inventory.Create(aSampleMo().withType("typeA").withName("A1").build());
		inventory.Create(aSampleMo().withType("typeA").withName("A2").build());
		inventory.Create(aSampleMo().withType("typeB").withName("B").build());

		// When
		InventoryFilter filterA = (new InventoryFilter()).ByType("typeA");
		ManagedObjectCollectionRepresentation typeAMos = inventory.GetManagedObjectsByFilter(filterA).GetFirstPage();

		// Then
		Assert.Equal(2, typeAMos.ManagedObjects.Count);

		// When
		InventoryFilter filterB = (new InventoryFilter()).ByType("typeB");
		ManagedObjectCollectionRepresentation typeBMos = inventory.GetManagedObjectsByFilter(filterB).GetFirstPage();

		// Then
		Assert.Equal(1, typeBMos.ManagedObjects.Count);
	}

    //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
	[Fact]      
	public virtual void bulkQuery()
	{
		// Given
		ManagedObjectRepresentation mo1 = inventory.Create(aSampleMo().withName("MO1").build());
		ManagedObjectRepresentation mo3 = inventory.Create(aSampleMo().withName("MO3").build());
		inventory.Create(aSampleMo().withName("MO2").build());

		// When
		ManagedObjectCollectionRepresentation moCollection = inventory.GetManagedObjectsByFilter((new InventoryFilter())
			.ByIds(new List<GId>{ mo3.Id,mo1.Id})).GetFirstPage();
		
		// Then
		IList<ManagedObjectRepresentation> mos = moCollection.ManagedObjects;
		Assert.Equal(mos.Count, 2);
	  
		//TODO: Fix ordering when fixed on mongo		
		var lst = new List<string> {"MO1", "MO3"};
		Assert.True(lst.Contains(mos[0].Name));
		Assert.True(lst.Contains(mos[1].Name));
	}

    //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
	[Fact]
	public virtual void queryWithFragmentType()
	{
		//Given
		inventory.Create(aSampleMo().withName("MO1").with(new Coordinate()).build());
		inventory.Create(aSampleMo().withName("MO2").with(new SecondFragment()).build());

		//When
		InventoryFilter filter = (new InventoryFilter()).ByFragmentType(typeof(SecondFragment));
		ManagedObjectCollectionRepresentation coordinates = inventory.GetManagedObjectsByFilter(filter).GetFirstPage();

		//Then
		Assert.Equal(coordinates.ManagedObjects.Count, 1);
	}

    //ORIGINAL LINE: @Test public void getAllWhen20ManagedObjectsPresent() throws Exception
	[Fact]
	public virtual void getAllWhen20ManagedObjectsPresent()
	{
		// Given
		int numOfMos = 20;
		for (int i = 0; i < numOfMos; i++)
		{
			inventory.Create(aSampleMo().withName("MO" + i).with(new Coordinate()).build());
		}

		var item = inventory.ManagedObjects;

		// When
		ManagedObjectCollectionRepresentation mos = inventory.ManagedObjects.GetFirstPage();

		// Then
		//assertThat(mos.PageStatistics.TotalPages, @is(greaterThanOrEqualTo(4)));

		// When
		ManagedObjectCollectionRepresentation secondPage = inventory.ManagedObjects.GetPage(mos, 2);

		// Then
		Assert.Equal(secondPage.PageStatistics.CurrentPage, 2);

		// When
		ManagedObjectCollectionRepresentation thirdPage = inventory.ManagedObjects.GetNextPage(secondPage);

		// Then
		Assert.Equal(thirdPage.PageStatistics.CurrentPage, 3);

		// When
		ManagedObjectCollectionRepresentation firstPage = inventory.ManagedObjects.GetPreviousPage(secondPage);
		Assert.Equal(firstPage.PageStatistics.CurrentPage, 1);
	}

	private void deleteMOs(IList<ManagedObjectRepresentation> mosOn1stPage)
	{
		foreach (ManagedObjectRepresentation mo in mosOn1stPage)
		{
			inventory.GetManagedObject(mo.Id).Delete();
		}
	}

	private IList<ManagedObjectRepresentation> MOsFrom1stPage
	{
		get
		{
			return inventory.ManagedObjects.GetFirstPage().ManagedObjects;
		}
	}

	public void Dispose()
	{

	}
    }
}