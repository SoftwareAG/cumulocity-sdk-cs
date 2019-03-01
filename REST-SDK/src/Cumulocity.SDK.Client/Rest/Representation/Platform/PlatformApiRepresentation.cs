using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Audit;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using Cumulocity.SDK.Client.Rest.Representation.User;

namespace Cumulocity.SDK.Client.Rest.Representation.Platform
{
	public class PlatformApiRepresentation : AbstractExtensibleRepresentation
	{
		public EventsApiRepresentation Event { get; set; }
		public MeasurementsApiRepresentation Measurement { get; set; }
		public AlarmsApiRepresentation Alarm { get; set; }
		public DeviceControlRepresentation DeviceControl { get; set; }
		public TenantApiRepresentation Tenant { get; set; }
		public InventoryRepresentation Inventory { get; set; }
		public IdentityRepresentation Identity { get; set; }
		public AuditRecordsRepresentation Audit { get; set; }
		public UsersApiRepresentation User { get; set; }
		//private CepApiRepresentation cep;
	}
}