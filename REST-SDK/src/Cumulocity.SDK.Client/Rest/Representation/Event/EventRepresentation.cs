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
		private GId id;

		private string type;

		private DateTime? time;

		private DateTime? creationTime;

		private string text;

		private ManagedObjectRepresentation managedObject;

		private ExternalIDRepresentation externalSource;

		public EventRepresentation()
		{
		}

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
	}
}