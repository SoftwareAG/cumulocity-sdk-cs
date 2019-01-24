using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class SupportUserDetailsRepresentation : BaseResourceRepresentation
	{
		private bool? enabled;

		private DateTime expiryDate;

		private int? activeRequestCount;

		public virtual bool? Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		public virtual DateTime ExpiryDate
		{
			get
			{
				return expiryDate;
			}
			set
			{
				this.expiryDate = value;
			}
		}

		public virtual int? ActiveRequestCount
		{
			get
			{
				return activeRequestCount;
			}
			set
			{
				this.activeRequestCount = value;
			}
		}
	}
}