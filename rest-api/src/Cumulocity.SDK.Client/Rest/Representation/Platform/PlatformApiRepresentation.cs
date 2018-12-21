using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using Cumulocity.SDK.Client.Rest.Representation.Operation;

namespace Cumulocity.SDK.Client.Rest.Representation.Platform
{
    public class PlatformApiRepresentation : AbstractExtensibleRepresentation
    {
	    public EventsApiRepresentation Event;
	    public MeasurementsApiRepresentation Measurement;
		//        private AuditRecordsRepresentation audit;
	    public AlarmsApiRepresentation Alarm;
		//        private UsersApiRepresentation user;
	    public DeviceControlRepresentation DeviceControl;
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