using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using System;
using Cumulocity.SDK.Client.Rest.API.Cep;

namespace Cumulocity.SDK.Client
{
	public interface IPlatform : IDisposable
	{
		IInventoryApi InventoryApi { get; }
		IIdentityApi IdentityApi { get; }
		IRestOperations Rest();
		IMeasurementApi MeasurementApi { get; }
		IDeviceControlApi DeviceControlApi { get; }
		IAlarmApi AlarmApi { get; }
		IEventApi EventApi { get; }
		IDeviceCredentialsApi DeviceCredentialsApi { get; }
		ICepApi CepApi { get; }
		//TenantOptionApi TenantOptionApi {get;}
		//AuditRecordApi AuditRecordApi {get;}
		//UserApi UserApi {get;}
		//BinariesApi BinariesApi {get;}
	}
}