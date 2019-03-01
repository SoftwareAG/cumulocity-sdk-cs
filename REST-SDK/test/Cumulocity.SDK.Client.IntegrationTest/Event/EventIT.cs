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
#pragma warning disable xUnit1013
#pragma warning disable 0612
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
			managedObject = fixture.platform.InventoryApi.Create(mo);
		}

		public void Dispose()
		{
			IList<EventRepresentation> eventsOn1stPage = getEventsFrom1stPage();
			while (eventsOn1stPage.Count > 0)
			{
				deleteMOs(eventsOn1stPage);
				eventsOn1stPage = getEventsFrom1stPage();
			}
		}
		private void deleteMOs(IList<EventRepresentation> mosOn1stPage)
		{
			foreach (EventRepresentation e in mosOn1stPage)
			{
				EventApi.Delete(e);
			}
		}

		private IList<EventRepresentation> getEventsFrom1stPage()
		{
			return EventApi.Events.GetFirstPage().Events;
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

		public void IHaveAEventWithNoType()
		{
			EventRepresentation rep = new EventRepresentation();
			rep.DateTime = DateTime.UtcNow;
			rep.Text = " Event of Managed Object : 1";
			rep.Source = managedObject;
			Input.Add(rep);
		}
		//@Given("I have a Event with no text value for the managed object$")
		public void IHaveAEventWithNoText()
		{
			EventRepresentation rep = new EventRepresentation();
			rep.Type = "type";
			rep.DateTime = DateTime.UtcNow;
			rep.Source = managedObject;
			Input.Add(rep);
		}

		//@Given("I have a Event with no time value for the managed object$")
		public void IHaveAEventWithNoTime()
		{
			EventRepresentation rep = new EventRepresentation();
			rep.Type = "type";
			rep.Text = " Event of Managed Object : 1";
			rep.Source = managedObject;
			Input.Add(rep);
		}
		//@Given("I have '(\\d+)' Events for the source '(\\d+)' the managed object$")
		public void IHaveEventsForSource(int n, int index) { 
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
		public void IHaveAEventWithTypeAndTime(String time, String type, int index) 
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

	//@When("I Create all Events$")
	public void ICreateAll()
		{
			try
			{
				foreach (var rep in Input)
				{
					var evt = EventApi.Create(rep);
					//lock(Locker)
						Result.Add(evt);
				}
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I GetFirstPage all Events$")
		public void IGetAllEvents()
		{
			try
			{
				collection = EventApi.Events.GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}
		//@When("I query all Events by type '([^']*)'$")
		public void IQueryAllByType(String type) {
			try {
				EventFilter filter = new EventFilter().ByType(type);
				collection = EventApi.GetEventsByFilter(filter).GetFirstPage();
			}
			catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}
		//@When("I query all Events by source '(\\d+)'$")
		public void IQueryAllBySource(int index)
		{
			try {
			ManagedObjectRepresentation mo = managedObject;
			EventFilter filter = new EventFilter().BySource(mo);
			collection = EventApi.GetEventsByFilter(filter).GetFirstPage();
		}
		catch (SDKException ex)
		{
			Status = ex.HttpStatus;
		}
    }
		//@When("I query all Events by source '(\\d+)' and type '([^']*)'$")

		public void IQueryAllBySourceAndType(int index, String type)
		{
			try {
				ManagedObjectRepresentation mo = managedObject;
				EventFilter filter = new EventFilter().BySource(mo).ByType(type);
				collection = EventApi.GetEventsByFilter(filter).GetFirstPage();
			} catch (SDKException ex) {
				Status = ex.HttpStatus;
			}
		}

		//@When("I query all Events by source '(\\d+)' and time '([^']*)'$")
		public void IQueryAllBySourceAndTime(int index, String time)
		{
			try {
				DateTime dateTime = DateTime.ParseExact(time, "o",
					System.Globalization.CultureInfo.InvariantCulture);
				ManagedObjectRepresentation mo = managedObject;
				EventFilter filter = new EventFilter().byDate(dateTime, dateTime).ByType("type");
				collection = EventApi.GetEventsByFilter(filter).GetFirstPage();
			} catch (SDKException ex) {
				Status = ex.HttpStatus;
			}
		}
		//@When("I GetFirstPage the Events with the created id$")
		public void IGetEventsWithCreatedId() 
		{
			try {
				Result1.Add(EventApi.GetEvent(Result[0].Id));
			} catch (SDKException ex)
			{
				Status = ex.HttpStatus;
			}
		}

		//@When("I Delete the Events with the created id$")
		public void IDeleteEventsWithCreatedId() 
		{
		        try {
						EventApi.Delete(Result[0]);
					} catch (SDKException ex) {
		            Status = ex.HttpStatus;
		        }
		}

		//@When("I Delete all Event collection$")
		public void IDeleteEventCollection() 
		{
		        try {
			        EventApi.DeleteEventsByFilter(new EventFilter());
				} catch (SDKException ex) {
			        Status = ex.HttpStatus;
				}
		}

		//@When("I Delete all Events by type '([^']*)'$")
		public void IDeleteAllByType(String type) 
		{
		        try {
				EventFilter typeFilter = new EventFilter().ByType(type);
			    EventApi.DeleteEventsByFilter(typeFilter);
			} catch (SDKException ex) {
			        Status = ex.HttpStatus;
			}
		}

		//@When("I query all Events by page '(\\d+)'$")
		public void IQueryAllByPageNumber(int pageNumber) 
		{
		    try {
				collection1 = EventApi.Events.GetPage(collection, pageNumber);
			} catch (SDKException ex) {
			    Status = ex.HttpStatus;
			}
		}

//@When("I should GetFirstPage next page which has current page '(\\d+)' and events '(\\d+)'$")
	public void IQueryAllByNextPage(int currentPage, int numEvents) 
{
        try {
		EventCollectionRepresentation collectionRepresentation = EventApi.Events.GetNextPage(collection1);
	    Assert.Equal(collectionRepresentation.PageStatistics.CurrentPage, currentPage);
		Assert.Equal(collectionRepresentation.Events.Count, numEvents);
	} catch (SDKException ex) {
	        Status = ex.HttpStatus;
	}

}

//@When("I should GetFirstPage previous page which has current page '(\\d+)' and events '(\\d+)'$")

	public void IQueryAllByPreviousPage(int currentPage, int numEvents) 
	{
	    try {
			EventCollectionRepresentation collectionRepresentation = EventApi.Events.GetPreviousPage(collection1);
		    Assert.Equal(currentPage, collectionRepresentation.PageStatistics.CurrentPage);
		    Assert.Equal(numEvents, collectionRepresentation.Events.Count);
		} catch (SDKException ex){
			Status = ex.HttpStatus;
		}

	}

// ------------------------------------------------------------------------
// Then
// ------------------------------------------------------------------------
public void IShouldGetNumberOfEvents(int count)
		{
			Assert.Equal(count, collection.Events.Count);
		}
		//@Then("Event response should be unprocessable$")
		public void ShouldBeBadRequest()
		{
			Assert.Equal(422, Status);
		}

		//@Then("I should GetFirstPage all the Events$")
		public void ShouldGetAllEvents()
		{
			Assert.Equal(collection.Events.Count, Result.Count);
		}

		//@Then("I should GetFirstPage the Events$")

		public void ShouldGetTheEvent()
		{
			Assert.Equal(Result1[0], Result1[0]);
		}

		//@Then("Events should not be found$")

		public void ShouldNotBeFound()
		{
			Assert.Equal( 404, Status);
		}
		//@Then("I should GetFirstPage '(\\d+)' Events of paging$")
		public void IShouldGetNumberOfEventsOfPaging(int count)
		{
			Assert.Equal(collection1.Events.Count, count);
		}
		// ------------------------------------------------------------------------
		// Test
		// ------------------------------------------------------------------------

		//    Scenario: Create Events
		[Fact]
		public void CreateEvents()
		{
			//    Given I have '2' Events of type 'type1' for the managed object
			iHaveEvents(2, "type1");
			//    When I Create all Events
			ICreateAll();
			//    And I GetFirstPage all Events
			IGetAllEvents();
			//    Then I should GetFirstPage '2' Events
			IShouldGetNumberOfEvents(2);
		}

		//    Scenario: Create Events without type
		[Fact]
		public void CreateEventsWithoutType() 
		{
			//    Given I have a Event with no type value for the managed object
			IHaveAEventWithNoType();
			//    When I Create all Events
			ICreateAll();
			//    Then Event response should be unprocessable
			ShouldBeBadRequest();
		}

		//
		//
		//    Scenario: Create Events without time
		[Fact]
		public void CreateEventsWithoutTime() 
		{
			//    Given I have a Event with no time value for the managed object
			IHaveAEventWithNoTime();
			//    When I Create all Events
			ICreateAll();
			//    Then Event response should be unprocessable
			ShouldBeBadRequest();
		}

	//
	//    Scenario: Create Events without text
	[Fact]
	public void CreateEventsWithoutText() 
	{
		//    Given I have a Event with no text value for the managed object
		IHaveAEventWithNoText();
		//    When I Create all Events
		ICreateAll();
		//    Then Event response should be unprocessable
		ShouldBeBadRequest();
	}

		//
		//    Scenario: Get Event collection
		[Fact]
		public void GetEventCollection() 
		{
			//    Given I have '2' Events of type 'type1' for the managed object
			iHaveEvents(2, "type1");
			//    When I Create all Events
			ICreateAll();
			//    And I GetFirstPage all Events
			IGetAllEvents();
			//    Then I should GetFirstPage all the Events
			ShouldGetAllEvents();
	}

		//
		//
		//    Scenario: Get event collection by type

		[Fact]
		public void GetEventCollectionByType()
		{
		//    Given I have '2' Events of type 'type' for the managed object
		iHaveEvents(2, "type");
		//    And I have '3' Events of type 'type1' for the managed object
		iHaveEvents(3, "type1");
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '5' Events
		IShouldGetNumberOfEvents(5);
		//    And I query all Events by type 'type'
		IQueryAllByType("type");
		//    Then I should GetFirstPage '2' Events
		IShouldGetNumberOfEvents(2);
		//    And I query all Events by type 'type1'
		IQueryAllByType("type1");
		//    Then I should GetFirstPage '3' Events
		IShouldGetNumberOfEvents(3);
		//    And I query all Events by type 'type2'
		IQueryAllByType("type2");
		//    Then I should GetFirstPage '0' Events
		IShouldGetNumberOfEvents(0);
		}

		//
		//
		//    Scenario: Get event collection by source

		[Fact]
		public void GetEventCollectionBySource()
		{
		//    Given I have '3' Events for the source '0' the managed object
		IHaveEventsForSource(3, 0);
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '3' Events
		IShouldGetNumberOfEvents(3);
		//    And I query all Events by source '0'
		IQueryAllBySource(0);
		//    Then I should GetFirstPage '3' Events
		IShouldGetNumberOfEvents(3);

		}


		//
		//    Scenario: Get event collection by source and type

		[Fact]
		public void GetEventCollectionbySourceAndType()
		{ 
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		IHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
        //    And I have a Event with time '2011-11-03T11:05:00.000+05:30' with type 'type1' and for '0' managed object
        IHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type1", 0);
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '2' Events
		IShouldGetNumberOfEvents(2);
		//    And I query all Events by source '0' and type 'type'
		IQueryAllBySourceAndType(0, "type");
		//    Then I should GetFirstPage '1' Events
		IShouldGetNumberOfEvents(1);
		//    And I query all Events by source '0' and type 'type1'
		IQueryAllBySourceAndType(0, "type1");
		//    Then I should GetFirstPage '1' Events
		IShouldGetNumberOfEvents(1);
		//    And I query all Events by source '0' and type 'type2'
		IQueryAllBySourceAndType(0, "type2");
		//    Then I should GetFirstPage '0' Events
		IShouldGetNumberOfEvents(0);
	}


		[Fact]
		public void GetEventCollectionbyTime() 
	{
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		IHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
		//    And I have a Event with time '2011-11-03T11:05:00.000+05:30' with type 'type1' and for '0' managed object
		IHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type1", 0);
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '2' Events
		IShouldGetNumberOfEvents(2);
		//    And I query all Events by source '0' and type 'type'
		IQueryAllBySourceAndTime(0, "2018-12-17T10:01:14.9072393Z");
		//    Then I should GetFirstPage '1' Events
		IShouldGetNumberOfEvents(1);

	}

		//
		//    Scenario: Get Event

		[Fact]
		public void GetEvent() 
	{
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		IHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage the Events with the created id
		IGetEventsWithCreatedId();
		//    Then I should GetFirstPage the Events
		ShouldGetTheEvent();
	}

		//
		//    Scenario: Delete Event

		[Fact]
		public void DeleteEvent() 
	{
		//    Given I have a Event with time '2011-11-03T11:01:00.000+05:30' with type 'type' and for '0' managed object
		IHaveAEventWithTypeAndTime("2018-12-17T10:01:14.9072393Z", "type", 0);
		//    When I Create all Events
		ICreateAll();
		//    And I Delete the Events with the created id
		IDeleteEventsWithCreatedId();
		//    And I GetFirstPage the Events with the created id
		IGetEventsWithCreatedId();
		//    Then Events should not be found
		ShouldNotBeFound();
	}

		//
		//    Scenario: Delete all Event collection by an empty filter

		[Fact]
		public void DeleteEventCollectionByEmptyFilter() 
	{
		//    Given I have '3' Events of type 'type1' for the managed object
		iHaveEvents(3, "type1");
		//    And I have '2' Events of type 'type2' for the managed object
		iHaveEvents(2, "type2");
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '5' Events
		IShouldGetNumberOfEvents(5);
		//    And I Delete all Event collection
		IDeleteEventCollection();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '0' Events
		IShouldGetNumberOfEvents(0);
	}

		//
		//    Scenario: Delete Events by filter

		[Fact]
		public void DeleteEventsByTypeFilter()
	{
		//    Given I have '3' Events of type 'type1' for the managed object
		iHaveEvents(3, "type1");
		//    And I have '2' Events of type 'type2' for the managed object
		iHaveEvents(2, "type2");
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '5' Events
		IShouldGetNumberOfEvents(5);
		//    And I Delete all Events by type 'type2'
		IDeleteAllByType("type2");
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '3' Events
		IShouldGetNumberOfEvents(3);
		//    And I query all Events by type 'type1'
		IQueryAllByType("type1");
		//    Then I should GetFirstPage '3' Events
		IShouldGetNumberOfEvents(3);
		//    And I query all Events by type 'type2'
		IQueryAllByType("type2");
		//    Then I should GetFirstPage '0' Events
		IShouldGetNumberOfEvents(0);
	}

		//
		//    Scenario: Get event collection by paging

		[Fact]
		public void GetEventCollectionByPaging() 
	{
		//    Given I have '17' Events for the source '0' the managed object
		IHaveEventsForSource(17, 0);
		//    When I Create all Events
		ICreateAll();
		//    And I GetFirstPage all Events
		IGetAllEvents();
		//    Then I should GetFirstPage '5' Events
		IShouldGetNumberOfEvents(5);
		//    And I query all Events by page '1'
		IQueryAllByPageNumber(1);
		//    Then I should GetFirstPage '5' Events of paging
		IShouldGetNumberOfEventsOfPaging(5);
		//    And I query all Events by page '2'
		IQueryAllByPageNumber(2);
		//    Then I should GetFirstPage '5' Events of paging
		IShouldGetNumberOfEventsOfPaging(5);
		//    And I query all Events by page '3'
		IQueryAllByPageNumber(3);
		//    Then I should GetFirstPage '5' Events of paging
		IShouldGetNumberOfEventsOfPaging(5);
		//      And I query all Events by page '4'
		IQueryAllByPageNumber(4);
		//    Then I should GetFirstPage '2' Events of paging
		IShouldGetNumberOfEventsOfPaging(2);
		//    And I query all Events by page '5'
		IQueryAllByPageNumber(5);
		//    Then I should GetFirstPage '0' Events of paging
		IShouldGetNumberOfEventsOfPaging(0);
		//    Then I should GetFirstPage previous page which has current page '4' and events '2'
	}
}
}