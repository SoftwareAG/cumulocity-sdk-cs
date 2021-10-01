using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
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
    public class OperationsController : ControllerBase
    {
        public PlatformImpl Platform { get; set; }

        public IInventoryApi InventoryAPI { get; set; }

        public IIdentityApi IdentityApi { get; set; }

        public IDeviceControlApi DeviceControlApi { get; set; }

        public OperationsController(PlatformImpl platform)
        {
            Platform = platform;
            InventoryAPI = Platform.InventoryApi;
            IdentityApi = Platform.IdentityApi;
            DeviceControlApi = Platform.DeviceControlApi;
        }

        [HttpPost]
        [Route("createNewOperation")]
        [Produces("application/json")]
        public ActionResult<string> CreateNewOperation()
        {
            ManagedObjectRepresentation managedObjectRepresentation = resolveManagedObject();
            OperationRepresentation operationRepresentation = new OperationRepresentation();
            operationRepresentation.DeviceId = managedObjectRepresentation.Id;
            operationRepresentation.Set("Restart", "description");
            operationRepresentation.SetProperty("c8y_Restart", new Object());

            OperationRepresentation operationRepresentationResponse = DeviceControlApi.Create(operationRepresentation);
            return operationRepresentation.toJSON();
        }

        [HttpGet]
        [Route("getAllOperations")]
        [Produces("application/json")]
        public List<OperationRepresentation> GetAllOperations()
        {
            IOperationCollection operationCollection = DeviceControlApi.Operations;
            PagedOperationCollectionRepresentation<OperationCollectionRepresentation> pagedOperationCollectionRepresentation = operationCollection.GetFirstPage();
            IEnumerable<OperationRepresentation> iterable = pagedOperationCollectionRepresentation.AllPages();
            List<OperationRepresentation> operationRepresentationList = iterable.ToList();
            return operationRepresentationList;
        }

        private ManagedObjectRepresentation resolveManagedObject()
        {
            try
            {
                ExternalIDRepresentation externalIDRepresentation = IdentityApi.GetExternalId(new ID("c8y_Serial", "NewCSSDKDevice2"));
                return externalIDRepresentation.ManagedObject;
            }
            catch (SDKException e)
            {
                ManagedObjectRepresentation newManagedObject = new ManagedObjectRepresentation();
                newManagedObject.SetProperty("c8y_IsDevice", new Object());
                newManagedObject.Name = "NewCSSDKDevice2";
                newManagedObject.Type = "NewCSSDKDevice2";
                newManagedObject.SetProperty("com_cumulocity_model_Agent", new Object());
                List<string> opList = new List<string> { "c8y_Restart" };
                newManagedObject.SetProperty("c8y_SupportedOperations", opList);

                ManagedObjectRepresentation createdMO = InventoryAPI.Create(newManagedObject);
                ExternalIDRepresentation externalIDRepresentation = new ExternalIDRepresentation();
                externalIDRepresentation.Type = "c8y_Serial";
                externalIDRepresentation.ExternalId = "NewCSSDKDevice2";
                externalIDRepresentation.ManagedObject = createdMO;

                IdentityApi.Create(externalIDRepresentation);
                return createdMO;
            }
        }
    }
}
