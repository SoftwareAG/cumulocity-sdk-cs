using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Audit;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{

	public class AlarmRepresentationBuilder : AbstractObjectBuilder<AlarmRepresentation>
	{

		private readonly ISet<object> dynamicProperties = new System.Collections.Generic.HashSet<object>();

		public virtual AlarmRepresentationBuilder withId(GId value)
		{
			setFieldValue("id", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withStatus(string value)
		{
			setFieldValue("status", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withSeverity(string value)
		{
			setFieldValue("severity", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withHistory(AuditRecordCollectionRepresentation value)
		{
			setFieldValue("history", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withText(string value)
		{
			setFieldValue("text", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withSource(ManagedObjectRepresentation value)
		{
			setFieldValue("Source", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withType(string value)
		{
			setFieldValue("type", value);
			return this;
		}

		[Obsolete]
		public virtual AlarmRepresentationBuilder withTime(DateTime value)
		{
			setFieldValue("time", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withDateTime(DateTime value)
		{
			setFieldValue("DateTime", value);
			return this;
		}

		[Obsolete]
		public virtual AlarmRepresentationBuilder withCreationTime(DateTime value)
		{
			setFieldValue("creationTime", value);
			return this;
		}

		public virtual AlarmRepresentationBuilder withCreationDateTime(DateTime value)
		{
			setFieldValue("creationTime", value);
			return this;
		}

		protected internal override AlarmRepresentation createDomainObject()
		{
			AlarmRepresentation alarm = new AlarmRepresentation();
			foreach (object @object in dynamicProperties)
			{
				alarm.set(@object);
			}
			return alarm;
		}

		//ORIGINAL LINE: public AlarmRepresentationBuilder with(final Object object)
		public virtual AlarmRepresentationBuilder with(object @object)
		{
			dynamicProperties.Add(@object);
			return this;
		}

	}
}
