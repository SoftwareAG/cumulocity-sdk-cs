using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class OptionRepresentation : BaseResourceRepresentation
	{
		//ORIGINAL LINE: @Size(max = 256) private String category;
		private string category;

		//ORIGINAL LINE: @Size(max = 256) private String key;
		private string key;

		private string value;

		public static OptionRepresentation asOptionRepresetation(string category, string key, string value)
		{
			OptionRepresentation optionRepresentation = new OptionRepresentation();
			optionRepresentation.Key = key;
			optionRepresentation.Value = value;
			optionRepresentation.Category = category;
			return optionRepresentation;
		}

		[MaxLength(256)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Category
		{
			get
			{
				return category;
			}
			set
			{
				this.category = value;
			}
		}

		[MaxLength(256)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Key
		{
			get
			{
				return key;
			}
			set
			{
				this.key = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getValue()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignore = true) public boolean isTrue()
		[JsonIgnore]
		public virtual bool IsTrue
		{
			get
			{
				return "true".Equals(Value, StringComparison.CurrentCultureIgnoreCase);
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignore = true) public boolean isFalse()
		[JsonIgnore]
		public virtual bool IsFalse
		{
			get
			{
				return !IsTrue;
			}
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((string.ReferenceEquals(category, null)) ? 0 : category.GetHashCode());
			result = prime * result + ((string.ReferenceEquals(key, null)) ? 0 : key.GetHashCode());
			result = prime * result + ((string.ReferenceEquals(value, null)) ? 0 : value.GetHashCode());
			return result;
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			OptionRepresentation other = (OptionRepresentation)obj;
			if (string.ReferenceEquals(category, null))
			{
				if (!string.ReferenceEquals(other.category, null))
				{
					return false;
				}
			}
			else if (!category.Equals(other.category))
			{
				return false;
			}
			if (string.ReferenceEquals(key, null))
			{
				if (!string.ReferenceEquals(other.key, null))
				{
					return false;
				}
			}
			else if (!key.Equals(other.key))
			{
				return false;
			}
			if (string.ReferenceEquals(value, null))
			{
				if (!string.ReferenceEquals(other.value, null))
				{
					return false;
				}
			}
			else if (!value.Equals(other.value))
			{
				return false;
			}
			return true;
		}
	}
}