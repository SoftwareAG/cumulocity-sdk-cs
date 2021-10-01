using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
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
    public class InventoryController : ControllerBase
    {
        public InventoryController(PlatformImpl platform)
        {
            Platform = platform;
            InventoryAPI = Platform.InventoryApi;
            IdentityApi = Platform.IdentityApi;
        }

        public PlatformImpl Platform { get; set; }

        public IInventoryApi InventoryAPI { get; set; }

        public IIdentityApi IdentityApi { get; set; }

        [HttpGet]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult<string> CreateManagedObject([FromQuery] string managedObjectName, [FromQuery] string managedObjectType)
        {
            ManagedObjectRepresentation managedObjectRepresentation = new ManagedObjectRepresentation();
            managedObjectRepresentation.SetProperty("c8y_IsDevice", new Object());
            managedObjectRepresentation.Name = managedObjectName;
            managedObjectRepresentation.Type = managedObjectType;

            ManagedObjectRepresentation createdMO = InventoryAPI.Create(managedObjectRepresentation);
            ExternalIDRepresentation externalIDRepresentation = new ExternalIDRepresentation();
            externalIDRepresentation.Type = "c8y_Serial";
            externalIDRepresentation.ExternalId = managedObjectName;
            externalIDRepresentation.ManagedObject = createdMO;

            IdentityApi.Create(externalIDRepresentation);
            return createdMO.toJSON();
        }

        [HttpGet]
        [Route("Read")]
        [Produces("application/json")]
        public ActionResult<string> ReadManagedObject([FromQuery] string externalId)
        {
            ExternalIDRepresentation externalIDRepresentation = IdentityApi.GetExternalId(new ID("c8y_Serial", externalId));
            ManagedObjectRepresentation managedObjectRepresentation = externalIDRepresentation.ManagedObject;
            return InventoryAPI.Get(managedObjectRepresentation.Id).toJSON();
        }

        [HttpGet]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult<string> DeleteManagedObject([FromQuery] string externalId)
        {
            ExternalIDRepresentation externalIDRepresentation = IdentityApi.GetExternalId(new ID("c8y_Serial", externalId));
            ManagedObjectRepresentation managedObjectRepresentation = externalIDRepresentation.ManagedObject;
            InventoryAPI.Delete(managedObjectRepresentation.Id);
            return $"Managed object of id {externalId} has been deleted";
        }
    }
}
