using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using System;
using Cumulocity.SDK.Client.Rest.API.Audit;
using Cumulocity.SDK.Client.Rest.API.Cep;
using Cumulocity.SDK.Client.Rest.API.Option;
using Cumulocity.SDK.Client.Rest.API.User;

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
		ITenantOptionApi TenantOptionApi {get;}
		IAuditRecordApi AuditRecordApi {get;}
		IUserApi UserApi {get;}
		//IBinariesApi BinariesApi {get;}
	}
}