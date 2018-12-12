using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class BufferProcessor
    {
        //private static readonly Logger LOG = LoggerFactory.getLogger(typeof(BufferProcessor));

        private Task executor;

        private PersistentProvider persistentProvider;

        private RestConnector restConnector;

        private IBufferRequestService service;

        public BufferProcessor(PersistentProvider persistentProvider, IBufferRequestService service,
            RestConnector restConnector)
        {
            this.persistentProvider = persistentProvider;
            this.service = service;
            this.restConnector = restConnector;
            //this.executor = Task.Factory.StartNew(new ThreadFactoryAnonymousInnerClass(this));
            executor = Task.Factory.StartNew(() => { });
        }

//	private class ThreadFactoryAnonymousInnerClass : ThreadFactory
//	{
//		private readonly BufferProcessor outerInstance;
//
//		public ThreadFactoryAnonymousInnerClass(BufferProcessor outerInstance)
//		{
//			this.outerInstance = outerInstance;
//		}
//
//		public override Thread newThread(ThreadStart r)
//		{
//			Thread thread = new Thread(r);
//			thread.Name = "buffering-process";
//			thread.Daemon = true;
//			return thread;
//		}
//	}

        public virtual void startProcessing()
        {
            //executor.Start(new RunnableAnonymousInnerClass(this));
        }

//	private class RunnableAnonymousInnerClass : ThreadStart
//	{
//		private readonly BufferProcessor outerInstance;
//
//		public RunnableAnonymousInnerClass(BufferProcessor outerInstance)
//		{
//			this.outerInstance = outerInstance;
//		}
//
//		public virtual void run()
//		{
//			try
//			{
//				while (!Thread.CurrentThread.IsAlive)
//				{
//					ProcessingRequest processingRequest = outerInstance.persistentProvider.poll();
//					outerInstance.service.addResponse(processingRequest.Id, sendRequest(processingRequest.Entity));
//
//				}
//			}
//			catch (Exception ex)
//			{
//				Exception cause = ex;
//
//				while (cause.InnerException != null)
//				{
//					cause = cause.InnerException;
//				}
//				if (cause is Exception)
//				{
//					Thread.CurrentThread.Interrupt();
//				}
//				else
//				{
//					throw ex;
//				}
//			}
//		}
//
//		private Result sendRequest(BufferedRequest httpPostRequest)
//		{
//			Result result = new Result();
//			while (true)
//			{
//				try
//				{
//					object response = doSendRequest(httpPostRequest);
//					result.Response = response;
//					return result;
//				}
//				catch (SDKException e)
//				{
//
//					if (e.HttpStatus <= 500 && !e.Message.Contains(NO_ERROR_REPRESENTATION))
//					{
//						result.Exception = e;
//						return result;
//					}
//					//platform is down
//					//LOG.warn("Couldn't connect to platform. Waiting..." + e.Message);
//					waitForPlatform();
//				}
//				//catch (ClientHandlerException e)
//				catch (Exception e)
//				{
//					//if (e.InnerException != null && e.InnerException is ConnectException)
//					if (e.InnerException != null && e.InnerException is Exception)
//					{
//						//lack of connection
//						//LOG.warn("Couldn't connect to platform. Waiting..." + e.Message);
//						waitForConnection();
//					}
//					else
//					{
//						result.Exception = new Exception("Exception occured while processing buffered request: ", e);
//						return result;
//					}
//				}
////				catch (Exception e)
////				{
////					result.Exception = new Exception("Exception occured while processing buffered request: ", e);
////					return result;
////				}
//			}
//		}
//
//		private void waitForPlatform()
//		{
//			try
//			{
//				Thread.Sleep(5 * 60 * 1000);
//			}
//			catch (Exception e)
//			{
//				throw new Exception("", e);
//			}
//		}
//
//		private void waitForConnection()
//		{
//			try
//			{
//				Thread.Sleep(30 * 1000);
//			}
//			catch (Exception e)
//			{
//				throw new Exception("", e);
//			}
//		}
//
//		private object doSendRequest(BufferedRequest httpPostRequest)
//		{
//			string method = httpPostRequest.Method;
//			if (string.ReferenceEquals(method, HttpMethod.Post))
//			{
//				return outerInstance.restConnector.Post(httpPostRequest.Path, httpPostRequest.MediaType, httpPostRequest.Representation);
//			}
//			else if (string.ReferenceEquals(method, HttpMethod.Put))
//			{
//				return outerInstance.restConnector.put(httpPostRequest.Path, httpPostRequest.MediaType, httpPostRequest.Representation);
//			}
//			else
//			{
//				throw new System.ArgumentException("This method is not supported in buffering processor: " + method);
//			}
//		}
//	}

        public virtual void shutdown()
        {
            //executor.shutdownNow();
        }
    }
}