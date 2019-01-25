using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Audit;
using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.Option
{
#pragma warning disable CS0168
	public class Option : Document<GId>, IAuditLogSource<string>
	{
		private readonly ILogger _logger;

		public static readonly string SECURE_PREFIX = "credentials.";

		private string key;

		private string category;

		private string value;

		private bool editable = true;

		public static Option asOption(string category, string key, string value)
		{
			return new Option(category, key, value);
		}

		public static Option asOption(OptionPK key, string value)
		{
			return new Option(key, value);
		}

		public Option()
		{
		}

		public Option(OptionPK key, string value)
		{
			this.key = key.Key;
			this.category = key.Category;
			this.value = value;
		}

		public Option(string category, string key, string value)
		{
			this.key = key;
			this.category = category;
			this.value = value;
		}

		public Option(string category, string key, string value, bool editable) : this(category, key, value)
		{
			this.editable = editable;
		}

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

		public virtual bool Editable
		{
			get
			{
				return editable;
			}
			set
			{
				this.editable = value;
			}
		}

		[JsonIgnore]
		public virtual int? IntValue
		{
			get
			{
				if (String.IsNullOrEmpty(value))
				{
					return null;
				}
				try
				{
					return int.Parse(value.Trim());
				}
				catch (System.FormatException)
				{
					//logger.warn("Value for key {} is not a number: {}!", Key, value);
					return null;
				}
			}
		}
		[JsonIgnore]
		public virtual long? LongValue
		{
			get
			{
				try
				{
					return long.Parse(value.Trim());
				}
				catch (System.FormatException)
				{
					//logger.warn("Value for key {} is not a number: {}!", Key, value);
					return null;
				}
			}
		}


		public virtual int? getIntValue(int? defaultValue)
		{
			if (IntValue != null) return IntValue;
			if (defaultValue != null) return defaultValue;
			else
			{
				throw new NullReferenceException();
			}
		}

		[JsonIgnore]
		public virtual bool? Boolean
		{
			get
			{
				if ("true".Equals(Value, StringComparison.CurrentCultureIgnoreCase))
				{
					return true;
				}
				if ("false".Equals(Value, StringComparison.CurrentCultureIgnoreCase))
				{
					return false;
				}
				return null;
			}
		}

		[JsonIgnore]
		public virtual DateTime? DateTime
		{
			get
			{
				try
				{
					//ORIGINAL LINE: final DateTime dateTime = new DateTime(value);
					return System.DateTime.Parse(value);
				}
				//ORIGINAL LINE: catch (final IllegalArgumentException ex)
				catch (Exception ex)
				{
					return null;
				}
			}
		}

		[JsonIgnore]
		public virtual bool Encrypted
		{
			get
			{
				return Key.StartsWith(SECURE_PREFIX, StringComparison.Ordinal);
			}
		}

		public static bool getBoolean(Option option)
		{
			return getBooleanOrDefault(option, false);
		}

		public static bool getBooleanOrDefault(Option option, bool defaultValue)
		{
			if (option == null || string.ReferenceEquals(option.value, null) || option.value.Length == 0)
			{
				return defaultValue;
			}
			return "true".Equals(option.value, StringComparison.CurrentCultureIgnoreCase);
		}

		public static int getInteger(Option option)
		{
			return getIntegerOrDefault(option, 0);
		}

		public static long getLong(Option option)
		{
			return getLongOrDefault(option, 0);
		}

		public static int getIntegerOrDefault(Option option, int defaultValue)
		{
			if (option == null || string.ReferenceEquals(option.value, null) || option.value.Length == 0)
			{
				return defaultValue;
			}
			return int.Parse(option.value);
		}

		public static long getLongOrDefault(Option option, long defaultValue)
		{
			if (option == null || string.ReferenceEquals(option.value, null) || option.value.Length == 0)
			{
				return defaultValue;
			}
			return long.Parse(option.value);
		}

		[JsonIgnore]
		public string LogSource
		{
			get
			{
				return $"{category}/{key}";
			}
		}

		[JsonIgnore]
		public virtual OptionPK OptionPK
		{
			get
			{
				return new OptionPK(category, key);
			}
		}
		public virtual string buildOptionId()
		{
			return Category + "::" + Key;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || this.GetType() != o.GetType())
			{
				return false;
			}
			if (!base.Equals(o))
			{
				return false;
			}

			Option option = (Option)o;

			if (editable != option.editable)
			{
				return false;
			}
			if (!string.ReferenceEquals(key, null) ? !key.Equals(option.key) : !string.ReferenceEquals(option.key, null))
			{
				return false;
			}
			if (!string.ReferenceEquals(category, null) ? !category.Equals(option.category) : !string.ReferenceEquals(option.category, null))
			{
				return false;
			}
			return !string.ReferenceEquals(value, null) ? value.Equals(option.value) : string.ReferenceEquals(option.value, null);
		}

		public new int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 31 * result + (!string.ReferenceEquals(key, null) ? key.GetHashCode() : 0);
			result = 31 * result + (!string.ReferenceEquals(category, null) ? category.GetHashCode() : 0);
			result = 31 * result + (!string.ReferenceEquals(value, null) ? value.GetHashCode() : 0);
			result = 31 * result + (editable ? 1 : 0);
			return result;
		}

		public new string ToString() => "Option{" +
											 "key='" + key + '\'' +
											 ", category='" + category + '\'' +
											 ", value='" + value + '\'' +
											 ", editable=" + editable +
											 '}';
	}
}