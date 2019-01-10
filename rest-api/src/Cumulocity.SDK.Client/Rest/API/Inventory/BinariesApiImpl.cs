using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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


		//ORIGINAL LINE: @Override public ManagedObjectRepresentation replaceFile(GId containerId, String contentType, InputStream fileStream) throws SDKException
		public  ManagedObjectRepresentation replaceFile(GId containerId, string contentType, Stream fileStream)
		{
			return restConnector.putStream<ManagedObjectRepresentation>(BinariesUrl + V + containerId.Value, contentType, fileStream, typeof(ManagedObjectRepresentation));
		}

		//ORIGINAL LINE: @Override public void deleteFile(GId containerId) throws SDKException
		public void deleteFile(GId containerId)
		{
			restConnector.Delete(BinariesUrl + V + containerId.Value);
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		public Stream downloadFile(GId id)
		{
			return restConnector.Get<Stream>(BinariesUrl + V + id.Value, MediaType.APPLICATION_OCTET_STREAM_TYPE,  typeof(Stream));
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
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
