using Cumulocity.SDK.Client.Rest.Representation.Event;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	public class AuditRecordRepresentation : EventRepresentation
	{
		private string user;

		private string application;

		private string activity;

		private string severity;

		private ISet<Change> changes;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string User
		{
			get
			{
				return user;
			}
			set
			{
				this.user = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Application
		{
			get
			{
				return application;
			}
			set
			{
				this.application = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Activity
		{
			get
			{
				return activity;
			}
			set
			{
				this.activity = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Severity
		{
			get
			{
				return severity;
			}
			set
			{
				this.severity = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ISet<Change> Changes
		{
			get
			{
				return changes;
			}
			set
			{
				this.changes = value;
			}
		}
	}
}