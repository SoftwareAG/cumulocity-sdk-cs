using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
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
#pragma warning disable 0612
	[Collection("Live tests")]
	public class InventoryTests : IDisposable
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
        public void CreateAndDeleteManagedObject()
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
        public void CreateAndGetManagedObject()
        {
            // Given
            var rep = aSampleMo().build();
            var created = inventory.Create(rep);

			// When
#pragma warning disable 0612
			var result = inventory.GetManagedObject(created.Id).Get();

            // Then
            Assert.Equal(result.Id, created.Id);
            Assert.Equal(result.Name, created.Name);
            Assert.Equal(result.Type, created.Type);
        }

        [Fact]
        public void CreateAndUpdateManagedObject()
        {
            // Given
            var rep = aSampleMo().build();
            var result = inventory.Create(rep);

            // When
            var coordinate = new Position {Lat = 100.0m, Lng = 10.0m};
            result.Set(coordinate);

            var id = result.Id;
            result.Id = null;
            result.LastUpdated = null;

            var updated = inventory.GetManagedObject(id).Update(result);
            Assert.NotNull(updated);
            Assert.Equal(coordinate, updated.Get<Position>());
        }

        [Fact]
        public void CreateAndUpdateManagedObjectByRemovingFragment()
        {
            // Given
            var rep = aSampleMo().With(new Position()).build();
            var created = inventory.Create(rep);

            //When
            created.Set(new object(), "c8y_Position");
            var id = created.Id;
            created.Id = null;
            created.LastUpdated = null;
#pragma warning disable 0612
			var updated = inventory.GetManagedObject(id).Update(created);

            // Then
            Assert.NotNull(updated.Id);
            Assert.NotNull(updated.Get<Position>());
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
        public void CreateManagedObjectWith2ThreePhaseElectricityFragments()
        {
            // Given
            var rep = aSampleMo().With(new ThreePhaseElectricitySensor()).With(
                new ThreePhaseElectricitySensor()).build();

            // When
            var result = inventory.Create(rep);

            // Then
            Assert.NotNull(result.Id);
            Assert.NotNull(result.Get<ThreePhaseElectricitySensor>());
        }

        [Fact]
        public void CreateManagedObjectWithCoordinateFragment()
        {
            // Given
            var coordinate = new Position();
            coordinate.Lat = 100.0m;
            coordinate.Lng = 10.0m;
            var rep = aSampleMo().With(coordinate).build();

            // When
            var result = inventory.Create(rep);

            // Then
            Assert.NotNull(result.Id);
            var fragment = result.Get<Position>();
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
			//Assert.NotNull(created.Self);
			//Assert.NotSame(created, rep);
        }

        [Fact]
        public void 
	        CreateManagedObjectWithThreePhaseElectricitySensor()
        {
            // Given
            var rep = aSampleMo().With(new ThreePhaseElectricitySensor()).build();

            // When
            var result = inventory.Create(rep);

            // Then
            Assert.NotNull(result.Id);
            Assert.NotNull(result.Get<ThreePhaseElectricitySensor>());
        }
        
        [Fact]
        public  void UpdatingManagedObjectByMultipleThreads()
        {
            //
        }
        

     [Fact]
     public  void TryToGetNonExistentManagedObject()
     {
            // Given
            ManagedObjectRepresentation rep = aSampleMo().WithID(new GId("1")).build();
    
            // Then
         var ex = Record.Exception(() => inventory.GetManagedObject(rep.Id).Get());
    
         Assert.NotNull(ex);
         Assert.IsType<SDKException>(ex);
         Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);		
     }

    [Fact]
	public void TryToDeleteNonExistentManagedObject()
	{
		// Given
		ManagedObjectRepresentation rep = aSampleMo().WithID(new GId("1")).build();
	    
	    // When
	    var ex = Record.Exception(() => inventory.GetManagedObject(rep.Id).Delete());
	    
	    // Then
	    Assert.NotNull(ex);
	    Assert.IsType<SDKException>(ex);
	    Assert.Equal((int) HttpStatusCode.NotFound, ((SDKException) ex).HttpStatus);	
	}

	[Fact]
	public  void TryToUpdateNonExistentManagedObject()
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
	public  void GetAllWhenNoManagedObjectPresent()
	{
		// When
		ManagedObjectCollectionRepresentation mos = inventory.GetManagedObjectsByFilter((new InventoryFilter()).ByType("not_existing_mo_type")).GetFirstPage();

		// Then
		Assert.Equal(0,mos.ManagedObjects.Count);
	}

	[Fact]
	public  void GetAllWhen2ManagedObjectArePresent()
	{
		var item = GetRandomString();
		
		// Given
		ManagedObjectRepresentation rep1 = aSampleMo().WithType(item).build();
		ManagedObjectRepresentation rep2 = aSampleMo().WithType(item).build();

		// When
		var c1 = inventory.Create(rep1);
		var c2 = inventory.Create(rep2);

		// Then
		ManagedObjectCollectionRepresentation mos = inventory.GetManagedObjectsByFilter((new InventoryFilter()).ByType(item)).GetFirstPage();
		Assert.Equal(2, mos.ManagedObjects.Count);
	}

	[Fact]
	public void AddGetAndRemoveChildDevices()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().WithName("parent1").build());
		ManagedObjectRepresentation child1 = inventory.Create(aSampleMo().WithName("child11").build());
		ManagedObjectRepresentation child2 = inventory.Create(aSampleMo().WithName("child21").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child1).Build();

		// When
		var parentMo = inventory.GetManagedObject(parent.Id);
		parentMo.AddChildDevice(childRef1);
		parentMo.AddChildDevice(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.GetManagedObject(parent.Id).ChildDevices.GetFirstPage();

		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };
		Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));
		// When
		parentMo.DeleteChildDevice(child1.Id);
		parentMo.DeleteChildDevice(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildDevices = inventory.GetManagedObject(parent.Id).ChildDevices.GetFirstPage();
		Assert.Equal(0,allChildDevices.References.Count);
	}
	        
	[Fact]
	public virtual void GetPagedChildDevices()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().WithName("parent").build());
		var parentMo = inventory.GetManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.Create(aSampleMo().WithName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child).Build();
			parentMo.AddChildDevice(childRef);
		}

		// When
		var refCollection = inventory.GetManagedObject(parent.Id).ChildDevices;

		// Then
		assertCollectionPaged(refCollection);

	}

	private void assertCollectionPaged(IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> refCollection)
	{
		Assert.Equal(refCollection.GetFirstPage().References.Count, this.fixture.platform.PageSize);
		Assert.Contains("pageSize=" + this.fixture.platform.PageSize + "&currentPage=1", refCollection.GetFirstPage().Self);
		Assert.Contains("pageSize=" + this.fixture.platform.PageSize + "&currentPage=2", refCollection.GetFirstPage().Next);
		Assert.Null(refCollection.GetFirstPage().Prev);
		
		Assert.Equal( 1, refCollection.GetFirstPage().PageStatistics.CurrentPage);

		ManagedObjectReferenceCollectionRepresentation secondPage = refCollection.GetPage(refCollection.GetFirstPage(), 2);
		Assert.Equal( 1, secondPage.References.Count);
	}

	[Fact]
	public virtual void AddGetAndRemoveChildAssets()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().WithName("parent").build());
		ManagedObjectRepresentation child1 = inventory.Create(aSampleMo().WithName("child1").build());
		ManagedObjectRepresentation child2 = inventory.Create(aSampleMo().WithName("child2").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child1).Build();

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

[Fact]
public virtual void AddGetAndRemoveChildAdditions()
{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().WithName("parent").build());
		ManagedObjectRepresentation child1 = inventory.Create(aSampleMo().WithName("child1").build());
		ManagedObjectRepresentation child2 = inventory.Create(aSampleMo().WithName("child2").build());

		ManagedObjectReferenceRepresentation childRef1 = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child1).Build();

		// When
		var parentMo = inventory.GetManagedObject(parent.Id);
		parentMo.AddChildAdditions(childRef1);
		parentMo.AddChildAdditions(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation refCollection = inventory.GetManagedObject(parent.Id).ChildAdditions.GetFirstPage();
		IList<ManagedObjectReferenceRepresentation> refs = refCollection.References;
	
		ISet<GId> childDeviceIDs = new HashSet<GId>() {refs[0].ManagedObject.Id, refs[1].ManagedObject.Id };
	    Assert.True(childDeviceIDs.SetEquals(new HashSet<GId>() {child1.Id, child2.Id}));
		// When
		parentMo.DeleteChildAddition(child1.Id);
		parentMo.DeleteChildAddition(child2.Id);

		// Then
		ManagedObjectReferenceCollectionRepresentation allChildAdditions = inventory.GetManagedObject(parent.Id).ChildAdditions.GetFirstPage();
	    Assert.Equal(0,allChildAdditions.References.Count);
}


	[Fact]
	public virtual void GetPagedChildAssets()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().WithName("parent").build());
		var parentMo = inventory.GetManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.Create(aSampleMo().WithName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child).Build();
			parentMo.addChildAssets(childRef);
		}

		// When
		var refCollection = inventory.GetManagedObject(parent.Id).ChildAssets;

		// Then
		assertCollectionPaged(refCollection);

	}


	[Fact]    
	public virtual void GetPagedChildAdditions()
	{
		// Given
		ManagedObjectRepresentation parent = inventory.Create(aSampleMo().WithName("parent").build());
		var parentMo = inventory.GetManagedObject(parent.Id);

		for (int i = 0; i < this.fixture.platform.PageSize+1; i++)
		{
			ManagedObjectRepresentation child = inventory.Create(aSampleMo().WithName("child" + i).build());
			ManagedObjectReferenceRepresentation childRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child).Build();
			parentMo.AddChildAdditions(childRef);
		}

		// When
		var refCollection = inventory.GetManagedObject(parent.Id).ChildAdditions;

		// Then
		assertCollectionPaged(refCollection);
	}

	[Fact]  
	public virtual void QueryWithManagedObjectType()
	{
		 var type1 = GetRandomString();
		 var type2 = GetRandomString();

		// Given
		inventory.Create(aSampleMo().WithType(type1).WithName("A1").build());
		inventory.Create(aSampleMo().WithType(type1).WithName("A2").build());
		inventory.Create(aSampleMo().WithType(type2).WithName("B").build());

		// When
		InventoryFilter filterA = (new InventoryFilter()).ByType(type1);
		ManagedObjectCollectionRepresentation typeAMos = inventory.GetManagedObjectsByFilter(filterA).GetFirstPage();

		// Then
		Assert.Equal(2, typeAMos.ManagedObjects.Count);

		// When
		InventoryFilter filterB = (new InventoryFilter()).ByType(type2);
		ManagedObjectCollectionRepresentation typeBMos = inventory.GetManagedObjectsByFilter(filterB).GetFirstPage();

		// Then
		Assert.Equal(1, typeBMos.ManagedObjects.Count);
	}

	[Fact]      
	public virtual void BulkQuery()
	{
		// Given
		ManagedObjectRepresentation mo1 = inventory.Create(aSampleMo().WithName("MO1").build());
		ManagedObjectRepresentation mo3 = inventory.Create(aSampleMo().WithName("MO3").build());
		inventory.Create(aSampleMo().WithName("MO2").build());

		// When
		ManagedObjectCollectionRepresentation moCollection = inventory.GetManagedObjectsByFilter((new InventoryFilter())
			.ByIds(new List<GId>{ mo3.Id,mo1.Id})).GetFirstPage();
		
		// Then
		IList<ManagedObjectRepresentation> mos = moCollection.ManagedObjects;
		Assert.Equal( 2, mos.Count);
	  
		//TODO: Fix ordering when fixed on mongo		
		var lst = new List<string> {"MO1", "MO3"};
		Assert.Contains(mos[0].Name, lst);
		Assert.Contains(mos[1].Name, lst);
	}

	[Fact]
	public virtual void QueryWithFragmentType()
	{
		//Given
		inventory.Create(aSampleMo().WithName("MO1").With(new Coordinate()).build());
		inventory.Create(aSampleMo().WithName("MO2").With(new SecondFragment()).build());

		//When
		InventoryFilter filter = (new InventoryFilter()).ByFragmentType(typeof(SecondFragment));
		ManagedObjectCollectionRepresentation coordinates = inventory.GetManagedObjectsByFilter(filter).GetFirstPage();

		//Then
		Assert.Equal( 1, coordinates.ManagedObjects.Count);
	}

    //ORIGINAL LINE: @Test public void getAllWhen20ManagedObjectsPresent() throws Exception
	[Fact]
	public virtual void GetAllWhen20ManagedObjectsPresent()
	{
		// Given
		int numOfMos = 20;
		for (int i = 0; i < numOfMos; i++)
		{
			inventory.Create(aSampleMo().WithName("MO" + i).With(new Coordinate()).build());
		}

		var item = inventory.ManagedObjects;

		// When
		ManagedObjectCollectionRepresentation mos = inventory.ManagedObjects.GetFirstPage();

		// When
		ManagedObjectCollectionRepresentation secondPage = inventory.ManagedObjects.GetPage(mos, 2);

		// Then
		Assert.Equal( 2, secondPage.PageStatistics.CurrentPage);

		// When
		ManagedObjectCollectionRepresentation thirdPage = inventory.ManagedObjects.GetNextPage(secondPage);

		// Then
		Assert.Equal( 3, thirdPage.PageStatistics.CurrentPage);

		// When
		ManagedObjectCollectionRepresentation firstPage = inventory.ManagedObjects.GetPreviousPage(secondPage);
		Assert.Equal( 1, firstPage.PageStatistics.CurrentPage);
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
	public static String GetRandomString()
	{
		var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
		var length = 15;

		var chars = new char[length];
		var rd = new Random();

		for (var i = 0; i < length; i++)
		{
			chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
		}

		return new String(chars);
	}
	}
}