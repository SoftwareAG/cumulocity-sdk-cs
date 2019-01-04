using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class SupportUserDetailsRepresentation : BaseResourceRepresentation
	{
		//ORIGINAL LINE: @NotNull private System.Nullable<bool> enabled;
		private bool? enabled;

		private DateTime expiryDate;

		private int? activeRequestCount;

		//ORIGINAL LINE: @JSONProperty(value = "enabled") public System.Nullable<bool> getEnabled()
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

		//ORIGINAL LINE: @JSONProperty(value = "expiryDate") @JSONConverter(type = DateTimeConverter.class) public DateTime getExpiryDate()
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

		//ORIGINAL LINE: @JSONProperty(value = "activeRequestCount") public System.Nullable<int> getActiveRequestCount()
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