using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public interface BinariesApi
	{

		//ORIGINAL LINE: ManagedObjectRepresentation uploadFile(ManagedObjectRepresentation container, byte[] bytes) throws SDKException;
		ManagedObjectRepresentation uploadFile(ManagedObjectRepresentation container, sbyte[] bytes);

		//ORIGINAL LINE: ManagedObjectRepresentation replaceFile(GId containerId, String contentType, InputStream fileStream) throws SDKException;
		ManagedObjectRepresentation replaceFile(GId containerId, string contentType, Stream fileStream);

		//ORIGINAL LINE: InputStream downloadFile(GId id) throws SDKException;
		Stream downloadFile(GId id);

		//ORIGINAL LINE: void deleteFile(GId containerId) throws SDKException;
		void deleteFile(GId containerId);

	}
}
