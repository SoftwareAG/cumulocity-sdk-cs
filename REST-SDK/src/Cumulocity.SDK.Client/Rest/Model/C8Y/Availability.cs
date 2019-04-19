/*
 * Copyright (C) 2019 Cumulocity GmbH
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using System;
using System.Data;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
	[PackageName("c8y_Availability")]
	public class Availability
	{
		private DateTime lastMessage;
		private ConnectionState status;

		public Availability()
		{
		}

		public Availability(DateTime lastMessage, ConnectionState status)
		{
			this.lastMessage = lastMessage;
			this.status = status;
		}

		[JsonProperty("lastMessage", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime LastMessage
		{
			get
			{
				return lastMessage;
			}
			set
			{
				this.lastMessage = value;
			}
		}
		[JsonProperty("status")]
		public virtual ConnectionState Status
		{
			get
			{
				return status;
			}
			set
			{
				this.status = value;
			}
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((lastMessage == null) ? 0 : lastMessage.GetHashCode());
			result = prime * result + ((status == null) ? 0 : status.GetHashCode());
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
			Availability other = (Availability)obj;
			if (lastMessage == null)
			{
				if (other.lastMessage != null)
				{
					return false;
				}
			}
			else if (!lastMessage.Equals(other.lastMessage))
			{
				return false;
			}
			if (status != other.status)
			{
				return false;
			}
			return true;
		}
	}
}