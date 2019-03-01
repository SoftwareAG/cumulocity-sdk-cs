using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System.IO;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public class BinariesApiImpl : IBinariesApi
	{
		private const string V = "/";
		private readonly RestConnector restConnector;
		private readonly InventoryRepresentation inventoryRepresentation;

		public BinariesApiImpl(RestConnector restConnector, InventoryRepresentation inventoryRepresentation)
		{
			this.restConnector = restConnector;
			this.inventoryRepresentation = inventoryRepresentation;
		}

		public ManagedObjectRepresentation UploadFile(ManagedObjectRepresentation container, byte[] bytes)
		{
			return restConnector.postFile<ManagedObjectRepresentation>(BinariesUrl, container, bytes, container);
		}

		public ManagedObjectRepresentation ReplaceFile(GId containerId, string contentType, Stream fileStream)
		{
			return restConnector.putStream<ManagedObjectRepresentation>(BinariesUrl + V + containerId.Value, contentType, fileStream, typeof(ManagedObjectRepresentation));
		}

		public void DeleteFile(GId containerId)
		{
			restConnector.Delete(BinariesUrl + V + containerId.Value);
		}

		public Task<Stream> DownloadFile(GId id)
		{
			return restConnector.GetStream(BinariesUrl + V + id.Value, MediaType.APPLICATION_OCTET_STREAM_TYPE);
		}

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