using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Model.Event
{
	public sealed class CumulocityAlarmStatuses : IAlarmStatus
	{
		public static readonly CumulocityAlarmStatuses ACTIVE = new CumulocityAlarmStatuses("ACTIVE", InnerEnum.ACTIVE);
		public static readonly CumulocityAlarmStatuses ACKNOWLEDGED = new CumulocityAlarmStatuses("ACKNOWLEDGED", InnerEnum.ACKNOWLEDGED);
		public static readonly CumulocityAlarmStatuses CLEARED = new CumulocityAlarmStatuses("CLEARED", InnerEnum.CLEARED);

		private static readonly IList<CumulocityAlarmStatuses> valueList = new List<CumulocityAlarmStatuses>();

		static CumulocityAlarmStatuses()
		{
			valueList.Add(ACTIVE);
			valueList.Add(ACKNOWLEDGED);
			valueList.Add(CLEARED);
		}

		public enum InnerEnum
		{
			ACTIVE,
			ACKNOWLEDGED,
			CLEARED
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private CumulocityAlarmStatuses(string name, InnerEnum innerEnum)
		{
			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public static CumulocityAlarmStatuses asAlarmStatus(string status)
		{
			return string.ReferenceEquals(status, null) ? null : valueOf(status);
		}

		public static IList<CumulocityAlarmStatuses> values()
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

		public string name()
		{
			return nameValue;
		}

		public static CumulocityAlarmStatuses valueOf(string name)
		{
			foreach (CumulocityAlarmStatuses enumInstance in CumulocityAlarmStatuses.valueList)
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
