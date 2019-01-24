#region Cumulocity GmbH

// /*
//  * Copyright (C) 2015-2018
//  *
//  * Permission is hereby granted, free of charge, to any person obtaining a copy of
//  * this software and associated documentation files (the "Software"),
//  * to deal in the Software without restriction, including without limitation the rights to use,
//  * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
//  * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  *
//  * The above copyright notice and this permission notice shall be
//  * included in all copies or substantial portions of the Software.
//  *
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  */

#endregion Cumulocity GmbH

using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Identity
{
	public class IdentityIT : IClassFixture<IdentityFixture>,IDisposable
	{
		public IdentityIT(IdentityFixture fixture)
		{
			Fixture = fixture;
			IdentityApi = fixture.platform.IdentityApi;
			Input = new List<ExternalIDRepresentation>();
			Result1 = new List<ExternalIDRepresentation>();
			NotFound = false;
		}

		public IIdentityApi IdentityApi { get; set; }
		public IdentityFixture Fixture { get; set; }
		public List<ExternalIDRepresentation> Input { get; set; }
		public List<ExternalIDRepresentation> Result1 { get; set; }
		public bool NotFound { get; set; }

		public void iHaveManagedObject(long globalId, string extId, string type)
		{
			var rep = new ExternalIDRepresentation();
			rep.ExternalId = extId;
			rep.Type = type;
			var mo = new ManagedObjectRepresentation();
			var gId = new GId();
			gId.Value = globalId.ToString();
			mo.Id = gId;
			rep.ManagedObject = mo;
			Input.Add(rep);
		}

		public virtual void iCallCreate()
		{
			var io = Input.First();
			var id = IdentityApi.Create(io);
			Result1.Add(id);
		}

		public virtual void shouldGetBackTheExternalId()
		{
			var collection1 = IdentityApi.GetExternalIdsOfGlobalId(Input.First().ManagedObject.Id).GetFirstPage();
			Assert.Equal(1, collection1.ExternalIds.Count);
			Assert.Equal(Input.First().ExternalId, collection1.ExternalIds.First().ExternalId);
			Assert.Equal(Input.First().Type, collection1.ExternalIds.First().Type);
			Assert.Equal(Input.First().ManagedObject.Id.Value, collection1.ExternalIds.First().ManagedObject.Id.Value);
		}

		public virtual void iGetTheExternalId()
		{
			Result1.Clear();
			try
			{
				XtId id = new XtId(Input.First().ExternalId);
				id.Type = Input.First().Type;
				Result1.Add(IdentityApi.GetExternalId(id));
			}
			catch (SDKException e)
			{
				NotFound = (e.HttpStatus.Equals(404));
			}
		}

		//@When("I Create all external ids")
		public void iCreateAll()
		{
			foreach (ExternalIDRepresentation rep in Input)
			{
				Result1.Add(IdentityApi.Create(rep));
			}
		}

		//@Then("I should GetFirstPage back all the external ids")
		public void shouldGetBackAllTheIds()
		{
			var collection1 = IdentityApi.GetExternalIdsOfGlobalId(Input.First().ManagedObject.Id).GetFirstPage();
			Assert.Equal(Input.Count, collection1.ExternalIds.Count);

			Dictionary<String, ExternalIDRepresentation> result = new Dictionary<String, ExternalIDRepresentation>();

			for (int index = 0; index < Input.Count; index++)
			{
				result.Add(collection1.ExternalIds[index].ExternalId, collection1.ExternalIds[index]);
			}

			for (int index = 0; index < Input.Count; index++)
			{
				ExternalIDRepresentation rep = result[Input[index].ExternalId];
				Assert.NotNull(rep);
				Assert.Equal(Input[index].Type, rep.Type);
				Assert.Equal(Input[index].ManagedObject.Id.Value, rep.ManagedObject.Id.Value);
			}
		}

		//@When("I Delete the external id")
		public void iDeleteTheExternalId() {  
			ExternalIDRepresentation extIdRep = new ExternalIDRepresentation();
			extIdRep.ExternalId = Input.First().ExternalId;
			extIdRep.Type = Input.First().Type;
		    IdentityApi.DeleteExternalId(extIdRep);
	}

		//@Then("External id should not be found")
		public void shouldNotBeFound()
		{
			Assert.True(NotFound);
		}


		//Scenario: Create one external id and GetFirstPage all for the global id
		[Fact]
		public void createExternalIdAndGetAllForTheGlobalId()
		{
			//Given I have external id for '100' with value 'DN-1' and type 'com.nsn.DN'
			iHaveManagedObject(100, "DN-1", "com.nsn.DN");
			//When I Create the external id
			iCallCreate();
			//Then I should GetFirstPage back the external id
			shouldGetBackTheExternalId();
		}

		//
		//    Scenario: Create multiple external ids and GetFirstPage all for the global id
		[Fact]
		public void createMultipleExternalIdsAndGetAllForTheGlobalId()
		{
			//    Given I have the global id '200' with following external ids:
			//            | type | value |
			//            | com.type1 | 1002 |
			//            | com.dn | DN-1 |
			iHaveManagedObject(200, "com.type1", "1002");
			iHaveManagedObject(200, "com.dn", "DN-1");
			//    When I Create all external ids
			iCreateAll();
			//    Then I should GetFirstPage back all the external ids
			shouldGetBackAllTheIds();
		}

		//
		//    Scenario: Create one external id and GetFirstPage the external id
		[Fact]
		public void createOneExternalIdAndGetTheExternalId()
		{
			//    Given I have external id for '100' with value 'DN-1' and type 'com.nsn.DN'
			iHaveManagedObject(100, "DN-1", "com.nsn.DN");
			//    When I Create the external id
			iCallCreate();
			//    And I GetFirstPage the external id
			iGetTheExternalId();
			//    Then I should GetFirstPage back the external id
			shouldGetBackTheExternalId();
		}

		//
		//    Scenario: Create one external id, Delete it and GetFirstPage the external id

		[Fact]
		public void createOneExternalIdAndDeleteIdAndGetTheExternalId()
		{
		//    Given I have external id for '100' with value 'DN-1' and type 'com.nsn.DN'
			iHaveManagedObject(100, "DN-1", "com.nsn.DN");
        //    When I Create the external id
		iCallCreate();
		//    And I Delete the external id
		iDeleteTheExternalId();
		//    And I GetFirstPage the external id
		iGetTheExternalId();
		//    Then External id should not be found
		shouldNotBeFound();
	}

		public void Dispose()
		{
			Fixture?.Dispose();

			foreach (ExternalIDRepresentation e in Input)
			{
				try
				{
					IdentityApi.DeleteExternalId(e);
				}
				catch (SDKException e1)
				{
					if (e1.HttpStatus != 404)
					{
						throw e1;
					}
				}
			}
		}
	}
}