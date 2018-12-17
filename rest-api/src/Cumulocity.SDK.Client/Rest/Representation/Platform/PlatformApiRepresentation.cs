using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Platform
{
    public class PlatformApiRepresentation : AbstractExtensibleRepresentation
    {
	      public EventsApiRepresentation Event;
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
    }
}