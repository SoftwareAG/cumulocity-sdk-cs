using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Measurement
{
	public class MeasurementRepresentation : AbstractExtensibleRepresentation, ICloneable
	{
		//ORIGINAL LINE: @Null(operation = Command.CREATE) private GId id;
		private GId id;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private String type;
		private string type;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private DateTime time;
		private DateTime time;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private ManagedObjectRepresentation source;
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

		//ORIGINAL LINE: @JSONProperty(value = "deprecated_Time", ignore = true) @Deprecated public Date getTime()
		//	[Obsolete]
		//	public virtual DateTime Time
		//	{
		//		get
		//		{
		//			return time == null ? null : time.toDate();
		//		}
		//		set
		//		{
		//			this.time = value == null ? null : newLocal(value);
		//		}
		//	}

		//ORIGINAL LINE: @JSONProperty(value = "time", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getDateTime()
		[JsonProperty("time",NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime DateTime
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
		//ORIGINAL LINE: @Override protected Object clone() throws CloneNotSupportedException
		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}