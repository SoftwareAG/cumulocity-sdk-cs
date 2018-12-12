using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Platform
{
    public class PlatformApiRepresentation : AbstractExtensibleRepresentation
    {
//        private EventsApiRepresentation @event;
//
//        private MeasurementsApiRepresentation measurement;
//
//        private AuditRecordsRepresentation audit;
//
//        private AlarmsApiRepresentation alarm;
//
//        private UsersApiRepresentation user;
//
//        private DeviceControlRepresentation deviceControl;
//
//        private CepApiRepresentation cep;
//
//        private TenantApiRepresentation tenant;

        public InventoryRepresentation Inventory { get; set; }

        public IdentityRepresentation Identity { get; set; }

//        public virtual EventsApiRepresentation Event
//        {
//            get
//            {
//                return @event;
//            }
//            set
//            {
//                this.@event = value;
//            }
//        }
//
//
//        public virtual MeasurementsApiRepresentation Measurement
//        {
//            get
//            {
//                return measurement;
//            }
//            set
//            {
//                this.measurement = value;
//            }
//        }
//
//
//        public virtual AuditRecordsRepresentation Audit
//        {
//            get
//            {
//                return audit;
//            }
//            set
//            {
//                this.audit = value;
//            }
//        }
//
//
//        public virtual AlarmsApiRepresentation Alarm
//        {
//            get
//            {
//                return alarm;
//            }
//            set
//            {
//                this.alarm = value;
//            }
//        }
//
//
//        public virtual UsersApiRepresentation User
//        {
//            get
//            {
//                return user;
//            }
//            set
//            {
//                this.user = value;
//            }
//        }
//
//
//        public virtual DeviceControlRepresentation DeviceControl
//        {
//            get
//            {
//                return deviceControl;
//            }
//            set
//            {
//                this.deviceControl = value;
//            }
//        }
//
//
////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public CepApiRepresentation getCep()
//        public virtual CepApiRepresentation Cep
//        {
//            get
//            {
//                return cep;
//            }
//            set
//            {
//                this.cep = value;
//            }
//        }
//
//
//        public virtual TenantApiRepresentation Tenant
//        {
//            get
//            {
//                return tenant;
//            }
//            set
//            {
//                this.tenant = value;
//            }
//        }
    }
}