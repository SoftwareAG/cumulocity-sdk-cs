﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public class CustomPropertiesMapRepresentation : BaseResourceRepresentation
	{

		private IDictionary<string, object> customProperties;

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
