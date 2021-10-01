using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        public MeasurementController(PlatformImpl platform)
        {
            Platform = platform;
            InventoryAPI = Platform.InventoryApi;
            IdentityApi = Platform.IdentityApi;
            MeasurementApi = Platform.MeasurementApi;
        }

        public PlatformImpl Platform { get; set; }

        public IInventoryApi InventoryAPI { get; set; }

        public IIdentityApi IdentityApi { get; set; }

        public IMeasurementApi MeasurementApi { get; set; }

        [HttpGet]
        [Route("CreateMeasurements")]
        public ActionResult<string> CreateMeasurements()
        {
            for(int temp = 0; temp < 30; temp++)
            {
                ManagedObjectRepresentation managedObjectRepresentation = resolveManagedObject();
                createMeasurement(managedObjectRepresentation);
            }
            return "Measurements created";
        }

        private void createMeasurement(ManagedObjectRepresentation managedObjectRepresentation)
        {
            MeasurementRepresentation measurementRepresentation = new MeasurementRepresentation();
            measurementRepresentation.Source = managedObjectRepresentation;
            measurementRepresentation.DateTime = new DateTime();
            measurementRepresentation.Type = "c8y_PTCMeasurement";
            Random rand = new Random();
            measurementRepresentation.Set(new TemperatureMeasurement() { T = new MeasurementValue() { Unit = "C", Value =  rand.Next(100)} });
            MeasurementApi.Create(measurementRepresentation);
        }

        private ManagedObjectRepresentation resolveManagedObject()
        {
            try
            {
                ExternalIDRepresentation externalIDRepresentation = IdentityApi.GetExternalId(new ID("c8y_Serial", "NewCSSDKDevice"));
                return externalIDRepresentation.ManagedObject;
            } 
            catch (SDKException e)
            {
                ManagedObjectRepresentation newManagedObject = new ManagedObjectRepresentation();
                newManagedObject.SetProperty("c8y_IsDevice", new Object());
                newManagedObject.Name = "NewCSSDKDevice";
                newManagedObject.Type = "NewCSSDKDevice";

                ManagedObjectRepresentation createdMO = InventoryAPI.Create(newManagedObject);
                ExternalIDRepresentation externalIDRepresentation = new ExternalIDRepresentation();
                externalIDRepresentation.Type = "c8y_Serial";
                externalIDRepresentation.ExternalId = "NewCSSDKDevice";
                externalIDRepresentation.ManagedObject = createdMO;

                IdentityApi.Create(externalIDRepresentation);
                return createdMO;
            }
        }
    }
}
