using Cumulocity.SDK.Client.Rest.Model.Buffering;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        [Fact]
        public void shouldReturnMultipleRequestsFromQueue()
        {
            List<ProcessingRequest> requests = new List<ProcessingRequest>();
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            Task.Factory.StartNew(() => {
                while (true)
                {
                    if(token.IsCancellationRequested)
                    {
                        break;
                    }
                    ProcessingRequest result = persistentProvider.poll();
                    requests.Add(result);
                }
            }, token);
            for (int offer = 0; offer < 5; offer++)
            {
                BufferedRequest request = BufferedRequest.create("POST", "test", AlarmMediaType.ALARM, new AlarmRepresentation());
                long id = offer + 1;
                persistentProvider.offer(new ProcessingRequest(id, request));
            }
            Thread.Sleep(3000);
            tokenSource.Cancel();
            Assert.Equal(5, requests.Count);
        }

        [Fact]
        public void shouldReturnMultipleRequestsFromQueue2()
        {
            List<ProcessingRequest> requests = new List<ProcessingRequest>();
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            for (int offer = 0; offer < 2; offer++)
            {
                BufferedRequest request = BufferedRequest.create("POST", "test", AlarmMediaType.ALARM, new AlarmRepresentation());
                long id = offer + 1;
                persistentProvider.offer(new ProcessingRequest(id, request));
            }
            Task.Factory.StartNew(() => {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    ProcessingRequest result = persistentProvider.poll();
                    requests.Add(result);
                }
            }, token);
            for (int offer = 2; offer < 7; offer++)
            {
                BufferedRequest request = BufferedRequest.create("POST", "test", AlarmMediaType.ALARM, new AlarmRepresentation());
                long id = offer + 1;
                persistentProvider.offer(new ProcessingRequest(id, request));
            }
            Thread.Sleep(3000);
            tokenSource.Cancel();
            Assert.Equal(7, requests.Count);
        }

        [Fact]
        public void testCounterIncrement()
        {
            persistentProvider.generateId();
            Assert.Equal(2, persistentProvider.Counter);
        }
    }
}
