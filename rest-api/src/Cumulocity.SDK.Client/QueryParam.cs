namespace Cumulocity.SDK.Client
{
    public class QueryParam
    {

        private readonly IParam key;

        private readonly string value;

        public QueryParam(IParam key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public virtual IParam Key
        {
            get
            {
                return key;
            }
        }

        public virtual string Value
        {
            get
            {
                return value;
            }
        }
    }
}