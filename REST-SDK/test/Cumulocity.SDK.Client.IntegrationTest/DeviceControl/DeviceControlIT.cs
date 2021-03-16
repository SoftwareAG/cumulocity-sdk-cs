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

using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Notification;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.API.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Polling;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using Cumulocity.SDK.Client.UnitTest.Devicecontrol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.DeviceControl
{
#pragma warning disable xUnit1013
#pragma warning disable 0612
	public class DeviceControlIT : IClassFixture<DeviceControlFixture>, IDisposable
	{
		private readonly IDeviceControlApi deviceControlResource;
		private readonly IInventoryApi inventoryApi;
		private readonly List<ManagedObjectRepresentation> managedObjects = new List<ManagedObjectRepresentation>();
		private readonly DeviceControlFixture fixture;
		private OperationNotificationSubscriber subscriber;
		private OperationRepresentation operation1;
		private SimpleOperationProcessor operationProcessor = new SimpleOperationProcessor();
		private FixedRatePoller poller = null;
		private OperationCollectionRepresentation allOperations;

		public DeviceControlIT(DeviceControlFixture fixture)
		{
			this.fixture = fixture;
			this.inventoryApi = fixture.platform.InventoryApi;
			CreateManagedObjects();
			deviceControlResource = fixture.platform.DeviceControlApi;
			operation1 = null;
			allOperations = null;
		}

		public void Dispose()
		{
			if (poller != null)
			{
				poller.Stop();
			}
			if (subscriber != null)
			{
				subscriber.Disconnect();
				subscriber = null;
			}
		}

		public void CreateManagedObjects()
		{
			ManagedObjectRepresentation agent = aSampleMo().WithName("Agent").WithType("com.type").With(new Agent())
				.build();
			ManagedObjectRepresentation device = aSampleMo().WithName("Device").WithType("com.type")
				.build();
			ManagedObjectRepresentation agent2 = aSampleMo().WithName("Agent2").WithType("com.type").With(new Agent())
				.build();
			ManagedObjectRepresentation device2 = aSampleMo().WithName("Device2").WithType("com.type")
				.build();

			agent = inventoryApi.Create(agent);
			device = inventoryApi.Create(device);
			agent2 = inventoryApi.Create(agent2);
			device2 = inventoryApi.Create(device2);

			managedObjects.Add(agent);
			managedObjects.Add(device);
			managedObjects.Add(agent2);
			managedObjects.Add(device2);

			addChildDevice(agent, device);
			addChildDevice(agent2, device2);
		}

		private void addChildDevice(ManagedObjectRepresentation parent, ManagedObjectRepresentation child)
		{
			ManagedObjectReferenceRepresentation deviceRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).WithMo(child).Build();
			inventoryApi.GetManagedObject(parent.Id).AddChildDevice(deviceRef);
		}

		private static ManagedObjectRepresentationBuilder aSampleMo()
		{
			return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
				.MO_REPRESENTATION);
		}

		//    Scenario: Create Operation and poll it
		[Fact]
		public void CreateOperationAndPollIt()
		{
			//    Given I have a poller for agent '0'
			IHaveAPollerForAgent(0);
			//    When I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
			//    Then poller should receive operation
			PollerShouldRecieveOperation();
		}

		//
		//    Scenario: adding operations to queue
		[Fact]
		public void AddingOperationToQueue()
		{
			//    When I GetFirstPage all operations for device '1'
			IGetAllOperationsForAgent(1);
			//    Then I should receive '0' operations
			IShouldReceiveXOperations(0);
			//    When I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
			//    And I GetFirstPage all operations for device '1'
			IGetAllOperationsForDeviceX(1);
			//    Then I should receive '1' operations
			IShouldReceiveXOperations(1);
		}

		//  Scenario: GetFirstPage notification about new operation
		[Fact]
		public void GetNotificationAboutNewOperation()
		{
			//      Given I have a operation subscriber for agent '0'
			IHaveAOperationSubscriberForAgent(0);
			//    When I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
			//    Then subscriber should receive operation
			SubscriberShouldReceiveOperation();
		}

		//
		//    Scenario: Operation CRUD

		[Fact]
		public void OperationCRUD()
		{
			//    When I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
			//    And I call GetFirstPage on created operation
			ICallGetOnCreatedOperation();
			//    Then I should receive operation with status 'PENDING'
			IShouldReceiveOperationWithStatusX("PENDING");
			//    When I Update created operation with status 'EXECUTING'
			IUpdateCreatedOperationWithStatusX("EXECUTING");
			//    And I call GetFirstPage on created operation
			ICallGetOnCreatedOperation();
			//    Then I should receive operation with status 'EXECUTING'
			IShouldReceiveOperationWithStatusX("EXECUTING");
		}

		//
		//    Scenario: query operations by status
		[Fact]
		public void QueryOperationByStatus()
		{
			IQueryOperationsWithStatusX("EXECUTING");
			int numOfExecuting = allOperations.Operations.Count;
			IQueryOperationsWithStatusX("PENDING");
			int numOfPending = allOperations.Operations.Count;
			//    When I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
			//    And I Update created operation with status 'EXECUTING'
			IUpdateCreatedOperationWithStatusX("EXECUTING");
			//    And I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
			//    When I query operations with status 'PENDING'
			IQueryOperationsWithStatusX("PENDING");
			//    Then I should receive '1' operations
			IShouldReceiveXOperations(numOfPending + 1);
			//    And all received operations should have status 'PENDING'
			AllRecievedOperationsShouldHaveStatusX("PENDING");
			//    When I query operations with status 'EXECUTING'
			IQueryOperationsWithStatusX("EXECUTING");
			//    Then I should receive '1' operations
			IShouldReceiveXOperations(numOfExecuting + 1);
			//    And all received operations should have status 'EXECUTING'
			AllRecievedOperationsShouldHaveStatusX("EXECUTING");
		}

		//
		//    Scenario: query operations by device

		[Fact]
		public void QueryOperationsByDevice() 
		{
			//    And I Create an operation for device '1'
			ICreateAnOperationForDevice(1);
		//    And I Create an operation for device '1'
		ICreateAnOperationForDevice(1);
		//    And I Create an operation for device '3'
		ICreateAnOperationForDevice(3);
		//    When I GetFirstPage all operations for device '1'
		IGetAllOperationsForDeviceX(1);
		//    Then I should receive '2' operations
		IShouldReceiveXOperations(2);
		//    When I GetFirstPage all operations for device '3'
		IGetAllOperationsForDeviceX(3);
		//    Then I should receive '1' operations
		IShouldReceiveXOperations(1);
	}

		//
		//    Scenario: query operations by agent

		[Fact]
		public void QueryOperationsByAgent()
	{
	//    And I Create an operation for device '1'
	ICreateAnOperationForDevice(1);
	//    And I Create an operation for device '1'
	ICreateAnOperationForDevice(1);
	//    And I Create an operation for device '3'
	ICreateAnOperationForDevice(3);
	//    When I GetFirstPage all operations for agent '0'
	IGetAllOperationsForAgent(0);
	//    Then I should receive '2' operations
	IShouldReceiveXOperations(2);
	//    When I GetFirstPage all operations for agent '2'
	IGetAllOperationsForAgent(2);
	//    Then I should receive '1' operations
	IShouldReceiveXOperations(1);
	}

	[Fact]
	public void OperationRepresentationValidation()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("UnRegisteredKey", "DummyValue");
		OperationRepresentation rep = Helpers.GetObject<OperationRepresentation>(dictionary);
		// Any unregistered attribute of OperationRepresentation will not be added to the object.
		Assert.True(rep.ToString().Equals("{}"));

		dictionary.Add("Description", "This is the description");
		rep = Helpers.GetObject<OperationRepresentation>(dictionary);
		// Registered attributes of OperationRepresentation will be added to the object.
		Assert.Contains("Description", rep.ToString());
	}

		//
		//Given
		//

		//@Given("^I have a poller for agent '([^']*)'$")

		public void IHaveAPollerForAgent(int arg1)
		{
			GId agentId = getMoId(arg1);
			poller = new OperationsByAgentAndStatusPollerImpl(deviceControlResource, agentId.Value, OperationStatus.PENDING, operationProcessor);
			poller.Start();
		}

		private GId getMoId(int arg1)
		{
			GId deviceId = managedObjects[arg1].Id;
			return deviceId;
		}

		//@Given("^I have a operation subscriber for agent '([^']*)'$")
		public void IHaveAOperationSubscriberForAgent(int arg1)
		{
			GId agentId = getMoId(arg1);
			subscriber = new OperationNotificationSubscriber(fixture.platform);

			subscriber.Subscribe(agentId, new Handler(operationProcessor));

			try
			{
				Thread.Sleep(10000);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		//
		//When
		//

		//@When("^I Create an operation for device '([^']*)'$")
		public void ICreateAnOperationForDevice(int deviceNum)
		{
			GId deviceId = getMoId(deviceNum);
			OperationRepresentation operationRepresentation = new OperationRepresentation();
			operationRepresentation.DeviceId = deviceId;
			operationRepresentation.Set("smaple_value", "sample_operation_type");
			operation1 = deviceControlResource.Create(operationRepresentation);
		}

		//@When("^I GetFirstPage all operations for agent '([^']*)'$")

		public void IGetAllOperationsForAgent(int agentNum)
		{
			OperationFilter filter = new OperationFilter().ByAgent(getMoId(agentNum).Value);
			allOperations = deviceControlResource.GetOperationsByFilter(filter).GetFirstPage();
		}

		//@When("^I GetFirstPage all operations for device '([^']*)'$")
		public void IGetAllOperationsForDeviceX(int deviceNum)
		{
			OperationFilter filter = new OperationFilter().ByDevice(getMoId(deviceNum).Value);
			allOperations = deviceControlResource.GetOperationsByFilter(filter).GetFirstPage();
		}

		//@When("^I call GetFirstPage on created operation$")
		public void ICallGetOnCreatedOperation()
		{
			GId operationId = operation1.Id;
			operation1 = deviceControlResource.GetOperation(operationId);
		}

		//@When("^I Update created operation with status '([^']*)'$")
		public void IUpdateCreatedOperationWithStatusX(String status)
		{
			operation1.Status = status;
			operation1 = deviceControlResource.Update(operation1);
		}

		//@When("^I query operations with status '([^']*)'$")
		public void IQueryOperationsWithStatusX(String status)
		{
			OperationFilter filter = new OperationFilter().ByStatus(OperationStatus.valueOf(status));
			allOperations = deviceControlResource.GetOperationsByFilter(filter).GetFirstPage();
		}

		//
		//Then
		//
		//@Then("^poller should receive operation$")

		public void PollerShouldRecieveOperation()
		{
			try
			{
				Thread.Sleep(11000);
				Assert.Equal(1, operationProcessor.Operations.Count);
			}
			catch (ThreadInterruptedException e)
			{
				Debug.WriteLine(e.StackTrace.ToString());
			}
		}

		//@Then("^I should receive '([^']*)' operations$")
		public void IShouldReceiveXOperations(int amount)
		{
			Assert.Equal(amount, allOperations.Operations.Count);
		}

		//@Then("^subscriber should receive operation$")
		public void SubscriberShouldReceiveOperation()
		{
			Thread.Sleep(11000);
			Assert.Equal(1, operationProcessor.Operations.Count);

			try
			{

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		//@Then("^I should receive operation with status '([^']*)'$")
		public void IShouldReceiveOperationWithStatusX(String status)
		{
			Assert.Equal(status, operation1.Status);
		}

		//@Then("^all received operations should have status '([^']*)'$")

		public void AllRecievedOperationsShouldHaveStatusX(String status)
		{
			foreach (OperationRepresentation operation in allOperations.Operations)
			{
				Assert.Equal(status, operation.Status);
			}
		}

		//
		//Handler
		//

		public class Handler : ISubscriptionListener<GId, OperationRepresentation>
		{
			private SimpleOperationProcessor operationProcessor;

			public Handler(SimpleOperationProcessor processor)
			{
				this.operationProcessor = processor;
			}

			public void OnError(ISubscription<GId> subscription, Exception ex)
			{
			}

			public void OnNotification(ISubscription<GId> subscription, OperationRepresentation notification)
			{
				operationProcessor.Process(notification);
			}
		}
	}
}