using System;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;

namespace Cumulocity.SDK.Client
{
    public interface IPlatform : IDisposable
    {
        IInventoryApi InventoryApi {get;}
        IIdentityApi IdentityApi { get; }
        IRestOperations Rest();

        //MeasurementApi MeasurementApi {get;}

        //DeviceControlApi DeviceControlApi {get;}

        //IAlarmApi AlarmApi {get;}

        IEventApi EventApi {get;}

        //AuditRecordApi AuditRecordApi {get;}

        //CepApi CepApi {get;}

        //DeviceCredentialsApi DeviceCredentialsApi {get;}

        //BinariesApi BinariesApi {get;}

        //UserApi UserApi {get;}

        //TenantOptionApi TenantOptionApi {get;}
    }


}