using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	//ORIGINAL LINE: @Builder
	//@NoArgsConstructor
	//@AllArgsConstructor
	//public class OptionsRepresentation extends BaseResourceRepresentation implements DynamicProperties
	public class OptionsRepresentation : BaseResourceRepresentation
	{
		//ORIGINAL LINE: @Singular private Map<String, Object> properties = new HashMap<>();
		private IDictionary<string, object> properties = new Dictionary<string, object>();

		public virtual string getProperty(string name)
		{
			//ORIGINAL LINE: final Object o = properties.get(name);
			object o = properties[name];
			if (o != null)
			{
				return o.ToString();
			}
			return null;
		}

		public virtual void setProperty(string name, object value)
		{
			properties[name] = value;
		}

		public virtual ICollection<string> propertyNames()
		{
			return properties.Keys;
		}

		public bool hasProperty(string name)
		{
			return properties.ContainsKey(name);
		}

		public object removeProperty(string name)
		{
			return properties.Remove(name);
		}
	}
}