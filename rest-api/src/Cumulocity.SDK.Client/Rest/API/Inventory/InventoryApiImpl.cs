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

		public ManagedObjectRepresentation create(ManagedObjectRepresentation representation)
		{
			return restConnector.Post(MOCollectionUrl, InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
		}

		IManagedObjectCollection IInventoryApi.ManagedObjects => ManagedObjects;

		public ManagedObjectRepresentation get(GId id)
		{
			return restConnector.Get<ManagedObjectRepresentation>($"{MOCollectionUrl}/{id.Value}",
				InventoryMediaType.GetInstance.MANAGED_OBJECT, typeof(ManagedObjectRepresentation));
		}

		public ManagedObjectRepresentation Get(GId id)
		{
			throw new NotImplementedException();
		}

		public void delete(GId id)
		{
			restConnector.Delete($"{MOCollectionUrl}/{id.Value}");
		}

		public ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation)
		{
			return restConnector.Put($"{MOCollectionUrl}/{managedObjectRepresentation.Id.Value}", InventoryMediaType.GetInstance.MANAGED_OBJECT, managedObjectRepresentation);
		}

		public IManagedObject getManagedObject(GId globalId)
		{
			return getManagedObjectApi(globalId);
		}

		public IManagedObject getManagedObjectApi(GId globalId)
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

		public IManagedObjectCollection getManagedObjectsByFilter(InventoryFilter filter)
		{
			if (filter == null)
			{
				return ManagedObjects;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return (IManagedObjectCollection)new ManagedObjectCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(MOCollectionUrl, @params), pageSize);
		}

		[Obsolete]
		public IManagedObjectCollection getManagedObjectsByListOfIds(IList<GId> ids)
		{
			return getManagedObjectsByFilter((new InventoryFilter()).byIds(ids));
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