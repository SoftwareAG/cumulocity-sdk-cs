using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Inventory
{
    public class MOChildrenOrderingITTest
    {
        private InventoryFixture fixture;
        private IInventoryApi inventory;

        public MOChildrenOrderingITTest(InventoryFixture fixture)
        {
            this.fixture = fixture;
            inventory = fixture.platform.InventoryApi;	
            ManagedObjectRepresentation parentRep = inventory.Create(aSampleMo().WithName("parentRep").build());
        }
        
        private static ManagedObjectRepresentationBuilder aSampleMo()
        {
            return Cumulocity.SDK.Client.Rest.Representation.Builder.RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation.MO_REPRESENTATION);
        }
    }
}