using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
using Cumulocity.SDK.Client.Rest.Model.Option;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;

namespace Cumulocity.SDK.Client.Rest.API.Option
{
	using System.Collections.Generic;

	public class TenantOptionApiImpl : ITenantOptionApi
	{

		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private TenantApiRepresentation tenantApiRepresentation;

		public TenantOptionApiImpl(RestConnector restConnector, TenantApiRepresentation tenantApiRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.tenantApiRepresentation = tenantApiRepresentation;
			this.pageSize = pageSize;
		}

		//ORIGINAL LINE: @Override public OptionRepresentation getOption(OptionPK optionPK) throws SDKException
		public  OptionRepresentation getOption(OptionPK optionPK)
		{
			string url = TenantApiRepresentation.TenantOptionForCategoryAndKey.Replace("{category}", optionPK.Category).Replace("{key}", optionPK.Key);
			return restConnector.Get<OptionRepresentation>(url, OptionMediaType.OPTION, typeof(OptionRepresentation));
		}

		//ORIGINAL LINE: @Override public TenantOptionCollection getOptions() throws SDKException
		public  ITenantOptionCollection Options
		{
			get
			{
				string url = SelfUri;
				return new TenantOptionCollectionImpl(restConnector, url, pageSize);
			}
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: private String getSelfUri() throws SDKException
		private string SelfUri
		{
			get
			{
				return TenantApiRepresentation.Options.Self;
			}
		}

		//ORIGINAL LINE: @Override public OptionRepresentation save(OptionRepresentation representation) throws SDKException
		public  OptionRepresentation save(OptionRepresentation representation)
		{
				return restConnector.Post<OptionRepresentation>(SelfUri, OptionMediaType.OPTION, representation);		
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: @Override public Future saveAsync(OptionRepresentation representation) throws SDKException
		//public override Future<> saveAsync(OptionRepresentation representation)
		//{
		//	return restConnector.postAsync(SelfUri, OptionMediaType.OPTION, representation);
		//}


		//ORIGINAL LINE: @Override public void delete(OptionPK optionPK) throws SDKException
		public  void delete(OptionPK optionPK)
		{
			string url = TenantApiRepresentation.TenantOptionForCategoryAndKey.Replace("{category}", optionPK.Category).Replace("{key}", optionPK.Key);
			restConnector.Delete(url);
		}

		//ORIGINAL LINE: @Override public List<OptionRepresentation> getAllOptionsForCategory(String category) throws SDKException
		public  IList<OptionRepresentation> getAllOptionsForCategory(string category)
		{
			string url = TenantApiRepresentation.TenantOptionsForCategory.Replace("{category}", category);
			OptionsRepresentation optionsRepresentation = restConnector.Get<OptionsRepresentation>(url, CumulocityMediaType.APPLICATION_JSON_TYPE, typeof(OptionsRepresentation));

			return transformOptions(category, optionsRepresentation);
		}

		private TenantApiRepresentation TenantApiRepresentation
		{
			get
			{
				if (tenantApiRepresentation == null)
				{
					tenantApiRepresentation = buildTenantApiRepresentation();
				}
				return tenantApiRepresentation;
			}
		}

		private TenantApiRepresentation buildTenantApiRepresentation()
		{
			OptionCollectionRepresentation optionCollectionRepresentation = new OptionCollectionRepresentation();
			optionCollectionRepresentation.Self = "/tenant/options";

			TenantCollectionRepresentation tenants = new TenantCollectionRepresentation();
			tenants.Self = "/tenant/tenants";

			TenantApiRepresentation tenantApiRepresentation = new TenantApiRepresentation();
			tenantApiRepresentation.Options = optionCollectionRepresentation;
			tenantApiRepresentation.Tenants = tenants;
			tenantApiRepresentation.TenantApplicationForId = "/tenant/tenants/{tenantId}/applications/{applicationId}";
			tenantApiRepresentation.TenantApplications = "/tenant/tenants/{tenantId}/applications";
			tenantApiRepresentation.TenantForId = "/tenant/tenants/{tenantId}";
			tenantApiRepresentation.TenantOptionForCategoryAndKey = "/tenant/options/{category}/{key}";
			tenantApiRepresentation.TenantOptionsForCategory = "/tenant/options/{category}";
			return tenantApiRepresentation;
		}

		private IList<OptionRepresentation> transformOptions(string category, OptionsRepresentation optionsRepresentation)
		{
			IList<OptionRepresentation> options = new List<OptionRepresentation>();
			foreach (string key in optionsRepresentation.propertyNames())
			{
				options.Add(OptionRepresentation.asOptionRepresetation(category, key, optionsRepresentation.getProperty(key)));
			}
			return options;
		}

	}

}
