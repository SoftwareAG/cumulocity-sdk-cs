using System;
using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.API.DeviceControl;
using Cumulocity.SDK.Client.Rest.API.Event;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.API.Inventory;
using Cumulocity.SDK.Client.Rest.API.Measurement;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Cumulocity.SDK.Client.Rest.Representation.Platform;

namespace Cumulocity.SDK.Client.Rest
{
    public class PlatformImpl : PlatformParameters, IPlatform, IDisposable
    {
        private const string PLATFORM_URL = "platform";

        public const string CLIENT_PARAMETERS = "platformParameters";

        public const string CUMULOCITY_PAGE_SIZE = "cumulocityPageSize";

        public const string CUMULOCITY_PASSWORD = "cumulocityPassword";

        public const string CUMULOCITY_USER = "cumulocityUser";

        public const string CUMULOCITY_TENANT = "cumulocityTenant";

        public const string CUMULOCITY_PORT = "cumulocityPort";

        public const string CUMOLOCITY_HOST = "cumolocityHost";

        public const string CUMOLOCITY_APPLICATION_KEY = "applicationKey";

        public const string CUMOLOCITY_PROXY_HOST = "proxyHost";

        public const string CUMULOCITY_PROXY_PORT = "proxyPort";

        public const string CUMULOCITY_PROXY_USER = "proxyUser";

        public const string CUMULOCITY_PROXY_PASSWORD = "proxyPassword";


        private PlatformApiRepresentation platformApiRepresentation;

        public PlatformImpl(string host, CumulocityCredentials credentials) : base(host, credentials,
            new ClientConfiguration())
        {
        }

        public PlatformImpl(string host, CumulocityCredentials credentials, ClientConfiguration clientConfiguration) :
            base(host, credentials, clientConfiguration)
        {
        }

        public PlatformImpl(string host, int port, CumulocityCredentials credentials) : base(getHostUrl(host, port),
            credentials, new ClientConfiguration())
        {
        }

        public PlatformImpl(string host, CumulocityCredentials credentials, int pageSize) : base(host, credentials,
            new ClientConfiguration(), pageSize)
        {
        }

        public PlatformImpl(string host, CumulocityCredentials credentials, ClientConfiguration clientConfiguration,
            int pageSize) : base(host, credentials, clientConfiguration, pageSize)
        {
        }

        public PlatformImpl(string host, int port, CumulocityCredentials credentials, int pageSize) : base(
            getHostUrl(host, port), credentials, new ClientConfiguration(), pageSize)
        {
        }

        [Obsolete]
        public PlatformImpl(string host, string tenantId, string user, string password, string applicationKey) : base(
            host, new CumulocityCredentials(tenantId, user, password, applicationKey), new ClientConfiguration())
        {
        }

        [Obsolete]
        public PlatformImpl(string host, string tenantId, string user, string password, string applicationKey,
            int pageSize) : base(host, new CumulocityCredentials(tenantId, user, password, applicationKey),
            new ClientConfiguration(), pageSize)
        {
        }

        public PlatformImpl()
        {
            //empty constructor for spring based initialization
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public IRestOperations Rest()
        {
            throw new NotImplementedException();
        }

	    public  IInventoryApi InventoryApi
        {
            get
            {
                var restConnector = createRestConnector();
                return new InventoryApiImpl(restConnector, new UrlProcessor(), getPlatformApi(restConnector).Inventory , PageSize);
            }
        }

	    public IIdentityApi IdentityApi
	    {
		    get
		    {
			    var restConnector = createRestConnector();
				return new IdentityApiImpl(restConnector,new TemplateUrlParser(), getPlatformApi(restConnector).Identity, PageSize);
			}
	    }

	    public IEventApi EventApi {
		    get
		    {
			    var restConnector = createRestConnector();
			    return new EventApiImpl(restConnector, new UrlProcessor(), getPlatformApi(restConnector).Event, PageSize);
			}
	    }
	    public IAlarmApi AlarmApi
		{
		    get
		    {
			    var restConnector = createRestConnector();
			    return new AlarmApiImpl(restConnector, new UrlProcessor(), getPlatformApi(restConnector).Alarm, PageSize);
		    }
	    }

	    public IMeasurementApi MeasurementApi
		{
		    get
		    {
			    var restConnector = createRestConnector();
			    return new MeasurementApiImpl(restConnector, new UrlProcessor(), getPlatformApi(restConnector).Measurement, PageSize);
		    }
	    }

	    public IDeviceCredentialsApi DeviceCredentialsApi
		{
		    get
		    {
			    var restConnector = createRestConnector();
			    return new DeviceCredentialsApiImpl(this,restConnector);
		    }
	    }

	    public IDeviceControlApi DeviceControlApi
		{
		    get
		    {
			    var restConnector = createRestConnector();
			    return new DeviceControlApiImpl(this,restConnector, new UrlProcessor(), getPlatformApi(restConnector).DeviceControl, PageSize);
		    }
	    }


		private static string getHostUrl(string host, int port)
        {
            return "http://" + host + ":" + port;
        }

        //ORIGINAL LINE: public static Platform createPlatform() throws SDKException
        public IPlatform CreatePlatform()
        {
            //TODO: Add the singleton pattern 
            PlatformImpl platform = null;
            try
            {
                var host = GetProperty(CUMOLOCITY_HOST);
                var port = int.Parse(GetProperty(CUMULOCITY_PORT));
                var tenantId = GetProperty(CUMULOCITY_TENANT);
                var user = GetProperty(CUMULOCITY_USER);
                var password = GetProperty(CUMULOCITY_PASSWORD);
                var applicationKey = GetProperty(CUMOLOCITY_APPLICATION_KEY);
                if (ReferenceEquals(host, null) || ReferenceEquals(tenantId, null) || ReferenceEquals(user, null) ||
                    ReferenceEquals(password, null))
                    throw new SDKException("Cannot Create Platform as Mandatory Param are not set");
                if (GetProperty(CUMULOCITY_PAGE_SIZE) != null)
                {
                    var pageSize = int.Parse(GetProperty(CUMULOCITY_PAGE_SIZE));
                    platform = new PlatformImpl(host, port,
                        new CumulocityCredentials(tenantId, user, password, applicationKey), pageSize);
                }
                else
                {
                    platform = new PlatformImpl(host, port,
                        new CumulocityCredentials(tenantId, user, password, applicationKey));
                }

                var proxyHost = GetProperty(CUMOLOCITY_PROXY_HOST);
                var proxyPort = -1;
                if (GetProperty(CUMULOCITY_PROXY_PORT) != null)
                    proxyPort = int.Parse(GetProperty(CUMULOCITY_PROXY_PORT));
                var proxyUser = GetProperty(CUMULOCITY_PROXY_USER);
                var proxyPassword = GetProperty(CUMULOCITY_PROXY_PASSWORD);

                if (!ReferenceEquals(proxyHost, null) && proxyPort > 0)
                {
                    platform.ProxyHost = proxyHost;
                    platform.ProxyPort = proxyPort;
                }

                if (!ReferenceEquals(proxyUser, null) && !ReferenceEquals(proxyPassword, null))
                {
                    platform.ProxyUserId = proxyUser;
                    platform.ProxyPassword = proxyPassword;
                }
            }
            catch (FormatException e)
            {
                throw new SDKException("Invalid Number :" + e.Message);
            }

            return platform;
        }

        private string GetProperty(string propertyName)
        {
            return (string) GetType().GetProperty(propertyName).GetValue(this, null);
        }
        
        private PlatformApiRepresentation getPlatformApi(RestConnector restConnector)
        {
            lock (this)
            {
                if (platformApiRepresentation == null)
                {
                    platformApiRepresentation = restConnector.Get<PlatformApiRepresentation>(platformUrl(), PlatformMediaType.PLATFORM_API, typeof(PlatformApiRepresentation));
                }
                return platformApiRepresentation;
            }
        }
        
        private string platformUrl()
        {
            return Host + PLATFORM_URL;
        }
    }
}