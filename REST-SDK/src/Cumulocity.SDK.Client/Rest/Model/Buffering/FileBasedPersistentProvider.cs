using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class FileBasedPersistentProvider : PersistentProvider
    {
        protected static readonly string NEW_REQUESTS_PATH = "new-requests";

        protected static readonly string NEW_REQUESTS_TEMP_PATH = "new-requests-temp";

        protected static readonly int MAX_QUEUE_SIZE = 10;

        private long _counter = 1;

        public long Counter { get => _counter; }

        private volatile CountdownEvent latch = new CountdownEvent(1);

        private Queue<ProcessingRequest> requests = new Queue<ProcessingRequest>();

        private DirectoryInfo newRequestsTemp;
        private DirectoryInfo newRequests;

        public FileBasedPersistentProvider(string filePath) : this(DEFAULT_BUFFER_LIMIT, filePath)
        {
        }

        public FileBasedPersistentProvider(long bufferLimit, string filePath) : base(bufferLimit)
        {
            newRequestsTemp = new DirectoryInfo(Path.Combine(filePath, NEW_REQUESTS_TEMP_PATH));
            newRequests = new DirectoryInfo(Path.Combine(filePath, NEW_REQUESTS_PATH));
            setup();
        }

        private void setup()
        {
            if(!newRequestsTemp.Exists)
            {
                newRequestsTemp.Create();
            }
            if (newRequests.Exists)
            {
                initRequestIdCounter();
            }
            else
            {
                newRequests.Create();
            }
        }

        private void initRequestIdCounter()
        {
            FileInfo[] incomingFiles = getIncomingFilesSorted();
            if(incomingFiles.Length > 0)
            {
                long lastRequestId = long.Parse(incomingFiles[incomingFiles.Length - 1].Name);
                Interlocked.Exchange(ref _counter, lastRequestId + 10);
            }
        }

        private FileInfo[] getIncomingFilesSorted()
        {
            FileInfo[] files = newRequests.GetFiles();
            Array.Sort(files, delegate (FileInfo fi1, FileInfo fi2) { return fi1.Name.CompareTo(fi2.Name); });
            return files;
        }

        public override long generateId()
        {
            return Interlocked.Increment(ref _counter);
        }

        public override void offer(ProcessingRequest request)
        {
            if(newRequests.GetFiles().Length >= bufferLimit)
            {
                throw new InvalidOperationException("Queue is full");
            }
            String fileName = $"{request.Id}";
            writeToFile(request.Entity.toCsvString(), new FileStream(Path.Combine(newRequestsTemp.FullName, fileName), FileMode.Create, FileAccess.ReadWrite));
            moveFile(fileName, newRequestsTemp, newRequests);
            latch.Signal();
        }

        private void writeToFile(string content, FileStream fileStream)
        {
            try
            {
                fileStream.Write(Encoding.UTF8.GetBytes(content), 0, Encoding.UTF8.GetBytes(content).Length);
            }
            catch(IOException ex)
            {
                throw new IOException("I/O error", ex);
            }
            finally
            {
                fileStream.Close();
            }
        }

        private void moveFile(String fileName, DirectoryInfo fromQueue, DirectoryInfo toQueue)
        {
            FileInfo from = new FileInfo(Path.Combine(fromQueue.FullName, fileName));
            FileInfo to = new FileInfo(Path.Combine(toQueue.FullName, fileName));
            from.MoveTo(to.FullName);
        }

        public override ProcessingRequest poll()
        {
            if(requests.Count() == 0)
            {
                readFilesToQueue();
            }

            if(requests.Count() == 0)
            {
                waitForRequest();
                readFilesToQueue();
                latch = new CountdownEvent(1);
            }
            return requests.Dequeue();
        }

        private void waitForRequest()
        {
            try
            {
                latch.Wait();
            }
            catch(ThreadInterruptedException ex)
            {
                throw new Exception($"{ex}");
            }
        }

        private void readFilesToQueue()
        {
            FileInfo[] files = getIncomingFilesSorted();
            foreach(var file in files)
            {
                requests.Enqueue(readFromFile(file));
                file.Delete();
                if (requests.Count() == MAX_QUEUE_SIZE)
                    break;
            }
        }

        private ProcessingRequest readFromFile(FileInfo file)
        {
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            string content;
            using (StreamReader reader = new StreamReader(fs))
            {
                content = reader.ReadToEnd();
            }
            return ProcessingRequest.parse(file.Name, content);
        }
    }
}
