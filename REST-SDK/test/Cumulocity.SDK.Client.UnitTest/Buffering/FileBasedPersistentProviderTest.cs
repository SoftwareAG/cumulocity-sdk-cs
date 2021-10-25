using Cumulocity.SDK.Client.Rest.Model.Buffering;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Buffering
{
    public class FileBasedPersistentProviderTest
    {
        private string pathToTempFolder;
        private FileBasedPersistentProvider persistentProvider;
        private static readonly string NEW_REQUESTS_PATH = "new-requests";

        private static readonly string NEW_REQUESTS_TEMP_PATH = "new-requests-temp";

        public FileBasedPersistentProviderTest()
        {
            pathToTempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            persistentProvider = new FileBasedPersistentProvider(pathToTempFolder);
        }

        [Fact]
        public void shouldInitializeRequestIdCounter()
        {
            string path = Path.Combine(pathToTempFolder, NEW_REQUESTS_PATH, "13");
            File.Create(path);
            persistentProvider = new FileBasedPersistentProvider(pathToTempFolder);
            Assert.True(persistentProvider.Counter > 13);
        }

        [Fact]
        public void shouldPersistRequestToFile()
        {
            BufferedRequest request = BufferedRequest.create("POST", "test", AlarmMediaType.ALARM, new AlarmRepresentation());
            persistentProvider.offer(new ProcessingRequest(1, request));
            Assert.True(File.Exists(Path.Combine(pathToTempFolder, NEW_REQUESTS_PATH, "1")));
        }

        [Fact]
        public void shouldNotCreateAFileWhenQueueIsFull()
        {
            BufferedRequest request = BufferedRequest.create("POST", "test", AlarmMediaType.ALARM, new AlarmRepresentation());
            persistentProvider = new FileBasedPersistentProvider(1, pathToTempFolder);
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                persistentProvider.offer(new ProcessingRequest(1, request));
                persistentProvider.offer(new ProcessingRequest(2, request));
            });
            Assert.Equal("Queue is full", exception.Message);
        }

        [Fact]
        public void shouldReturnRequestFromQueue()
        {
            BufferedRequest request = BufferedRequest.create("POST", "test", AlarmMediaType.ALARM, new AlarmRepresentation());
            persistentProvider.offer(new ProcessingRequest(1, request));
            ProcessingRequest result = persistentProvider.poll();
            Assert.Equal(1, result.Id);
        }
    }
}
