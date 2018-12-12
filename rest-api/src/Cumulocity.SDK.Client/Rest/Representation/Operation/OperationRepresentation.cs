using System;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Connector;
using Cumulocity.SDK.Client.Rest.Representation.Identity;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NoArgsConstructor @AllArgsConstructor(access = AccessLevel.PRIVATE) @Builder(builderMethodName = "aOperation") public class OperationRepresentation extends AbstractExtensibleRepresentation
    public class OperationRepresentation : AbstractExtensibleRepresentation
    {
        private long? bulkOperationId;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Getter(onMethod = @_(@JSONProperty(ignoreIfNull = true))) @Setter @Null(operation = { Command.UPDATE }) private ConnectorReferenceRepresentation connector;
        private ConnectorReferenceRepresentation connector;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Null(operation = { Command.CREATE, Command.UPDATE }) private DateTime creationTime;
        private DateTime creationTime;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Getter(onMethod = @_(@JSONProperty(ignoreIfNull = true))) @Setter @Null(operation = { Command.CREATE, Command.UPDATE }) private DeliveryRepresentation delivery;
        private DeliveryRepresentation delivery;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Null(operation = { Command.CREATE, Command.UPDATE }) private ExternalIDCollectionRepresentation deviceExternalIDs;
        private ExternalIDCollectionRepresentation deviceExternalIDs;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Null(operation = Command.UPDATE) private GId deviceId;
        private GId deviceId;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Null(operation = Command.CREATE) private String failureReason;
        private string failureReason;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Null(operation = { Command.CREATE }) private GId id;
        private GId id;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.UPDATE) @Null(operation = Command.CREATE) private String status;
        private string status;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONConverter(type = IDTypeConverter.class) @JSONProperty(ignoreIfNull = true) public GId getId()
        public virtual GId Id
        {
            get => id;
            set => id = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONConverter(type = IDTypeConverter.class) @JSONProperty(ignoreIfNull = true) public GId getDeviceId()
        public virtual GId DeviceId
        {
            get => deviceId;
            set => deviceId = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getDeviceName()
        public virtual string DeviceName { get; set; }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getStatus()
        public virtual string Status
        {
            get => status;
            set => status = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getFailureReason()
        public virtual string FailureReason
        {
            get => failureReason;
            set => failureReason = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "deprecated_CreationTime", ignore = true) @Deprecated public Date getCreationTime()
//	[Obsolete]
//	public virtual DateTime CreationTime
//	{
//		get
//		{
//			return creationTime == null ? null : creationTime.toDate();
//		}
//		set
//		{
//			this.creationTime = value == null ? null : newLocal(value);
//		}
//	}


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "creationTime", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getCreationDateTime()
        public virtual DateTime CreationDateTime
        {
            get => creationTime;
            set => creationTime = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ExternalIDCollectionRepresentation getDeviceExternalIDs()
        public virtual ExternalIDCollectionRepresentation DeviceExternalIDs
        {
            get => deviceExternalIDs;
            set => deviceExternalIDs = value;
        }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getBulkOperationId()
        public virtual long? BulkOperationId
        {
            get => bulkOperationId;
            set => bulkOperationId = value;
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            var result = 1;
            result = prime * result + (creationTime == null ? 0 : creationTime.GetHashCode());
            result = prime * result + (deviceExternalIDs == null ? 0 : deviceExternalIDs.GetHashCode());
            result = prime * result + (deviceId == null ? 0 : deviceId.GetHashCode());
            result = prime * result + (ReferenceEquals(failureReason, null) ? 0 : failureReason.GetHashCode());
            result = prime * result + (id == null ? 0 : id.GetHashCode());
            result = prime * result + (ReferenceEquals(status, null) ? 0 : status.GetHashCode());
            result = prime * result + (bulkOperationId == null ? 0 : bulkOperationId.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            var other = (OperationRepresentation) obj;
            if (creationTime == null)
            {
                if (other.creationTime != null) return false;
            }
            else if (!creationTime.Equals(other.creationTime))
            {
                return false;
            }

            if (deviceExternalIDs == null)
            {
                if (other.deviceExternalIDs != null) return false;
            }
            else if (!deviceExternalIDs.Equals(other.deviceExternalIDs))
            {
                return false;
            }

            if (deviceId == null)
            {
                if (other.deviceId != null) return false;
            }
            else if (!deviceId.Equals(other.deviceId))
            {
                return false;
            }

            if (ReferenceEquals(failureReason, null))
            {
                if (!ReferenceEquals(other.failureReason, null)) return false;
            }
            else if (!failureReason.Equals(other.failureReason))
            {
                return false;
            }

            if (id == null)
            {
                if (other.id != null) return false;
            }
            else if (!id.Equals(other.id))
            {
                return false;
            }

            if (ReferenceEquals(status, null))
            {
                if (!ReferenceEquals(other.status, null)) return false;
            }
            else if (!status.Equals(other.status))
            {
                return false;
            }

            if (bulkOperationId == null)
            {
                if (other.bulkOperationId != null) return false;
            }
            else if (!bulkOperationId.Equals(other.status))
            {
                return false;
            }

            return true;
        }
    }
}