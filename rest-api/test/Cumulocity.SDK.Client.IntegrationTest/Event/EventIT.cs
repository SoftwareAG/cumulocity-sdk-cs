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

using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Event
{
	public class EventIT : IClassFixture<EventFixture>, IDisposable
	{
		public EventIT(EventFixture fixture)
		{
			EventApi = fixture.platform.EventApi;
			Input = new List<EventRepresentation>();
			Result = new List<EventRepresentation>();
			Result1 = new List<EventRepresentation>();
			Status = 200;

			var mo = new ManagedObjectRepresentation();
			mo.Name = "MO";
			managedObject = fixture.platform.InventoryApi.create(mo);
		}

		public void Dispose()
		{
		}

		private readonly ManagedObjectRepresentation managedObject;

		public object Status { get; set; }

		public List<EventRepresentation> Result1 { get; set; }

		public List<EventRepresentation> Input { get; set; }
		public List<EventRepresentation> Result { get; }
		public IEventApi EventApi { get; set; }

		private EventCollectionRepresentation collection;

		private EventCollectionRepresentation collection1;

		// ------------------------------------------------------------------------
		// Given
		// ------------------------------------------------------------------------
		//@Given("I have '(\\d+)' Events of type '([^']*)' for the managed object$")
		private void iHaveEvents(int n, string type)
		{
			for (var i = 0; i < n; i++)
			{
				var rep = new EventRepresentation();
				rep.Type = type;
				rep.DateTime = DateTime.UtcNow;
				rep.Text = " Event of Managed Object : " + i;
				rep.Source = managedObject;
				Input.Add(rep);
			}
		}

		//@Given("I have a Event with no type value for the managed object$")

		public void iHaveAEventWithNoType()
		{
			EventRepresentation rep = new EventRepresentation();
			rep.DateTime = DateTime.UtcNow;
			rep.Text = " Event of Managed Object : 1";
			rep.Source = managedObject;
			Input.Add(rep);
		}
		//@Given("I have a Event with no text value for the managed object$")
		public void iHaveAEventWithNoText()
		{
			EventRepresentation rep = new EventRepresentation();
			rep.Type = "type";
			rep.DateTime = DateTime.UtcNow;
			rep.Source = managedObject;
			Input.Add(rep);
		}

		//@Given("I have a Event with no time value for the managed object$")
		public void iHaveAEventWithNoTime()
		{
			EventRepresentation rep = new EventRepresentation();
			rep.Type = "type";
			rep.Text = " Event of Managed Object : 1";
			rep.Source = managedObject;
			Input.Add(rep);
		}
		// ------------------------------------------------------------------------
		// When
		// ------------------------------------------------------------------------

		//@When("I create all Events$")
		public void iCreateAll()
		{
			try
			{
				foreach (var rep in Input)
				{
					var evt = EventApi.create(rep);
					Result.Add(evt);
				}
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I get all Events$")
		public void iGetAllEvents()
		{
			try
			{
				collection = EventApi.getEvents().get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}
		// ------------------------------------------------------------------------
		// Then
		// ------------------------------------------------------------------------
		public void iShouldGetNumberOfEvents(int count)
		{
			Assert.Equal(collection.Events.Count, count);
		}
		//@Then("Event response should be unprocessable$")
		public void shouldBeBadRequest()
		{
			Assert.Equal(422, Status);
		}
		// ------------------------------------------------------------------------
		// Test
		// ------------------------------------------------------------------------

		//    Scenario: Create Events
		[Fact]
		public void createEvents()
		{
			//    Given I have '2' Events of type 'type1' for the managed object
			iHaveEvents(2, "type1");
			//    When I create all Events
			iCreateAll();
			//    And I get all Events
			iGetAllEvents();
			//    Then I should get '2' Events
			iShouldGetNumberOfEvents(2);
		}

		//    Scenario: Create Events without type
		[Fact]
		public void createEventsWithoutType() 
		{
			//    Given I have a Event with no type value for the managed object
			iHaveAEventWithNoType();
			//    When I create all Events
			iCreateAll();
			//    Then Event response should be unprocessable
			shouldBeBadRequest();
		}

		//
		//
		//    Scenario: Create Events without time
		[Fact]
		public void createEventsWithoutTime() 
		{
			//    Given I have a Event with no time value for the managed object
			iHaveAEventWithNoTime();
			//    When I create all Events
			iCreateAll();
			//    Then Event response should be unprocessable
			shouldBeBadRequest();
		}

	//
	//    Scenario: Create Events without text
	[Fact]
	public void createEventsWithoutText() 
	{
		//    Given I have a Event with no text value for the managed object
		iHaveAEventWithNoText();
		//    When I create all Events
		iCreateAll();
		//    Then Event response should be unprocessable
		shouldBeBadRequest();
	}
}
}