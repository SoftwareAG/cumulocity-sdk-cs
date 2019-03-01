using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	[JsonObject]
	public class OptionsRepresentation : BaseResourceRepresentation
	{
		private IDictionary<string, object> properties = new Dictionary<string, object>();

		[JsonExtensionData]
		public IDictionary<string, object> Properties
		{
			get => properties;
			set => properties = value;
		}

		public virtual string getProperty(string name)
		{
			object o = Properties[name];
			if (o != null)
			{
				return o.ToString();
			}
			return null;
		}

		public virtual void setProperty(string name, object value)
		{
			Properties[name] = value;
		}

		public virtual ICollection<string> propertyNames()
		{
			return Properties.Keys;
		}

		public bool hasProperty(string name)
		{
			return Properties.ContainsKey(name);
		}

		public object removeProperty(string name)
		{
			return Properties.Remove(name);
		}
	}
}