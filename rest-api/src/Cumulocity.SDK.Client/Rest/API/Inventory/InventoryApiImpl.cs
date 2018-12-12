using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

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

	//Method 'throws' clauses are not available in .NET:
	//ORIGINAL LINE: @Override public ManagedObjectRepresentation create(ManagedObjectRepresentation representation) throws SDKException
	public  ManagedObjectRepresentation create(ManagedObjectRepresentation representation)
	{
		return restConnector.Post(MOCollectionUrl, InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
	}

	IManagedObjectCollection IInventoryApi.ManagedObjects => ManagedObjects;

	//ORIGINAL LINE: @Override public ManagedObjectRepresentation get(GId id) throws SDKException
	 public  ManagedObjectRepresentation get(GId id)
	 {
		 return restConnector.Get<ManagedObjectRepresentation>(MOCollectionUrl + "/" + id.Value,
			 InventoryMediaType.GetInstance.MANAGED_OBJECT, typeof(ManagedObjectRepresentation));
	 }

	//ORIGINAL LINE: @Override public void delete(GId id) throws SDKException
	public ManagedObjectRepresentation Get(GId id)
	{
		throw new NotImplementedException();
	}

	public void delete(GId id)
	 {
		 restConnector.Delete(MOCollectionUrl + "/" + id.Value);
	 }

	//ORIGINAL LINE: @Override public ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation) throws SDKException
	 public  ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation)
	 {
		 return (ManagedObjectRepresentation )restConnector.Put(MOCollectionUrl + "/" + managedObjectRepresentation.Id.Value, InventoryMediaType.GetInstance.MANAGED_OBJECT, managedObjectRepresentation);
	 }

	//ORIGINAL LINE: @Override public ManagedObject getManagedObject(GId globalId) throws SDKException
	IManagedObjectCollection IInventoryApi.getManagedObjectsByListOfIds(IList<GId> ids)
	{
		throw new NotImplementedException();
	}

	public  IManagedObject getManagedObject(GId globalId)
	{
		return getManagedObjectApi(globalId);
	}

	//ORIGINAL LINE: @Override public ManagedObject getManagedObjectApi(GId globalId) throws SDKException
	public  IManagedObject getManagedObjectApi(GId globalId)
	{
		if ((globalId == null) || (globalId.Value == null))
		{
			throw new SDKException("Cannot determine the Global ID Value");
		}
		string url = MOCollectionUrl + "/" + globalId.Value;
		return new ManagedObjectImpl(restConnector, url, pageSize);
	}

	//ORIGINAL LINE: @Override public ManagedObjectCollection getManagedObjects() throws SDKException
	public  IManagedObjectCollection ManagedObjects
	{
		get
		{
			string url = MOCollectionUrl;
			return (IManagedObjectCollection) new ManagedObjectCollectionImpl(restConnector, url, pageSize);
		}
	}

	//ORIGINAL LINE: @Override public ManagedObjectCollection getManagedObjectsByFilter(InventoryFilter filter) throws SDKException
	public IManagedObjectCollection getManagedObjectsByFilter(InventoryFilter filter)
	{
		if (filter == null)
		{
			return ManagedObjects;
		}
		IDictionary<string, string> @params = filter.QueryParams;
		return (IManagedObjectCollection) new ManagedObjectCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(MOCollectionUrl, @params), pageSize);
	}

	//	IManagedObjectCollection IInventoryApi.getManagedObjectsByFilter(InventoryFilter filter)
	//	{
	//		throw new NotImplementedException();
	//	}
	

	[Obsolete]
	public  IManagedObjectCollection getManagedObjectsByListOfIds(IList<GId> ids)
	{
		return getManagedObjectsByFilter((new InventoryFilter()).byIds(ids));
	}

	//ORIGINAL LINE: protected String getMOCollectionUrl() throws SDKException
	protected internal virtual string MOCollectionUrl
	{
		get
		{
			return InventoryRepresentation.ManagedObjects.Self;
		}
	}

	//ORIGINAL LINE: private InventoryRepresentation getInventoryRepresentation() throws SDKException
	private InventoryRepresentation InventoryRepresentation
	{
		get
		{
			return inventoryRepresentation;
		}
	}
}
}