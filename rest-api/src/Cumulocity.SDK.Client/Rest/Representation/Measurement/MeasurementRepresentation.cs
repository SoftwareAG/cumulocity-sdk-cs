using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Measurement
{
	public class MeasurementRepresentation : AbstractExtensibleRepresentation, ICloneable
	{
		private GId id;

		private string type;

		private DateTime? time;

		private ManagedObjectRepresentation source;

		private ExternalIDRepresentation externalSource;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GidConverter))]
		public virtual GId Id
		{
			set
			{
				this.id = value;
			}
			get
			{
				return id;
			}
		}

		public virtual string Type
		{
			set
			{
				this.type = value;
			}
			get
			{
				return type;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ExternalIDRepresentation ExternalSource
		{
			set
			{
				this.externalSource = value;
			}
			get
			{
				return externalSource;
			}
		}

		[JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime? DateTime
		{
			get
			{
				return time;
			}
			set
			{
				this.time = value;
			}
		}

		public virtual ManagedObjectRepresentation Source
		{
			set
			{
				this.source = value;
			}
			get
			{
				return source;
			}
		}

		//Used in conversion to make sure not to do side effects
		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}