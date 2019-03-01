using System;

namespace Cumulocity.SDK.Client.Rest.Model.Option
{
	[Serializable]
	public class OptionPK
	{
		public static readonly OptionPK TWO_FACTOR_AUTHENTICATION_ENABLED = OptionCategory.TWO_FACTOR_AUTHENTICATION_CATEGORY.optionPk("enabled");

		public static readonly OptionPK TWO_FACTOR_AUTHENTICATION_ENFORCED = OptionCategory.TWO_FACTOR_AUTHENTICATION_CATEGORY.optionPk("enforced");

		public static readonly OptionPK TWO_FACTOR_AUTHENTICATION_PIN_LENGTH = OptionCategory.TWO_FACTOR_AUTHENTICATION_CATEGORY.optionPk("pin.lenght");

		public static readonly OptionPK TWO_FACTOR_AUTHENTICATION_PIN_VALIDITY = OptionCategory.TWO_FACTOR_AUTHENTICATION_CATEGORY.optionPk("pin.validity");

		public static readonly OptionPK TWO_FACTOR_AUTHENTICATION_TOKEN_LENGTH = OptionCategory.TWO_FACTOR_AUTHENTICATION_CATEGORY.optionPk("token.length");

		public static readonly OptionPK TWO_FACTOR_AUTHENTICATION_TOKEN_VALIDITY = OptionCategory.TWO_FACTOR_AUTHENTICATION_CATEGORY.optionPk("token.validity");

		public static readonly string SUPPORT_USER_ENABLED_SYSTEM = "system." + OptionCategory.SUPPORT_USER_NAME + ".enabled";

		public static readonly OptionPK SUPPORT_USER_ENABLED_SYSTEM_CONFIG = OptionCategory.CONFIGURATION.optionPk("system.support-user.enabled");

		public static readonly OptionPK SUPPORT_USER_ENABLED = OptionCategory.SUPPORT_USER.optionPk("enabled");

		public static readonly string SUPPORT_USER_VALIDITY_LIMIT_SYSTEM = "system." + OptionCategory.SUPPORT_USER_NAME + ".validity-limit";

		public static readonly OptionPK SUPPORT_USER_VALIDITY_LIMIT_CONFIG = OptionCategory.CONFIGURATION.optionPk("system.support-user.validity-limit");

		public static readonly OptionPK SENDER_ADDRESS = new OptionPK(OptionCategory.MESSAGING, "sms.senderAddress");

		public static readonly OptionPK SENDER_NAME = new OptionPK(OptionCategory.MESSAGING, "sms.senderName");

		public static readonly OptionPK STORAGE_LIMIT_THRESHOLD_LEVEL = OptionCategory.STORAGE_LIMITATION.optionPk("threshold.level");

		public static readonly OptionPK STORAGE_LIMIT_GROUP_NAME = OptionCategory.STORAGE_LIMITATION.optionPk("group.name");

		public static readonly OptionPK DEFAULT_APPLICATION = OptionCategory.APPLICATION.optionPk("default.application");

		public static readonly OptionPK FILES_MAX_SIZE = OptionCategory.FILES.optionPk("max.size");

		public static readonly OptionPK JWT_PRIVATE_KEY = OptionCategory.JWT.optionPk(Option.SECURE_PREFIX + "key.private");

		public static readonly OptionPK JWT_PUBLIC_KEY = OptionCategory.JWT.optionPk(Option.SECURE_PREFIX + "key.public");

		public const string MICROSERVICE_URL_KEY = "microservice.url";

		public const string CONFIGURATION_CATEGORY = "configuration";

		private string key;

		private string category;

		public OptionPK()
		{
		}

		public OptionPK(OptionPK source) : this(source.Category, source.Key)
		{
		}

		public OptionPK(OptionCategory category, string key) : this(category.Name, key)
		{
		}

		public OptionPK(string category, string key)
		{
			this.key = key;
			this.category = category;
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

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((string.ReferenceEquals(category, null)) ? 0 : category.GetHashCode());
			result = prime * result + ((string.ReferenceEquals(key, null)) ? 0 : key.GetHashCode());
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
			OptionPK other = (OptionPK)obj;
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
			return true;
		}

		public override string ToString()
		{
			return name("/");
		}

		public virtual string propertyName()
		{
			return name(".");
		}

		public virtual string name(string separator)
		{
			return category + separator + key;
		}
	}
}