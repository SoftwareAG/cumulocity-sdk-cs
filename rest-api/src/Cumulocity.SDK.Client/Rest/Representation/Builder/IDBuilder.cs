using Cumulocity.SDK.Client.Rest.Model;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public class IDBuilder : AbstractObjectBuilder<ID>
    {


         //ORIGINAL LINE: public IDBuilder withType(final String type)
        public virtual IDBuilder withType(string type)
        {
            setFieldValue("type", type);
            return this;
        }


        //ORIGINAL LINE: public IDBuilder withValue(final String value)
        public virtual IDBuilder withValue(string value)
        {
            setFieldValue("value", value);
            return this;
        }


        //ORIGINAL LINE: public IDBuilder withName(final String name)
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