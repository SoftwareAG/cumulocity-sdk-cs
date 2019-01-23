using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class BufferProcessor: IDisposable
    {

        private readonly PersistentProvider persistentProvider;
        private readonly RestConnector restConnector;
        private readonly IBufferRequestService service;

        private CancellationTokenSource _tokenSource = null;
        private readonly CancellationToken _token;
        private Task _executionTask = null;
        private bool disposedValue = false;

		public BufferProcessor(PersistentProvider persistentProvider, IBufferRequestService service,
            RestConnector restConnector)
        {
            this.persistentProvider = persistentProvider;
            this.service = service;
            this.restConnector = restConnector;
			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
		}



		public Task StartProcessing()
        {
	        _executionTask = Task.Factory.StartNew(this.LongRunningTask, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
	        return Task.FromResult(0);

		}
		public Task Shutdown()
		{
			Dispose();
			return Task.FromResult(0);
		}

		public void Dispose(bool disposing)
		{
			try
			{
				if (!disposedValue)
				{
					if (disposing)
					{
						_tokenSource.Cancel();
						_executionTask.Wait();
						_tokenSource.Dispose();
						_executionTask.Dispose();
					}

					disposedValue = true;
				}
			}
			finally
			{
				_tokenSource = null;
				_executionTask = null;
			}
		}

		void Dispose() => Dispose(true);

		private void LongRunningTask()
		{
			while (true)
			{

					if (_token.IsCancellationRequested)
					{
						_token.ThrowIfCancellationRequested();
					}
				// do long running job here
				ProcessingRequest processingRequest = persistentProvider.poll();
				service.addResponse(processingRequest.Id, SendRequest(processingRequest.Entity));

			}
		}
		private  Result SendRequest(BufferedRequest httpPostRequest)
		{
			Result result = new Result();
			while (true)
			{
				try
				{
					object response = DoSendRequest(httpPostRequest);
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
					WaitForPlatform();
				}
				//catch (ClientHandlerException e)
				catch (Exception e)
				{
					//if (e.InnerException != null && e.InnerException is ConnectException)
					if (e.InnerException != null && e.InnerException is Exception)
					{
						//lack of connection
						//LOG.warn("Couldn't connect to platform. Waiting..." + e.Message);
						WaitForConnection();
					}
					else
					{
						result.Exception = new Exception("Exception occured while processing buffered request: ", e);
						return result;
					}
				}
			}
		}
		private  object DoSendRequest(BufferedRequest httpPostRequest)
		{
			string method = httpPostRequest.Method;
			if (HttpMethod.Post.Method.Equals(method))
			{
				return restConnector.Post<AlarmRepresentation>(httpPostRequest.Path, httpPostRequest.MediaType, httpPostRequest.Representation);
			}
			else if (HttpMethod.Put.Method.Equals(method))
			{
				return restConnector.PutWithoutId(httpPostRequest.Path, httpPostRequest.MediaType, httpPostRequest.Representation);
			}
			else
			{
				throw new System.ArgumentException("This method is not supported in buffering processor: " + method);
			}
		}

		private static void WaitForPlatform()
		{

				Thread.Sleep(5 * 60 * 1000);

		}

		private static void WaitForConnection()
		{
				Thread.Sleep(30 * 1000);

		}

		void IDisposable.Dispose()
		{
			
		}

    }
}