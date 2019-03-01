using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class TenantApiRepresentation : AbstractExtensibleRepresentation
	{

		private TenantCollectionRepresentation tenants;
		private OptionCollectionRepresentation options;

		private string tenantForId;

		public TenantCollectionRepresentation Tenants
		{
			get => tenants;
			set => tenants = value;
		}

		public OptionCollectionRepresentation Options
		{
			get => options;
			set => options = value;
		}

		public string TenantForId
		{
			get => tenantForId;
			set => tenantForId = value;
		}

		public string TenantApplications
		{
			get => tenantApplications;
			set => tenantApplications = value;
		}

		public string TenantApplicationForId
		{
			get => tenantApplicationForId;
			set => tenantApplicationForId = value;
		}

		public string TenantOptionForCategoryAndKey
		{
			get => tenantOptionForCategoryAndKey;
			set => tenantOptionForCategoryAndKey = value;
		}

		public string TenantOptionsForCategory
		{
			get => tenantOptionsForCategory;
			set => tenantOptionsForCategory = value;
		}

		private string tenantApplications;
		private string tenantApplicationForId;

		private string tenantOptionForCategoryAndKey;
		private string tenantOptionsForCategory;

		//ORIGINAL LINE: @JSONProperty(ignore = true) public List<String> getURITemplates()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<string> URITemplates
		{
			get
			{
				IList<string> uriTemplates = new List<string>();
				uriTemplates.Add(this.TenantForId);
				uriTemplates.Add(this.TenantApplications);
				uriTemplates.Add(this.TenantApplicationForId);
				uriTemplates.Add(this.TenantOptionsForCategory);
				uriTemplates.Add(this.TenantOptionForCategoryAndKey);
				uriTemplates.Add(this.TenantOptionsForCategory);
				return uriTemplates;
			}
		}

	}
}
