using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Inventory
{
	public class BinariesApiIT: IClassFixture<BinariesApiFixture>,IDisposable
	{
		private readonly IBinariesApi binariesApi;
		private readonly string logoX200XfinalJpg;
		private readonly string imageJpg;
		private string destPath;

		public BinariesApiIT(BinariesApiFixture binariesApiFixture)
		{
			this.binariesApi = binariesApiFixture.platform.BinariesApi;
			logoX200XfinalJpg = "logo_200x200xfinal.jpg";
			imageJpg = "image/jpg";
			destPath = "test.jpg";
		}

		[Fact]
		public void ReplaceFiles()
		{
			//Given
			var mor = createManagedObjectRepresentation();
			var uploadedFile = binariesApi.uploadFile(mor, ReadFile(logoX200XfinalJpg));
			//When
			var replacedFile = binariesApi.replaceFile(uploadedFile.Id, imageJpg, new MemoryStream(ReadFile(logoX200XfinalJpg)));
			//Then
			Assert.NotNull(replacedFile);
		}

		[Fact]
		public void CheckUploadFile()
		{
			//Given
			var mor = createManagedObjectRepresentation();
			//When
			var result = binariesApi.uploadFile(mor,ReadFile(logoX200XfinalJpg));
			//Then
			Assert.NotNull(result);
			Assert.Equal( mor.Name, result.Name);

			binariesApi.deleteFile(result.Id);
		}


		[Fact]
		public void DeleteFile()
		{
			//Given
			var mor = createManagedObjectRepresentation();
			var uploadFile = binariesApi.uploadFile(mor, ReadFile(logoX200XfinalJpg));
			//When
			binariesApi.deleteFile(uploadFile.Id);
			//binariesApi.deleteFile(new GId("155951"));
			//Then
			Assert.NotNull(uploadFile);
		}

		[Fact]
		public void DownloadFile()
		{
			//Given
			var mor = createManagedObjectRepresentation();
			var uploadFile = binariesApi.uploadFile(mor, ReadFile(logoX200XfinalJpg));
			//When
			DownloadFileAsync(uploadFile.Id).Wait();
			//Then
			Assert.True(File.Exists(Path.Combine(GetDict(), destPath)));
		}

		private async Task DownloadFileAsync(GId uploadFileId)
		{
			var data = await binariesApi.downloadFile(uploadFileId);
			CopyStream(data, destPath);
		}

		private byte[] ReadFile(string fileName)
		{
			var path = GetPath(fileName);
			return ReadAllBytes(path);
		}

		private static string GetPath(string fileName)
		{
			var dirPath = GetDict();
			var filePath = Path.Combine(dirPath, $"Resources/Inventory/{fileName}");
			return filePath;
		}

		private static string GetDict()
		{
			var location = typeof(BinariesApiIT).GetTypeInfo().Assembly.Location;
			var dirPath = Path.GetDirectoryName(location);
			return dirPath;
		}

		private byte[] ReadAllBytes(string fileName)
		{
			byte[] buffer = null;
			using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				buffer = new byte[fs.Length];
				fs.Read(buffer, 0, (int)fs.Length);
			}
			return buffer;
		}

		private void CopyStream(Stream stream, string destPath)
		{
			using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
			{
				stream.CopyTo(fileStream);
			}
		}

		private ManagedObjectRepresentation createManagedObjectRepresentation()
		{
			ManagedObjectRepresentation mor = new ManagedObjectRepresentation();
			mor.Name = logoX200XfinalJpg;
			mor.Type = imageJpg;
			return mor;
		}

		public void Dispose()
		{
			if (File.Exists(Path.Combine(GetDict(), destPath)))
			{
				File.Delete(Path.Combine(GetDict(), destPath));
			}
		}
	}

}
