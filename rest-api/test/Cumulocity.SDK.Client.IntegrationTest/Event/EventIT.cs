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

		public object Locker { get; set; }

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
		//@Given("I have '(\\d+)' Events for the source '(\\d+)' the managed object$")
		public void iHaveEventsForSource(int n, int index) { 
			for (int i = 0; i<n; i++) {
				var rep = new EventRepresentation();
				rep.Type = "type";
				rep.DateTime = DateTime.UtcNow;
				rep.Text = " Event of Managed Object : " + i;
				rep.Source = managedObject;
				Input.Add(rep);
			}
		}

		//@Given("I have a Event with time '([^']*)' with type '([^']*)' and for '(\\d+)' managed object$")
		public void iHaveAEventWithTypeAndTime(String time, String type, int index) 
			{
				DateTime dateTime = DateTime.ParseExact(time, "o",
					System.Globalization.CultureInfo.InvariantCulture);
			EventRepresentation rep = new EventRepresentation();
			rep.Type =type ;
			rep.Text = " Event of Managed Object : ";
			rep.DateTime = dateTime;   //(DateConverter.string2Date(time));
			rep.Source =managedObject;
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
					//lock(Locker)
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
		//@When("I query all Events by type '([^']*)'$")
		public void iQueryAllByType(String type) {
			try {
				EventFilter filter = new EventFilter().byType(type);
				collection = EventApi.getEventsByFilter(filter).get();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}
		//@When("I query all Events by source '(\\d+)'$")
		public void iQueryAllBySource(int index)
		{
			try {
			ManagedObjectRepresentation mo = managedObject;
			EventFilter filter = new EventFilter().bySource(mo);
			collection = EventApi.getEventsByFilter(filter).get();
		}
		catch (SDKException ex)
		{
			Status = ex.HttpStatus;
		}
    }
		//@When("I query all Events by source '(\\d+)' and type '([^']*)'$")

		public void iQueryAllBySourceAndType(int index, String type)
		{
			try {
				ManagedObjectRepresentation mo = managedObject;
				EventFilter filter = new EventFilter().bySource(mo).byType(type);
				collection = EventApi.getEventsByFilter(filter).get();
			} catch (SDKException ex) {
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all Events by source '(\\d+)' and time '([^']*)'$")
		public void iQueryAllBySourceAndTime(int index, String time)
		{
			try {
				DateTime dateTime = DateTime.ParseExact(time, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				ManagedObjectRepresentation mo = managedObject;
				EventFilter filter = new EventFilter().byDate(dateTime, dateTime);
				collection = EventApi.getEventsByFilter(filter).get();
			} catch (SDKException ex) {
				Status = ex.HttpStatus;
			}
		}
		//@When("I get the Events with the created id$")
		public void iGetEventsWithCreatedId() 
		{
			try {
				Result1.Add(EventApi.getEvent(Result[0].Id));
			} catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I delete the Events with the created id$")
		public void iDeleteEventsWithCreatedId() 
		{
		        try {
						EventApi.delete(Result[0]);
					} catch (SDKException ex) {
		            Status = ex.HttpStatus;
		        }
		}

		//@When("I delete all Event collection$")
		public void iDeleteEventCollection() 
		{
		        try {
			        EventApi.deleteEventsByFilter(new EventFilter());
				} catch (SDKException ex) {
			        Status = ex.HttpStatus;
				}
		}

		//@When("I delete all Events by type '([^']*)'$")
		public void iDeleteAllByType(String type) 
		{
		        try {
				EventFilter typeFilter = new EventFilter().byType(type);
			    EventApi.deleteEventsByFilter(typeFilter);
			} catch (SDKException ex) {
			        Status = ex.HttpStatus;
			}
		}

		//@When("I query all Events by page '(\\d+)'$")
		public void iQueryAllByPageNumber(int pageNumber) 
		{
		    try {
				collection1 = EventApi.getEvents().getPage(collection, pageNumber);
			} catch (SDKException ex) {
			    Status = ex.HttpStatus;
			}
		}

//@When("I should get next page which has current page '(\\d+)' and events '(\\d+)'$")
	public void iQueryAllByNextPage(int currentPage, int numEvents) 
{
        try {
		EventCollectionRepresentation collectionRepresentation = EventApi.getEvents().getNextPage(collection1);
	    Assert.Equal(collectionRepresentation.PageStatistics.CurrentPage, currentPage);
		Assert.Equal(collectionRepresentation.Events.Count, numEvents);
	} catch (SDKException ex) {
	        Status = ex.HttpStatus;
	}

}

//@When("I should get previous page which has current page '(\\d+)' and events '(\\d+)'$")

	public void iQueryAllByPreviousPage(int currentPage, int numEvents) 
	{
	    try {
			EventCollectionRepresentation collectionRepresentation = EventApi.getEvents().getPreviousPage(collection1);
		    Assert.Equal(currentPage, collectionRepresentation.PageStatistics.CurrentPage);
		    Assert.Equal(numEvents, collectionRepresentation.Events.Count);
		} catch (SDKException ex){
			Status = ex.HttpStatus;
		}

	}

// ------------------------------------------------------------------------
// Then
// ------------------------------------------------------------------------
public void iShouldGetNumberOfEvents(int count)
		{
			Assert.Equal(count, collection.Events.Count);
		}
		//@Then("Event response should be unprocessable$")
		public void shouldBeBadRequest()
		{
			Assert.Equal(422, Status);
		}

		//@Then("I should get all the Events$")
		public void shouldGetAllEvents()
		{
			Assert.Equal(collection.Events.Count, Result.Count);
		}

		//@Then("I should get the Events$")

		public void shouldGetTheEvent()
		{
			Assert.Equal(Result1[0], Result1[0]);
		}

		//@Then("Events should not be found$")

		public void shouldNotBeFound()
		{
			Assert.Equal(Status, 404);
		}
		//@Then("I should get '(\\d+)' Events of paging$")
		public void iShouldGetNumberOfEventsOfPaging(int count)
		{
			Assert.Equal(collection1.Events.Count, count);
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

		//
		//    Scenario: Get Event collection
		[Fact]
		public void getEventCollection() 
		{
			//    Given I have '2' Events of type 'type1' for the managed object
			iHaveEvents(2, "type1");
			//    When I create all Events
			iCreateAll();
			//    And I get all Events
			iGetAllEvents();
			//    Then I should get all the Events
			shouldGetAllEvents();
	}

		//
		//
		//    Scenario: Get event collection by type

		[Fact]
		public void getEventCollectionByType()
		{
		//    Given I have '2' Events of type 'type' for the managed object
		iHaveEvents(2, "type");
		//    And I have '3' Events of type 'type1' for the managed object
		iHaveEvents(3, "type1");
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '5' Events
		iShouldGetNumberOfEvents(5);
		//    And I query all Events by type 'type'
		iQueryAllByType("type");
		//    Then I should get '2' Events
		iShouldGetNumberOfEvents(2);
		//    And I query all Events by type 'type1'
		iQueryAllByType("type1");
		//    Then I should get '3' Events
		iShouldGetNumberOfEvents(3);
		//    And I query all Events by type 'type2'
		iQueryAllByType("type2");
		//    Then I should get '0' Events
		iShouldGetNumberOfEvents(0);
		}

		//
		//
		//    Scenario: Get event collection by source

		[Fact]
		public void getEventCollectionBySource()
		{
		//    Given I have '3' Events for the source '0' the managed object
		iHaveEventsForSource(3, 0);
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '3' Events
		iShouldGetNumberOfEvents(3);
		//    And I query all Events by source '0'
		iQueryAllBySource(0);
		//    Then I should get '3' Events
		iShouldGetNumberOfEvents(3);

		}


		//
		//    Scenario: Get event collection by source and type

		[Fact]
		public void getEventCollectionbySourceAndType()
		{ 
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		iHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
        //    And I have a Event with time '2011-11-03T11:05:00.000+05:30' with type 'type1' and for '0' managed object
        iHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type1", 0);
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '2' Events
		iShouldGetNumberOfEvents(2);
		//    And I query all Events by source '0' and type 'type'
		iQueryAllBySourceAndType(0, "type");
		//    Then I should get '1' Events
		iShouldGetNumberOfEvents(1);
		//    And I query all Events by source '0' and type 'type1'
		iQueryAllBySourceAndType(0, "type1");
		//    Then I should get '1' Events
		iShouldGetNumberOfEvents(1);
		//    And I query all Events by source '0' and type 'type2'
		iQueryAllBySourceAndType(0, "type2");
		//    Then I should get '0' Events
		iShouldGetNumberOfEvents(0);
	}


		[Fact]
		public void getEventCollectionbyTime() 
	{
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		iHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
		//    And I have a Event with time '2011-11-03T11:05:00.000+05:30' with type 'type1' and for '0' managed object
		iHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type1", 0);
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '2' Events
		iShouldGetNumberOfEvents(2);
		//    And I query all Events by source '0' and type 'type'
		iQueryAllBySourceAndTime(0, "2018-12-17T10:01:14.9072393Z");
		//    Then I should get '1' Events
		iShouldGetNumberOfEvents(1);

	}

		//
		//    Scenario: Get Event

		[Fact]
		public void getEvent() 
	{
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		iHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
		//    When I create all Events
		iCreateAll();
		//    And I get the Events with the created id
		iGetEventsWithCreatedId();
		//    Then I should get the Events
		shouldGetTheEvent();
	}

		//
		//    Scenario: Delete Event

		[Fact]
		public void deleteEvent() 
	{
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		iHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
		//    When I create all Events
		iCreateAll();
		//    And I delete the Events with the created id
		iDeleteEventsWithCreatedId();
		//    And I get the Events with the created id
		iGetEventsWithCreatedId();
		//    Then Events should not be found
		shouldNotBeFound();
	}

		//
		//    Scenario: Delete all Event collection by an empty filter

		[Fact]
		public void deleteEventCollectionByEmptyFilter() 
	{
		//    Given I have '3' Events of type 'type1' for the managed object
		iHaveEvents(3, "type1");
		//    And I have '2' Events of type 'type2' for the managed object
		iHaveEvents(2, "type2");
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '5' Events
		iShouldGetNumberOfEvents(5);
		//    And I delete all Event collection
		iDeleteEventCollection();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '0' Events
		iShouldGetNumberOfEvents(0);
	}

		//
		//    Scenario: Delete Events by filter

		[Fact]
		public void deleteEventsByTypeFilter()
	{
		//    Given I have '3' Events of type 'type1' for the managed object
		iHaveEvents(3, "type1");
		//    And I have '2' Events of type 'type2' for the managed object
		iHaveEvents(2, "type2");
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '5' Events
		iShouldGetNumberOfEvents(5);
		//    And I delete all Events by type 'type2'
		iDeleteAllByType("type2");
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '3' Events
		iShouldGetNumberOfEvents(3);
		//    And I query all Events by type 'type1'
		iQueryAllByType("type1");
		//    Then I should get '3' Events
		iShouldGetNumberOfEvents(3);
		//    And I query all Events by type 'type2'
		iQueryAllByType("type2");
		//    Then I should get '0' Events
		iShouldGetNumberOfEvents(0);
	}

		//
		//    Scenario: Get event collection by paging

		[Fact]
		public void getEventCollectionByPaging() 
	{
		//    Given I have '17' Events for the source '0' the managed object
		iHaveEventsForSource(17, 0);
		//    When I create all Events
		iCreateAll();
		//    And I get all Events
		iGetAllEvents();
		//    Then I should get '5' Events
		iShouldGetNumberOfEvents(5);
		//    And I query all Events by page '1'
		iQueryAllByPageNumber(1);
		//    Then I should get '5' Events of paging
		iShouldGetNumberOfEventsOfPaging(5);
		//    And I query all Events by page '2'
		iQueryAllByPageNumber(2);
		//    Then I should get '5' Events of paging
		iShouldGetNumberOfEventsOfPaging(5);
		//    And I query all Events by page '3'
		iQueryAllByPageNumber(3);
		//    Then I should get '5' Events of paging
		iShouldGetNumberOfEventsOfPaging(5);
		//      And I query all Events by page '4'
		iQueryAllByPageNumber(4);
		//    Then I should get '2' Events of paging
		iShouldGetNumberOfEventsOfPaging(2);
		//    And I query all Events by page '5'
		iQueryAllByPageNumber(5);
		//    Then I should get '0' Events of paging
		iShouldGetNumberOfEventsOfPaging(0);
		//    Then I should get previous page which has current page '4' and events '2'
	}
}
}