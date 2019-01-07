﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	//ORIGINAL LINE: @Builder(builderMethodName = "change") @EqualsAndHashCode @NoArgsConstructor @AllArgsConstructor public class Change
	public class Change
	{

		public enum Type
		{
			REPLACED,
			ADDED,
			REMOVED
		}

		private string attribute;

		private string type;

		private object previousValue;

		private object newValue;

		private Type changeType;

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public Type getChangeType()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual Type ChangeType
		{
			get
			{
				return changeType;
			}
		}

		public Change(string attribute, string type, object previousValue, object newValue)
		{
			this.attribute = attribute;
			this.type = type;
			this.previousValue = previousValue;
			this.newValue = newValue;
		}

		public virtual string getType()
		{
			return type;
		}

		public virtual void setType(string type)
		{
			this.type = type;
		}

		public virtual string Attribute
		{
			get
			{
				return attribute;
			}
			set
			{
				this.attribute = value;
			}
		}


		public virtual object PreviousValue
		{
			get
			{
				return previousValue;
			}
			set
			{
				this.previousValue = value;
			}
		}


		public virtual object NewValue
		{
			get
			{
				return newValue;
			}
			set
			{
				this.newValue = value;
			}
		}


		public override string ToString()
		{
			return "Change [attribute=" + attribute + ", type=" + type + ", previousValue=" + previousValue + ", newValue=" + newValue + "]";
		}
	}

}
