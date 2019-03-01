using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Model.Operation
{
	public sealed class OperationStatus
	{
		public static readonly OperationStatus PENDING = new OperationStatus("PENDING", InnerEnum.PENDING);
		public static readonly OperationStatus SUCCESSFUL = new OperationStatus("SUCCESSFUL", InnerEnum.SUCCESSFUL);
		public static readonly OperationStatus FAILED = new OperationStatus("FAILED", InnerEnum.FAILED);
		public static readonly OperationStatus EXECUTING = new OperationStatus("EXECUTING", InnerEnum.EXECUTING);

		private static readonly IList<OperationStatus> valueList = new List<OperationStatus>();

		static OperationStatus()
		{
			valueList.Add(PENDING);
			valueList.Add(SUCCESSFUL);
			valueList.Add(FAILED);
			valueList.Add(EXECUTING);
		}

		public enum InnerEnum
		{
			PENDING,
			SUCCESSFUL,
			FAILED,
			EXECUTING
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private OperationStatus(string name, InnerEnum innerEnum)
		{
			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public static OperationStatus asOperationStatus(object status)
		{
			if (status is OperationStatus)
			{
				return (OperationStatus)status;
			}
			else
			{
				return asOperationStatus(status.ToString());
			}
		}

		public static OperationStatus asOperationStatus(string status)
		{
			return string.ReferenceEquals(status, null) ? null : valueOf(status);
		}

		public static IList<OperationStatus> values()
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

		public static OperationStatus valueOf(string name)
		{
			foreach (OperationStatus enumInstance in OperationStatus.valueList)
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