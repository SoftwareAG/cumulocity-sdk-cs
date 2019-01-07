using Cumulocity.SDK.Client.Rest.Representation.Event;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	//ORIGINAL LINE: @EqualsAndHashCode public class AuditRecordRepresentation extends EventRepresentation
	public class AuditRecordRepresentation : EventRepresentation
	{
		private string user;

		private string application;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private String activity;
		private string activity;

		private string severity;

		//ORIGINAL LINE: @Null(operation = Command.UPDATE) private Set<Change> changes;
		private ISet<Change> changes;

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getUser()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getApplication()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getActivity()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getSeverity()
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

		//ORIGINAL LINE: @JSONTypeHint(Change.class) @JSONProperty(ignoreIfNull = true) public Set<Change> getChanges()
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