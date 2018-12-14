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

#endregion

using System.Collections.Generic;
using System.Linq;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Identity
{
	public class IdentityIT : IClassFixture<IdentityFixture>
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
			var io= Input.First();
			var id =IdentityApi.create(io);
			Result1.Add(id);
		}

		public virtual void shouldGetBackTheExternalId()
		{
			var collection1 = IdentityApi.getExternalIdsOfGlobalId(Input.First().ManagedObject.Id).get();
			Assert.Equal(1, collection1.ExternalIds.Count);
			Assert.Equal(Input.First().ExternalId, collection1.ExternalIds.First().ExternalId);
			Assert.Equal(Input.First().Type, collection1.ExternalIds.First().Type);
			Assert.Equal(Input.First().ManagedObject.Id.Value, collection1.ExternalIds.First().ManagedObject.Id.Value);
		}

		//Scenario: Create one external id and get all for the global id
		[Fact]
		public void createExternalIdAndGetAllForTheGlobalId()
		{
			//Given I have external id for '100' with value 'DN-1' and type 'com.nsn.DN'
			iHaveManagedObject(100, "DN-1", "com.nsn.DN");
			//When I create the external id
			iCallCreate();
			//Then I should get back the external id
			shouldGetBackTheExternalId();
		}
	}
}