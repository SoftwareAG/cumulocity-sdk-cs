using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Connector;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
	//ORIGINAL LINE: @NoArgsConstructor @AllArgsConstructor(access = AccessLevel.PRIVATE) @Builder(builderMethodName = "aOperation") public class OperationRepresentation extends AbstractExtensibleRepresentation
	public class OperationRepresentation : AbstractExtensibleRepresentation
	{
		private long? bulkOperationId;

		//ORIGINAL LINE: @Getter(onMethod = @_(@JSONProperty(ignoreIfNull = true))) @Setter @Null(operation = { Command.UPDATE }) private ConnectorReferenceRepresentation connector;
		private ConnectorReferenceRepresentation connector;

		//ORIGINAL LINE: @Null(operation = { Command.CREATE, Command.UPDATE }) private DateTime creationTime;
		private DateTime? creationTime;

		//ORIGINAL LINE: @Getter(onMethod = @_(@JSONProperty(ignoreIfNull = true))) @Setter @Null(operation = { Command.CREATE, Command.UPDATE }) private DeliveryRepresentation delivery;
		private DeliveryRepresentation delivery;

		//ORIGINAL LINE: @Null(operation = { Command.CREATE, Command.UPDATE }) private ExternalIDCollectionRepresentation deviceExternalIDs;
		private ExternalIDCollectionRepresentation deviceExternalIDs;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Null(operation = Command.UPDATE) private GId deviceId;
		private GId deviceId;

		//ORIGINAL LINE: @Null(operation = Command.CREATE) private String failureReason;
		private string failureReason;

		//ORIGINAL LINE: @Null(operation = { Command.CREATE }) private GId id;
		private GId id;

		//ORIGINAL LINE: @NotNull(operation = Command.UPDATE) @Null(operation = Command.CREATE) private String status;
		private string status;

		//ORIGINAL LINE: @JSONConverter(type = IDTypeConverter.class) @JSONProperty(ignoreIfNull = true) public GId getId()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GidConverter))]
		public virtual GId Id
		{
			get => id;
			set => id = value;
		}

		//ORIGINAL LINE: @JSONConverter(type = IDTypeConverter.class) @JSONProperty(ignoreIfNull = true) public GId getDeviceId()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GidConverter))]
		public virtual GId DeviceId
		{
			get => deviceId; 
			set => deviceId = value;
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getDeviceName()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string DeviceName { get; set; }

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getStatus()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Status
		{
			get => status;
			set => status = value;
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getFailureReason()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string FailureReason
		{
			get => failureReason;
			set => failureReason = value;
		}

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

		//ORIGINAL LINE: @JSONProperty(value = "creationTime", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getCreationDateTime()
		[JsonProperty("creationTime", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime? CreationTime
		{
			get => creationTime;
			set => creationTime = value;
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ExternalIDCollectionRepresentation getDeviceExternalIDs()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ExternalIDCollectionRepresentation DeviceExternalIDs
		{
			get => deviceExternalIDs;
			set => deviceExternalIDs = value;
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getBulkOperationId()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
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
			var other = (OperationRepresentation)obj;
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