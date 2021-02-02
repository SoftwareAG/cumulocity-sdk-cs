using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Connector;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
#pragma warning disable CS0169
	public class OperationRepresentation : AbstractExtensibleRepresentation
	{
		private long? bulkOperationId;

		private ConnectorReferenceRepresentation connector;

		private DateTime? creationTime;

		private DeliveryRepresentation delivery;

		private ExternalIDCollectionRepresentation deviceExternalIDs;

		private GId deviceId;

		private string failureReason;

		private GId id;

		private string status;

		private string? description;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string? Description
		{
			get => description;
			set => description = value;
		}
		
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GidConverter))]
		public virtual GId Id
		{
			get => id;
			set => id = value;
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GidConverter))]
		public virtual GId DeviceId
		{
			get => deviceId; 
			set => deviceId = value;
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string DeviceName { get; set; }


		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Status
		{
			get => status;
			set => status = value;
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string FailureReason
		{
			get => failureReason;
			set => failureReason = value;
		}


		[JsonProperty("creationTime", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime? CreationTime
		{
			get => creationTime;
			set => creationTime = value;
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ExternalIDCollectionRepresentation DeviceExternalIDs
		{
			get => deviceExternalIDs;
			set => deviceExternalIDs = value;
		}

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