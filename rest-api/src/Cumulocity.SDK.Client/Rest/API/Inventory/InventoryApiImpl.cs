using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public class InventoryApiImpl : IInventoryApi//<T> where T : ManagedObjectCollectionRepresentation
	{
		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private InventoryRepresentation inventoryRepresentation;

		private UrlProcessor urlProcessor;
		private IManagedObjectCollection _managedObjects;

		public InventoryApiImpl(RestConnector restConnector, UrlProcessor urlProcessor, InventoryRepresentation inventoryRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.urlProcessor = urlProcessor;
			this.inventoryRepresentation = inventoryRepresentation;
			this.pageSize = pageSize;
		}

		public ManagedObjectRepresentation Create(ManagedObjectRepresentation representation)
		{
			return restConnector.Post(MOCollectionUrl, InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
		}

		IManagedObjectCollection IInventoryApi.ManagedObjects => ManagedObjects;

		public ManagedObjectRepresentation Get(GId id)
		{
			return restConnector.Get<ManagedObjectRepresentation>($"{MOCollectionUrl}/{id.Value}",
				InventoryMediaType.GetInstance.MANAGED_OBJECT, typeof(ManagedObjectRepresentation));
		}



		public void Delete(GId id)
		{
			restConnector.Delete($"{MOCollectionUrl}/{id.Value}");
		}

		public ManagedObjectRepresentation Update(ManagedObjectRepresentation managedObjectRepresentation)
		{
			return restConnector.Put($"{MOCollectionUrl}/{managedObjectRepresentation.Id.Value}", InventoryMediaType.GetInstance.MANAGED_OBJECT, managedObjectRepresentation);
		}

		public IManagedObject GetManagedObject(GId globalId)
		{
			return GetManagedObjectApi(globalId);
		}

		public IManagedObject GetManagedObjectApi(GId globalId)
		{
			if ((globalId == null) || (globalId.Value == null))
			{
				throw new SDKException("Cannot determine the Global ID Value");
			}
			string url = $"{MOCollectionUrl}/{globalId.Value}";
			return new ManagedObjectImpl(restConnector, url, pageSize);
		}

		public IManagedObjectCollection ManagedObjects
		{
			get
			{
				string url = MOCollectionUrl;
				return (IManagedObjectCollection)new ManagedObjectCollectionImpl(restConnector, url, pageSize);
			}
		}

		public IManagedObjectCollection GetManagedObjectsByFilter(InventoryFilter filter)
		{
			if (filter == null)
			{
				return ManagedObjects;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return (IManagedObjectCollection)new ManagedObjectCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(MOCollectionUrl, @params), pageSize);
		}

		[Obsolete]
		public IManagedObjectCollection GetManagedObjectsByListOfIds(IList<GId> ids)
		{
			return GetManagedObjectsByFilter((new InventoryFilter()).ByIds(ids));
		}

		protected internal virtual string MOCollectionUrl
		{
			get
			{
				return InventoryRepresentation.ManagedObjects.Self;
			}
		}

		private InventoryRepresentation InventoryRepresentation
		{
			get
			{
				return inventoryRepresentation;
			}
		}
	}
}