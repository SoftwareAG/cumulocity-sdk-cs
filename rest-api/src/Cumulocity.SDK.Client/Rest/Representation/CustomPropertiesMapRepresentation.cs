using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public class CustomPropertiesMapRepresentation : BaseResourceRepresentation
	{

		private IDictionary<string, object> customProperties;

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public Map<String, Object> getCustomProperties()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IDictionary<string, object> CustomProperties
		{
			get
			{
				return customProperties;
			}
			set
			{
				this.customProperties = value;
			}
		}

	}
}
