using System;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.util;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model
{
    //com.cumulocity.model
    [PackageName("com_cumulocity_model_id")]
    public class ID
    {
        private string name;

        private string type;

        private string value;

        public ID() : this(null)
        {
        }

        public ID(string id)
        {
            type = ExtensibilityConverter.ClassToStringRepresentation(GetType());
            value = id;
        }

        public ID(string type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public ID(string type, string value, string name)
        {
            this.type = type;
            this.value = value;
            this.name = name;
        }

        public virtual string Type
        {
            get => type;
            set => type = value;
        }


        public virtual string Value
        {
            get => value;
            set => this.value = value;
        }


        //JAVA TO C# TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @JSONProperty(ignore = true) public String getName()
        public virtual string Name
        {
            get => name;
            set => name = value;
        }


        public override int GetHashCode()
        {
            var result = !ReferenceEquals(type, null) ? type.GetHashCode() : 0;
            result = 31 * result + (!ReferenceEquals(value, null) ? value.GetHashCode() : 0);
            return result;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is ID)) return false;

            var id = (ID) o;

            if (!ReferenceEquals(type, null) ? !type.Equals(id.type) : !ReferenceEquals(id.type, null)) return false;
            if (!ReferenceEquals(value, null) ? !value.Equals(id.value) : !ReferenceEquals(id.value, null))
                return false;

            return true;
        }

	    /// <summary>
	    ///     This method compares if two ids have the same content (type, value) even if they have different classes. This
	    ///     method is useful for the identity service.
	    /// </summary>
	    /// <param name="o"> the ID to compare with this instance </param>
	    /// <returns> true if both ID have the same contents (type, value) regardless of the class, false otherwise </returns>
	    public virtual bool equalsIgnoreClass(object o)
        {
            if (this == o) return true;
            if (!(o is ID)) return false;

            var id = (ID) o;

            if (!ReferenceEquals(type, null)
                ? !type.Equals(id.type, StringComparison.CurrentCultureIgnoreCase)
                : !ReferenceEquals(id.type, null)) return false;
            if (!ReferenceEquals(value, null)
                ? !value.Equals(id.value, StringComparison.CurrentCultureIgnoreCase)
                : !ReferenceEquals(id.value, null)) return false;

            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("ID [type=").Append(type).Append(", value=").Append(value).Append("]");
            return builder.ToString();
        }

        public static string asString(ID id)
        {
            return id == null ? null : id.Value;
        }
    }
}