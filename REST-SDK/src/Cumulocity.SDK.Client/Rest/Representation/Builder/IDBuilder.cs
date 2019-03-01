using Cumulocity.SDK.Client.Rest.Model;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
	public class IDBuilder : AbstractObjectBuilder<ID>
	{
		public virtual IDBuilder withType(string type)
		{
			setFieldValue("type", type);
			return this;
		}

		public virtual IDBuilder withValue(string value)
		{
			setFieldValue("value", value);
			return this;
		}

		public virtual IDBuilder withName(string name)
		{
			setFieldValue("name", name);
			return this;
		}

		protected internal override ID createDomainObject()
		{
			return new ID();
		}
	}
}