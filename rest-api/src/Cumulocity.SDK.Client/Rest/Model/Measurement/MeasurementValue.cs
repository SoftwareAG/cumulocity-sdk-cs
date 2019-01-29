using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Model.Measurement
{
	[Serializable]
	public class MeasurementValue
	{

		private const long serialVersionUID = 82786895631760488L;

		private decimal value;

		private string unit;

		private ValueType type;

		private string quantity;

		private StateType state;

		public MeasurementValue()
		{
		}

		public MeasurementValue(string unit)
		{
			this.unit = unit;
		}

		public MeasurementValue(decimal value, string unit)
		{
			this.value = value;
			this.unit = unit;
		}

		public MeasurementValue(decimal value, string unit, ValueType type, string quantity, StateType state)
		{
			this.value = value;
			this.unit = unit;
			this.type = type;
			this.quantity = quantity;
			this.state = state;
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = false) public BigDecimal getValue()
		public virtual decimal Value
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


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = false) public String getUnit()
		public virtual string Unit
		{
			get
			{
				return unit;
			}
			set
			{
				this.unit = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ValueType getType()
		public virtual ValueType Type
		{
			get
			{
				return type;
			}
			set
			{
				this.type = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getQuantity()
		public virtual string Quantity
		{
			get
			{
				return quantity;
			}
			set
			{
				this.quantity = value;
			}
		}


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public StateType getState()
		public virtual StateType State
		{
			get
			{
				return state;
			}
			set
			{
				this.state = value;
			}
		}


		public override int GetHashCode()
		{
			int result = value != null ? value.GetHashCode() : 0;
			result = 31 * result + (!string.ReferenceEquals(unit, null) ? unit.GetHashCode() : 0);
			result = 31 * result + (type != null ? type.GetHashCode() : 0);
			result = 31 * result + (!string.ReferenceEquals(quantity, null) ? quantity.GetHashCode() : 0);
			result = 31 * result + (state != null ? state.GetHashCode() : 0);
			return result;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (!(o is MeasurementValue))
			{
				return false;
			}

			MeasurementValue that = (MeasurementValue)o;

			if (!string.ReferenceEquals(quantity, null) ? !quantity.Equals(that.quantity) : !string.ReferenceEquals(that.quantity, null))
			{
				return false;
			}
			if (state != that.state)
			{
				return false;
			}
			if (type != that.type)
			{
				return false;
			}
			if (!string.ReferenceEquals(unit, null) ? !unit.Equals(that.unit) : !string.ReferenceEquals(that.unit, null))
			{
				return false;
			}
			if (value != null ? !value.Equals(that.value) : that.value != null)
			{
				return false;
			}

			return true;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("MeasurementValue [value=");
			builder.Append(value);
			if (!string.ReferenceEquals(unit, null))
			{
				builder.Append(", unit=");
				builder.Append(unit);
			}
			if (type != null)
			{
				builder.Append(", type=");
				builder.Append(type);
			}
			if (!string.ReferenceEquals(quantity, null))
			{
				builder.Append(", quantity=");
				builder.Append(quantity);
			}
			if (state != null)
			{
				builder.Append(", state=");
				builder.Append(state);
			}
			builder.Append("]");
			return builder.ToString();
		}
	}
}
