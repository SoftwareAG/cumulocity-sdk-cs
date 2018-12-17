using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Event
{
	using System;

	public class EventRepresentation : AbstractExtensibleRepresentation
	{
		//ORIGINAL LINE: @Null(operation = { Command.CREATE }) private GId id;
		private GId id;

		//ORIGINAL LINE: @Null(operation = Command.UPDATE) @NotNull(operation = Command.CREATE) private String type;
		private string type;

		//ORIGINAL LINE: @Null(operation = Command.UPDATE) @NotNull(operation = Command.CREATE) private DateTime time;
		private DateTime? time;

		//ORIGINAL LINE: @Null(operation = { Command.CREATE, Command.UPDATE }) private DateTime creationTime;
		private DateTime? creationTime;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private String text;
		private string text;

		private ManagedObjectRepresentation managedObject;

		private ExternalIDRepresentation externalSource;

		public EventRepresentation()
		{
		}

		//ORIGINAL LINE: @JSONConverter(type = IDTypeConverter.class) @JSONProperty(ignoreIfNull = true) public GId getId()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GidConverter))]
		public virtual GId Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getType()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Type
		{
			get
			{
				return type;
			}
			set
			{
				this.type = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(value = "time", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getDateTime()
		[JsonProperty(propertyName: "time", NullValueHandling = NullValueHandling.Ignore)]
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

		//ORIGINAL LINE: @JSONProperty(value = "text", ignoreIfNull = true) public String getText()
		[JsonProperty(propertyName: "text", NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Text
		{
			get
			{
				return text;
			}
			set
			{
				this.text = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(value = "source", ignoreIfNull = true) public ManagedObjectRepresentation getSource()
		[JsonProperty(propertyName: "source", NullValueHandling = NullValueHandling.Ignore)]
		public virtual ManagedObjectRepresentation Source
		{
			get
			{
				return managedObject;
			}
			set
			{
				this.managedObject = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(value = "externalSource", ignoreIfNull = true) public ExternalIDRepresentation getExternalSource()
		[JsonProperty(propertyName: "externalSource", NullValueHandling = NullValueHandling.Ignore)]
		public virtual ExternalIDRepresentation ExternalSource
		{
			get
			{
				return externalSource;
			}
			set
			{
				this.externalSource = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(value = "creationTime", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getCreationDateTime()
		[JsonProperty(propertyName: "creationTime", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime? CreationDateTime
		{
			get
			{
				return creationTime;
			}
			set
			{
				this.creationTime = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(value = "deprecated_CreationTime", ignore = true) @Deprecated public Date getCreationTime()
		//[Obsolete]
		//public virtual DateTime CreationTime
		//{
		//	get
		//	{
		//		return creationTime == null ? null : creationTime.toDate();
		//	}
		//	set
		//	{
		//		this.creationTime = value == null ? null : newLocal(value);
		//	}
		//}
		//ORIGINAL LINE: @JSONProperty(value = "deprecated_Time", ignore = true) @Deprecated public Date getTime()
		//[Obsolete]
		//public virtual DateTime Time
		//{
		//	get
		//	{
		//		return time == null ? null : time.toDate();
		//	}
		//	set
		//	{
		//		this.time = value == null ? null : newLocal(value);
		//	}
		//}
	}
}