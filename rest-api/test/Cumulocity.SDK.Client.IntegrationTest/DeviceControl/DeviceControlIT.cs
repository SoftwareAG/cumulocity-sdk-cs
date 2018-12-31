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
			//operation1 = null;
			allOperations = null;
		}

		public void Dispose()
		{
			if (poller != null)
			{
				poller.stop();
			}
			if (subscriber != null)
			{
				subscriber.disconnect();
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

			agent = inventoryApi.create(agent);
			device = inventoryApi.create(device);
			agent2 = inventoryApi.create(agent2);
			device2 = inventoryApi.create(device2);

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
			inventoryApi.getManagedObject(parent.Id).addChildDevice(deviceRef);
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
			//    When I create an operation for device '1'
			iCreateAnOperationForDevice(1);
			//    Then poller should receive operation
			pollerShouldRecieveOperation();
		}

		//
		//Given
		//

		//@Given("^I have a poller for agent '([^']*)'$")

		public void iHaveAPollerForAgent(int arg1)
		{
			GId agentId = getMoId(arg1);
			poller = new OperationsByAgentAndStatusPollerImpl(deviceControlResource, agentId.Value, OperationStatus.PENDING, operationProcessor);
			poller.start();
		}

		private GId getMoId(int arg1)
		{
			GId deviceId = managedObjects[arg1].Id;
			return deviceId;
		}

		//
		//When
		//

		//@When("^I create an operation for device '([^']*)'$")
		public void iCreateAnOperationForDevice(int deviceNum)
		{
			GId deviceId = getMoId(deviceNum);
			OperationRepresentation operationRepresentation = new OperationRepresentation();
			operationRepresentation.DeviceId = deviceId;
			operationRepresentation.set("smaple_value", "sample_operation_type");
			operation1 = deviceControlResource.create(operationRepresentation);
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
				Assert.Equal(1,operationProcessor.Operations.Count); 
			}
			catch (ThreadInterruptedException e)
			{
				Debug.WriteLine(e.StackTrace.ToString());
			}
		}
	}
}