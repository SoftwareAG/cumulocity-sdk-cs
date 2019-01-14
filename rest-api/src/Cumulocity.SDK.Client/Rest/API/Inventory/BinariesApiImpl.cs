using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public class BinariesApiImpl : IBinariesApi
	{
		private const string V = "/";
		private readonly RestConnector restConnector;

		private InventoryRepresentation inventoryRepresentation;

		public BinariesApiImpl(RestConnector restConnector, InventoryRepresentation inventoryRepresentation)
		{
			this.restConnector = restConnector;
			this.inventoryRepresentation = inventoryRepresentation;
		}

		//ORIGINAL LINE: @Override public ManagedObjectRepresentation uploadFile(ManagedObjectRepresentation container, byte[] bytes) throws SDKException
		public ManagedObjectRepresentation uploadFile(ManagedObjectRepresentation container, byte[] bytes)
		{
			return restConnector.postFile<ManagedObjectRepresentation>(BinariesUrl, container, bytes, container);
		}

		public  ManagedObjectRepresentation replaceFile(GId containerId, string contentType, Stream fileStream)
		{
			return restConnector.putStream<ManagedObjectRepresentation>(BinariesUrl + V + containerId.Value, contentType, fileStream, typeof(ManagedObjectRepresentation));
		}

		public void deleteFile(GId containerId)
		{
			restConnector.Delete(BinariesUrl + V + containerId.Value);
		}

		public Task<Stream> downloadFile(GId id)
		{
			return restConnector.GetStream(BinariesUrl + V + id.Value, MediaType.APPLICATION_OCTET_STREAM_TYPE);
		}

		//ORIGINAL LINE: private String getBinariesUrl() throws SDKException
		private string BinariesUrl
		{
			get
			{
				string url = inventoryRepresentation.ManagedObjects.Self;
				return url.Replace("managedObjects", "binaries");
			}
		}
	}
}
