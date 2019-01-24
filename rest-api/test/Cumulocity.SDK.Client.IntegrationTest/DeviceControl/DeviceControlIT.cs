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
			createManagedObjects();
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

		public void createManagedObjects()
		{
			ManagedObjectRepresentation agent = aSampleMo().withName("Agent").withType("com.type").with(new Agent())
				.build();
			ManagedObjectRepresentation device = aSampleMo().withName("Device").withType("com.type")
				.build();
			ManagedObjectRepresentation agent2 = aSampleMo().withName("Agent2").withType("com.type").with(new Agent())
				.build();
			ManagedObjectRepresentation device2 = aSampleMo().withName("Device2").withType("com.type")
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
			ManagedObjectReferenceRepresentation deviceRef = RestRepresentationObjectMother.anMoRefRepresentationLike(SampleManagedObjectReferenceRepresentation.MO_REF_REPRESENTATION).withMo(child).build();
			inventoryApi.GetManagedObject(parent.Id).AddChildDevice(deviceRef);
		}

		private static ManagedObjectRepresentationBuilder aSampleMo()
		{
			return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
				.MO_REPRESENTATION);
		}

		//    Scenario: Create Operation and poll it
		[Fact]
		public void createOperationAndPollIt()
		{
			//    Given I have a poller for agent '0'
			iHaveAPollerForAgent(0);
			//    When I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    Then poller should receive operation
			pollerShouldRecieveOperation();
		}

		//
		//    Scenario: adding operations to queue
		[Fact]
		public void addingOperationToQueue()
		{
			//    When I GetFirstPage all operations for device '1'
			iGetAllOperationsForAgent(1);
			//    Then I should receive '0' operations
			iShouldReceiveXOperations(0);
			//    When I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    And I GetFirstPage all operations for device '1'
			iGetAllOperationsForDeviceX(1);
			//    Then I should receive '1' operations
			iShouldReceiveXOperations(1);
		}

		//  Scenario: GetFirstPage notification about new operation
		[Fact]
		public void getNotificationAboutNewOperation()
		{
			//      Given I have a operation subscriber for agent '0'
			iHaveAOperationSubscriberForAgent(0);
			//    When I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    Then subscriber should receive operation
			subscriberShouldReceiveOperation();
		}

		//
		//    Scenario: Operation CRUD

		[Fact]
		public void operationCRUD()
		{
			//    When I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    And I call GetFirstPage on created operation
			iCallGetOnCreatedOperation();
			//    Then I should receive operation with status 'PENDING'
			iShouldReceiveOperationWithStatusX("PENDING");
			//    When I Update created operation with status 'EXECUTING'
			iUpdateCreatedOperationWithStatusX("EXECUTING");
			//    And I call GetFirstPage on created operation
			iCallGetOnCreatedOperation();
			//    Then I should receive operation with status 'EXECUTING'
			iShouldReceiveOperationWithStatusX("EXECUTING");
		}

		//
		//    Scenario: query operations by status
		[Fact]
		public void queryOperationByStatus()
		{
			iQueryOperationsWithStatusX("EXECUTING");
			int numOfExecuting = allOperations.Operations.Count;
			iQueryOperationsWithStatusX("PENDING");
			int numOfPending = allOperations.Operations.Count;
			//    When I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    And I Update created operation with status 'EXECUTING'
			iUpdateCreatedOperationWithStatusX("EXECUTING");
			//    And I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    When I query operations with status 'PENDING'
			iQueryOperationsWithStatusX("PENDING");
			//    Then I should receive '1' operations
			iShouldReceiveXOperations(numOfPending + 1);
			//    And all received operations should have status 'PENDING'
			allRecievedOperationsShouldHaveStatusX("PENDING");
			//    When I query operations with status 'EXECUTING'
			iQueryOperationsWithStatusX("EXECUTING");
			//    Then I should receive '1' operations
			iShouldReceiveXOperations(numOfExecuting + 1);
			//    And all received operations should have status 'EXECUTING'
			allRecievedOperationsShouldHaveStatusX("EXECUTING");
		}

		//
		//    Scenario: query operations by device

		[Fact]
		public void queryOperationsByDevice() 
		{
			//    And I Create an operation for device '1'
			iCreateAnOperationForDevice(1);
		//    And I Create an operation for device '1'
		iCreateAnOperationForDevice(1);
		//    And I Create an operation for device '3'
		iCreateAnOperationForDevice(3);
		//    When I GetFirstPage all operations for device '1'
		iGetAllOperationsForDeviceX(1);
		//    Then I should receive '2' operations
		iShouldReceiveXOperations(2);
		//    When I GetFirstPage all operations for device '3'
		iGetAllOperationsForDeviceX(3);
		//    Then I should receive '1' operations
		iShouldReceiveXOperations(1);
	}

		//
		//    Scenario: query operations by agent

		[Fact]
		public void queryOperationsByAgent()
	{
	//    And I Create an operation for device '1'
	iCreateAnOperationForDevice(1);
	//    And I Create an operation for device '1'
	iCreateAnOperationForDevice(1);
	//    And I Create an operation for device '3'
	iCreateAnOperationForDevice(3);
	//    When I GetFirstPage all operations for agent '0'
	iGetAllOperationsForAgent(0);
	//    Then I should receive '2' operations
	iShouldReceiveXOperations(2);
	//    When I GetFirstPage all operations for agent '2'
	iGetAllOperationsForAgent(2);
	//    Then I should receive '1' operations
	iShouldReceiveXOperations(1);
	}

	//
	//Given
	//

	//@Given("^I have a poller for agent '([^']*)'$")

	public void iHaveAPollerForAgent(int arg1)
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
		public void iHaveAOperationSubscriberForAgent(int arg1)
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
		public void iCreateAnOperationForDevice(int deviceNum)
		{
			GId deviceId = getMoId(deviceNum);
			OperationRepresentation operationRepresentation = new OperationRepresentation();
			operationRepresentation.DeviceId = deviceId;
			operationRepresentation.set("smaple_value", "sample_operation_type");
			operation1 = deviceControlResource.Create(operationRepresentation);
		}

		//@When("^I GetFirstPage all operations for agent '([^']*)'$")

		public void iGetAllOperationsForAgent(int agentNum)
		{
			OperationFilter filter = new OperationFilter().ByAgent(getMoId(agentNum).Value);
			allOperations = deviceControlResource.GetOperationsByFilter(filter).GetFirstPage();
		}

		//@When("^I GetFirstPage all operations for device '([^']*)'$")
		public void iGetAllOperationsForDeviceX(int deviceNum)
		{
			OperationFilter filter = new OperationFilter().ByDevice(getMoId(deviceNum).Value);
			allOperations = deviceControlResource.GetOperationsByFilter(filter).GetFirstPage();
		}

		//@When("^I call GetFirstPage on created operation$")
		public void iCallGetOnCreatedOperation()
		{
			GId operationId = operation1.Id;
			operation1 = deviceControlResource.GetOperation(operationId);
		}

		//@When("^I Update created operation with status '([^']*)'$")
		public void iUpdateCreatedOperationWithStatusX(String status)
		{
			operation1.Status = status;
			operation1 = deviceControlResource.Update(operation1);
		}

		//@When("^I query operations with status '([^']*)'$")
		public void iQueryOperationsWithStatusX(String status)
		{
			OperationFilter filter = new OperationFilter().ByStatus(OperationStatus.valueOf(status));
			allOperations = deviceControlResource.GetOperationsByFilter(filter).GetFirstPage();
		}

		//
		//Then
		//
		//@Then("^poller should receive operation$")

		public void pollerShouldRecieveOperation()
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
		public void iShouldReceiveXOperations(int amount)
		{
			Assert.Equal(amount, allOperations.Operations.Count);
		}

		//@Then("^subscriber should receive operation$")
		public void subscriberShouldReceiveOperation()
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
		public void iShouldReceiveOperationWithStatusX(String status)
		{
			Assert.Equal(status, operation1.Status);
		}

		//@Then("^all received operations should have status '([^']*)'$")

		public void allRecievedOperationsShouldHaveStatusX(String status)
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

			public void onError(ISubscription<GId> subscription, Exception ex)
			{
			}

			public void onNotification(ISubscription<GId> subscription, OperationRepresentation notification)
			{
				operationProcessor.Process(notification);
			}
		}
	}
}