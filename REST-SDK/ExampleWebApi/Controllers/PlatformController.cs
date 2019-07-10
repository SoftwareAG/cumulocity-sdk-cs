using System;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model.C8Y;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        public PlatformController(PlatformImpl platform)
        {
            this.Platform = platform;
            this.InventoryAPI = this.Platform.InventoryApi;
            this.AlarmApi = this.Platform.AlarmApi;

            createSampleDevice();
        }

        private void createSampleDevice()
        {
            SampleDevice = new ManagedObjectRepresentation
            {
                Name = "Sample Device"
            };
            SampleDevice.Set(new IsDevice());
            SampleDevice = this.InventoryAPI.Create(SampleDevice);
        }

        public PlatformImpl Platform { get; set; }

        public IInventoryApi InventoryAPI { get; set; }

        public IAlarmApi AlarmApi { get; set; }

        public ManagedObjectRepresentation SampleDevice { get; set; }

        [HttpGet]
        [Route("CreateMoInventory")]
        public ActionResult<string> CreateMoInventory()
        {
            var mo = new ManagedObjectRepresentation
            {
                Name = "Hello, world!"
            };
            mo.Set(new IsDevice());
            mo = this.InventoryAPI.Create(mo);
            return mo.Self;
        }

        [HttpGet]
        [Route("CreateAlarmInventory")]
        public ActionResult<string> CreateAlarmInventory()
        {
            var alarm = this.AlarmApi.Create(new AlarmRepresentation()
            {
                Type = "com_nsn_bts_TrxFaulty",
                Status = "ACTIVE",
                Severity = "CRITICAL",
                Text = "Bad error",
                DateTime = DateTime.UtcNow,
                Source = SampleDevice

            });
            return alarm.Self;
        }
    }
}