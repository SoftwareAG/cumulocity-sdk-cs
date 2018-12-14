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
    public class InventoryTests : IClassFixture<InventoryFixture>
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
            var result = inventory.create(rep);

            // When
            var mo = inventory.getManagedObject(result.Id);
            mo.delete();

            // Then
            var deletedMo = inventory.getManagedObject(result.Id);
            var ex = Record.Exception(() => deletedMo.get());
            Assert.NotNull(ex);
            Assert.IsType<SDKException>(ex);
            Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);
        }

        [Fact]
        public void createAndGetManagedObject()
        {
            // Given
            var rep = aSampleMo().build();
            var created = inventory.create(rep);

            // When
            var result = inventory.getManagedObject(created.Id).get();

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
            var result = inventory.create(rep);

            // When
            var coordinate = new Position {Lat = 100.0m, Lng = 10.0m};
            result.set(coordinate);

            var id = result.Id;
            result.Id = null;
            result.LastUpdated = null;

            var updated = inventory.getManagedObject(id).update(result);
            Assert.NotNull(updated);
            Assert.Equal(coordinate, updated.get<Position>());
        }

        [Fact]
        public void createAndUpdateManagedObjectByRemovingFragment()
        {
            // Given
            var rep = aSampleMo().with(new Position()).build();
            var created = inventory.create(rep);

            //When
            created.set(new object(), "c8y_Position");
            var id = created.Id;
            created.Id = null;
            created.LastUpdated = null;
            var updated = inventory.getManagedObject(id).update(created);

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
            var created = inventory.create(rep);

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
            var result = inventory.create(rep);

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
            var result = inventory.create(rep);

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
            var created = inventory.create(rep);

            // Then
            Assert.NotNull(created.Id);
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
            var result = inventory.create(rep);

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
         var ex = Record.Exception(() => inventory.getManagedObject(rep.Id).get());
    
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
	    var ex = Record.Exception(() => inventory.getManagedObject(rep.Id).delete());
	    
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
	    var ex = Record.Exception(() => inventory.getManagedObject(new GId("1")).update(rep));
	    
	    // Then
	    Assert.NotNull(ex);
	    Assert.IsType<SDKException>(ex);
	    Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);	
	}

	[Fact]
	public  void getAllWhenNoManagedObjectPresent()
	{
		// When
		ManagedObjectCollectionRepresentation mos = inventory.getManagedObjectsByFilter((new InventoryFilter()).byType("not_existing_mo_type")).get();

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
		var c1 = inventory.create(rep1);
		var c2 = inventory.create(rep2);

		// Then
		ManagedObjectCollectionRepresentation mos = inventory.getManagedObjectsByFilter((new InventoryFilter()).byType("type1")).get();
		Assert.Equal(2, mos.ManagedObjects.Count);
	}

	[Fact]
	public void addGetAndRemoveChildDevices()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.create(aSampleMo().withName("parent1").build());
		ManagedObjectRepresentation child1 = inventory.create(aSampleMo().withName("child11").build());
		ManagedObjectRepresentation child2 = inventory.create(aSampleMo().withName("child21").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child1).build();

		// When
		var parentMo = inventory.getManagedObject(parent.Id);
		parentMo.addChildDevice(childRef1);
		parentMo.addChildDevice(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.getManagedObject(parent.Id).ChildDevices.get();

		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };
		//Assert.Equal(childDeviceIDs, new HashSet<GId>() {child1.Id, child2.Id });
		Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));
		// When
		parentMo.deleteChildDevice(child1.Id);
		parentMo.deleteChildDevice(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildDevices = inventory.getManagedObject(parent.Id).ChildDevices.get();
		Assert.Equal(0,allChildDevices.References.Count);
	}
	        
	[Fact]
	public virtual void getPagedChildDevices()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.create(aSampleMo().withName("parent").build());
		var parentMo = inventory.getManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.create(aSampleMo().withName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			parentMo.addChildDevice(childRef);
		}

		// When
		var refCollection = inventory.getManagedObject(parent.Id).ChildDevices;

		// Then
		assertCollectionPaged(refCollection);

	}

   //ORIGINAL LINE: private void assertCollectionPaged(ManagedObjectReferenceCollection refCollection) throws SDKException
	private void assertCollectionPaged(IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> refCollection)
	{
		var test = refCollection.get();
		Assert.Equal(refCollection.get().References.Count, this.fixture.platform.PageSize);
		Assert.True(refCollection.get().Self.Contains("pageSize=" + this.fixture.platform.PageSize + "&currentPage=1"));
		Assert.True(refCollection.get().Next.Contains("pageSize=" + this.fixture.platform.PageSize + "&currentPage=2"));
		Assert.Null(refCollection.get().Prev);
		
		Assert.Equal(refCollection.get().PageStatistics.CurrentPage, 1);

		ManagedObjectReferenceCollectionRepresentation secondPage = refCollection.getPage(refCollection.get(), 2);
		Assert.Equal(secondPage.References.Count, 1);
	}

//ORIGINAL LINE: @Test public void addGetAndRemoveChildAssets() throws Exception 
	[Fact]
	public virtual void addGetAndRemoveChildAssets()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.create(aSampleMo().withName("parent").build());
		ManagedObjectRepresentation child1 = inventory.create(aSampleMo().withName("child1").build());
		ManagedObjectRepresentation child2 = inventory.create(aSampleMo().withName("child2").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child1).build();

		// When
		var parentMo = inventory.getManagedObject(parent.Id);
		parentMo.addChildAssets(childRef1);
		parentMo.addChildAssets(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.getManagedObject(parent.Id).ChildAssets.get();
		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };

		Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));

		// When
		parentMo.deleteChildAsset(child1.Id);
		parentMo.deleteChildAsset(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildDevices = inventory.getManagedObject(parent.Id).ChildAssets.get();
		Assert.Equal(0,allChildDevices.References.Count);
	}

//ORIGINAL LINE: @Test public void addGetAndRemoveChildAdditions() throws Exception
[Fact]
public virtual void addGetAndRemoveChildAdditions()
{
		// Given
		ManagedObjectRepresentation parent = inventory.create(aSampleMo().withName("parent").build());
		ManagedObjectRepresentation child1 = inventory.create(aSampleMo().withName("child1").build());
		ManagedObjectRepresentation child2 = inventory.create(aSampleMo().withName("child2").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child1).build();

		// When
		var parentMo = inventory.getManagedObject(parent.Id);
		parentMo.addChildAdditions(childRef1);
		parentMo.addChildAdditions(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.getManagedObject(parent.Id).ChildAdditions.get();
		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
	
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };
		//Assert.Equal(childDeviceIDs, new HashSet<GId>() {  child1.Id,child2.Id });
	    Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));
		// When
		parentMo.deleteChildAddition(child1.Id);
		parentMo.deleteChildAddition(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildAdditions = inventory.getManagedObject(parent.Id).ChildAdditions.get();
		//assertEquals(0, allChildAdditions.References.size());
	    Assert.Equal(0,allChildAdditions.References.Count);
}


    //ORIGINAL LINE: @Test public void getPagedChildAssets() throws Exception
	[Fact]
	public virtual void getPagedChildAssets()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.create(aSampleMo().withName("parent").build());
		var parentMo = inventory.getManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.create(aSampleMo().withName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			parentMo.addChildAssets(childRef);
		}

		// When
		var refCollection = inventory.getManagedObject(parent.Id).ChildAssets;

		// Then
		assertCollectionPaged(refCollection);

	}


    //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
	[Fact]    
	public virtual void getPagedChildAdditions()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.create(aSampleMo().withName("parent").build());
		var parentMo = inventory.getManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.create(aSampleMo().withName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			parentMo.addChildAdditions(childRef);
		}

		// When
		var refCollection = inventory.getManagedObject(parent.Id).ChildAdditions;

		// Then
		assertCollectionPaged(refCollection);
	}

    //ORIGINAL LINE: @Test public void queryWithManagedObjectType() throws Exception
	[Fact]  
	public virtual void queryWithManagedObjectType()
	{
		// Given
		inventory.create(aSampleMo().withType("typeA").withName("A1").build());
		inventory.create(aSampleMo().withType("typeA").withName("A2").build());
		inventory.create(aSampleMo().withType("typeB").withName("B").build());

		// When
		InventoryFilter filterA = (new InventoryFilter()).byType("typeA");
		ManagedObjectCollectionRepresentation typeAMos = inventory.getManagedObjectsByFilter(filterA).get();

		// Then
		Assert.Equal(typeAMos.ManagedObjects.Count, 2);

		// When
		InventoryFilter filterB = (new InventoryFilter()).byType("typeB");
		ManagedObjectCollectionRepresentation typeBMos = inventory.getManagedObjectsByFilter(filterB).get();

		// Then
		Assert.Equal(typeBMos.ManagedObjects.Count, 1);
	}

    //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
	[Fact]      
	public virtual void bulkQuery()
	{
		// Given
		ManagedObjectRepresentation mo1 = inventory.create(aSampleMo().withName("MO1").build());
		ManagedObjectRepresentation mo3 = inventory.create(aSampleMo().withName("MO3").build());
		inventory.create(aSampleMo().withName("MO2").build());

		// When
		ManagedObjectCollectionRepresentation moCollection = inventory.getManagedObjectsByFilter((new InventoryFilter())
			.byIds(new List<GId>{ mo3.Id,mo1.Id})).get();
		
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
		inventory.create(aSampleMo().withName("MO1").with(new Coordinate()).build());
		inventory.create(aSampleMo().withName("MO2").with(new SecondFragment()).build());

		//When
		InventoryFilter filter = (new InventoryFilter()).byFragmentType(typeof(SecondFragment));
		ManagedObjectCollectionRepresentation coordinates = inventory.getManagedObjectsByFilter(filter).get();

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
			inventory.create(aSampleMo().withName("MO" + i).with(new Coordinate()).build());
		}

		var item = inventory.ManagedObjects;

		// When
		ManagedObjectCollectionRepresentation mos = inventory.ManagedObjects.get();

		// Then
		//assertThat(mos.PageStatistics.TotalPages, @is(greaterThanOrEqualTo(4)));

		// When
		ManagedObjectCollectionRepresentation secondPage = inventory.ManagedObjects.getPage(mos, 2);

		// Then
		Assert.Equal(secondPage.PageStatistics.CurrentPage, 2);

		// When
		ManagedObjectCollectionRepresentation thirdPage = inventory.ManagedObjects.getNextPage(secondPage);

		// Then
		Assert.Equal(thirdPage.PageStatistics.CurrentPage, 3);

		// When
		ManagedObjectCollectionRepresentation firstPage = inventory.ManagedObjects.getPreviousPage(secondPage);
		Assert.Equal(firstPage.PageStatistics.CurrentPage, 1);
	}

	private void deleteMOs(IList<ManagedObjectRepresentation> mosOn1stPage)
	{
		foreach (ManagedObjectRepresentation mo in mosOn1stPage)
		{
			inventory.getManagedObject(mo.Id).delete();
		}
	}

	private IList<ManagedObjectRepresentation> MOsFrom1stPage
	{
		get
		{
			return inventory.ManagedObjects.get().ManagedObjects;
		}
	}
	    
    }
}