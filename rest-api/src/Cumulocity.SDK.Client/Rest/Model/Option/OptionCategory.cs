using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Model.Option
{
	public sealed class OptionCategory
	{

		public static readonly OptionCategory ACCESS_CONTROL = new OptionCategory("ACCESS_CONTROL", InnerEnum.ACCESS_CONTROL, "access.control");

		public static readonly OptionCategory FILES = new OptionCategory("FILES", InnerEnum.FILES, "files");

		public static readonly OptionCategory ALARM_TYPE_MAPPING = new OptionCategory("ALARM_TYPE_MAPPING", InnerEnum.ALARM_TYPE_MAPPING, "alarm.type.mapping");

		public static readonly OptionCategory MESSAGING = new OptionCategory("MESSAGING", InnerEnum.MESSAGING, "messaging");

		public static readonly OptionCategory APPLICATION = new OptionCategory("APPLICATION", InnerEnum.APPLICATION, "application");

		public static readonly OptionCategory TWO_FACTOR_AUTHENTICATION_CATEGORY = new OptionCategory("TWO_FACTOR_AUTHENTICATION_CATEGORY", InnerEnum.TWO_FACTOR_AUTHENTICATION_CATEGORY, "two-factor-authentication");

		public static readonly OptionCategory SUPPORT_USER = new OptionCategory("SUPPORT_USER", InnerEnum.SUPPORT_USER, OptionCategory.SUPPORT_USER_NAME);

		public static readonly OptionCategory CONFIGURATION = new OptionCategory("CONFIGURATION", InnerEnum.CONFIGURATION, "configuration");

		public static readonly OptionCategory STORAGE_LIMITATION = new OptionCategory("STORAGE_LIMITATION", InnerEnum.STORAGE_LIMITATION, "storage.limitation");

		public static readonly OptionCategory DATA_BROKER = new OptionCategory("DATA_BROKER", InnerEnum.DATA_BROKER, "dataBroker");

		public static readonly OptionCategory TOKEN_PUBLIC_KEY = new OptionCategory("TOKEN_PUBLIC_KEY", InnerEnum.TOKEN_PUBLIC_KEY, "token.publicKey");

		public static readonly OptionCategory MICROSERVICE_RUNTIME = new OptionCategory("MICROSERVICE_RUNTIME", InnerEnum.MICROSERVICE_RUNTIME, "microservice.runtime");

		public static readonly OptionCategory JWT = new OptionCategory("JWT", InnerEnum.JWT, "jwt");

		public static readonly OptionCategory CEP = new OptionCategory("CEP", InnerEnum.CEP, "cep", true);

		public static readonly OptionCategory APAMA = new OptionCategory("APAMA", InnerEnum.APAMA, "apama", true);

		private static readonly IList<OptionCategory> valueList = new List<OptionCategory>();

		static OptionCategory()
		{
			valueList.Add(ACCESS_CONTROL);
			valueList.Add(FILES);
			valueList.Add(ALARM_TYPE_MAPPING);
			valueList.Add(MESSAGING);
			valueList.Add(APPLICATION);
			valueList.Add(TWO_FACTOR_AUTHENTICATION_CATEGORY);
			valueList.Add(SUPPORT_USER);
			valueList.Add(CONFIGURATION);
			valueList.Add(STORAGE_LIMITATION);
			valueList.Add(DATA_BROKER);
			valueList.Add(TOKEN_PUBLIC_KEY);
			valueList.Add(MICROSERVICE_RUNTIME);
			valueList.Add(JWT);
			valueList.Add(CEP);
			valueList.Add(APAMA);
		}

		public enum InnerEnum
		{
			ACCESS_CONTROL,
			FILES,
			ALARM_TYPE_MAPPING,
			MESSAGING,
			APPLICATION,
			TWO_FACTOR_AUTHENTICATION_CATEGORY,
			SUPPORT_USER,
			CONFIGURATION,
			STORAGE_LIMITATION,
			DATA_BROKER,
			TOKEN_PUBLIC_KEY,
			MICROSERVICE_RUNTIME,
			JWT,
			CEP,
			APAMA
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;
		private readonly bool flag;

		public const string ALARM_TYPE_MAPPING_VALUE_FORMAT = "(.*)\\|(.*)";

		public const string SUPPORT_USER_NAME = "support-user";
        
        #pragma warning disable 0169
		private readonly bool checkInParent;

		internal OptionCategory(string name, InnerEnum innerEnum, string value) 
		{

			nameValue = value;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
			flag = false;
		}
		internal OptionCategory(string name, InnerEnum innerEnum, string value,bool flagValue)
		{

			nameValue = value;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
			flag = flagValue;
		}

		public string Name { get; }

		public static OptionCategory forName(string name)
		{
			if (!string.ReferenceEquals(name, null))
			{
				foreach (OptionCategory optionCategory in OptionCategory.values())
				{
					if (optionCategory.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase))
					{
						
						return optionCategory;
					}
				}
			}

			return null;
		}


		public OptionPK optionPk(string value)
		{
			return new OptionPK(this, value);
		}

		public static IList<OptionCategory> values()
		{
			return valueList;
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static OptionCategory valueOf(string name)
		{
			foreach (OptionCategory enumInstance in OptionCategory.valueList)
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}
}
