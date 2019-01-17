using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class BufferProcessor
    {
        //private static readonly Logger LOG = LoggerFactory.getLogger(typeof(BufferProcessor));

        private readonly Thread executor;

        private readonly PersistentProvider persistentProvider;

        private readonly RestConnector restConnector;

        private readonly IBufferRequestService service;

        public BufferProcessor(PersistentProvider persistentProvider, IBufferRequestService service,
            RestConnector restConnector)
        {
            this.persistentProvider = persistentProvider;
            this.service = service;
            this.restConnector = restConnector;
            executor = new Thread(() => RunnableInnerClass.run(this))
	            { IsBackground = true};
        }



		public virtual void startProcessing()
        {
			executor.Start();

		}

		private class RunnableInnerClass 
		{
			public static void run(BufferProcessor outerInstance)
			{
				try
				{
					while (!Thread.CurrentThread.IsAlive)
					{
						ProcessingRequest processingRequest = outerInstance.persistentProvider.poll();
						outerInstance.service.addResponse(processingRequest.Id, sendRequest(processingRequest.Entity, outerInstance));

					}
				}
				catch (Exception ex)
				{
					Exception cause = ex;

					while (cause.InnerException != null)
					{
						cause = cause.InnerException;
					}
					if (cause is Exception)
					{
						Thread.CurrentThread.Interrupt();
					}
					else
					{
						throw ex;
					}
				}
			}

			private static Result sendRequest(BufferedRequest httpPostRequest, BufferProcessor outerInstance)
			{
				Result result = new Result();
				while (true)
				{
					try
					{
						object response = doSendRequest(httpPostRequest, outerInstance);
						result.Response = response;
						return result;
					}
					catch (SDKException e)
					{

						if (e.HttpStatus <= 500 && !e.Message.Contains(ResponseParser.NO_ERROR_REPRESENTATION))
						{
							result.Exception = e;
							return result;
						}
						//platform is down
						//LOG.warn("Couldn't connect to platform. Waiting..." + e.Message);
						waitForPlatform();
					}
					//catch (ClientHandlerException e)
					catch (Exception e)
					{
						//if (e.InnerException != null && e.InnerException is ConnectException)
						if (e.InnerException != null && e.InnerException is Exception)
						{
							//lack of connection
							//LOG.warn("Couldn't connect to platform. Waiting..." + e.Message);
							waitForConnection();
						}
						else
						{
							result.Exception = new Exception("Exception occured while processing buffered request: ", e);
							return result;
						}
					}
				}
			}

			private static void waitForPlatform()
			{
				try
				{
					Thread.Sleep(5 * 60 * 1000);
				}
				catch (Exception e)
				{
					throw new Exception("", e);
				}
			}

			private static void waitForConnection()
			{
				try
				{
					Thread.Sleep(30 * 1000);
				}
				catch (Exception e)
				{
					throw new Exception("", e);
				}
			}

			private static object doSendRequest(BufferedRequest httpPostRequest, BufferProcessor outerInstance)
			{
				string method = httpPostRequest.Method;
				if (string.ReferenceEquals(method, HttpMethod.Post))
				{
					return outerInstance.restConnector.Post(httpPostRequest.Path, httpPostRequest.MediaType, httpPostRequest.Representation);
				}
				else if (string.ReferenceEquals(method, HttpMethod.Put))
				{
					return outerInstance.restConnector.Put(httpPostRequest.Path, httpPostRequest.MediaType, httpPostRequest.Representation);
				}
				else
				{
					throw new System.ArgumentException("This method is not supported in buffering processor: " + method);
				}
			}
		}

		public virtual void shutdown()
		{
			executor.Stop();
		}
    }
}