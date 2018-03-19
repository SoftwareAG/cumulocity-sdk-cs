using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Interfaces;
using Cumulocity.MQTT.Utils;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cumulocity.MQTT
{
    /// <summary>
    /// Lightweight client for talking to an MQTT server
    /// </summary>
    /// <seealso cref="Cumulocity.MQTT.Interfaces.IClient" />
    public partial class Client : IClient
    {
        private readonly IConfiguration _config;
        private readonly IMqttClient _mqttClient;
        private readonly Dictionary<int, Func<List<string>, bool>> _storeSmartRest;
        private readonly Dictionary<int, Func<List<string>, bool>> _storeStaticOperations;
        private readonly Dictionary<int, Func<List<string>, bool>> _storeStaticOther;

        private enum ConnectionTypes
        {
            TCP,
            TLS,
            WS,
            WSS
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        public Client(IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.RollingFile(Path.Combine(Environment.CurrentDirectory, "MQTTClient-{Date}.txt"))
            .CreateLogger();
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _mqttClient = new MqttFactory().CreateMqttClient();
            _storeStaticOperations = AddOperationsToStore();
            _storeSmartRest = AddSmartRestToStore();
            _storeStaticOther = AddOtherStore();
            SubscribeEvt();
            DeviceCredentials = new DeviceCredentials(_mqttClient);
            MqttCustomSmartRest = new MqttCustomSmartRest(_mqttClient);
            MqttStaticAlarmTemplates = new MqttStaticAlarmTemplates(_mqttClient);
            MqttStaticEventTemplates = new MqttStaticEventTemplates(_mqttClient);
            MqttStaticInventoryTemplates = new MqttStaticInventoryTemplates(_mqttClient);
            MqttStaticMeasurementTemplates = new MqttStaticMeasurementTemplates(_mqttClient);
            MqttStaticOperationTemplates = new MqttStaticOperationTemplates(_mqttClient);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        public Client(IConfiguration config, IMqttClient mqttClient)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.RollingFile(Path.Combine(Environment.CurrentDirectory, "MQTTClient-{Date}.txt"))
            .CreateLogger();
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _mqttClient = mqttClient;
            _storeStaticOperations = AddOperationsToStore();
            _storeSmartRest = AddSmartRestToStore();
            _storeStaticOther = AddOtherStore();
            SubscribeEvt();
            DeviceCredentials = new DeviceCredentials(_mqttClient);
            MqttCustomSmartRest = new MqttCustomSmartRest(_mqttClient);
            MqttStaticAlarmTemplates = new MqttStaticAlarmTemplates(_mqttClient);
            MqttStaticEventTemplates = new MqttStaticEventTemplates(_mqttClient);
            MqttStaticInventoryTemplates = new MqttStaticInventoryTemplates(_mqttClient);
            MqttStaticMeasurementTemplates = new MqttStaticMeasurementTemplates(_mqttClient);
            MqttStaticOperationTemplates = new MqttStaticOperationTemplates(_mqttClient);
        }

        public IDeviceCredentials DeviceCredentials { get; }
        public IMqttCustomSmartRest MqttCustomSmartRest { get; }
        public IMqttStaticAlarmTemplates MqttStaticAlarmTemplates { get; }
        public IMqttStaticEventTemplates MqttStaticEventTemplates { get; }
        public IMqttStaticInventoryTemplates MqttStaticInventoryTemplates { get; }
        public IMqttStaticMeasurementTemplates MqttStaticMeasurementTemplates { get; }
        public IMqttStaticOperationTemplates MqttStaticOperationTemplates { get; }

        /// <summary>
        /// Lists all children of the device
        /// </summary>
        public event EventHandler<ChildrenOfDeviceEventArgs> ChildrenOfDeviceEvt;

        /// <summary>
        /// Tells the device to run the command send in the operation.
        /// </summary>
        public event EventHandler<CommandEventArgs> CommandEvt;

        /// <summary>
        /// Tells the device to change the communication mode.
        /// </summary>
        public event EventHandler<CommunicationModeEventArgs> CommunicationModeEvt;

        /// <summary>
        /// Tells the device to set the configuration send in the operation.
        /// </summary>
        public event EventHandler<ConfigurationEventArgs> ConfigurationEvt;

        /// <summary>
        /// Tells the device to download a configuration file from the url.
        /// </summary>
        public event EventHandler<DownloadConfigurationFileEventArgs> DownloadConfigurationFileEvt;

        /// <summary>
        /// Tells the device to install the firmware from the url.
        /// </summary>
        public event EventHandler<FirmwareEventArgs> FirmwareEvt;

        public event EventHandler<TemplateCollectionEventArgs> IsExistTemplateCollectionEvt;

        /// <summary>
        /// Tells the device to upload a log file for the given parameters.
        /// </summary>
        public event EventHandler<LogfileRequestEventArgs> LogfileRequestEvt;

        /// <summary>
        /// Tells the device to send the measurements specified by the request name.
        /// </summary>
        public event EventHandler<MeasurementRequestOperationEventArgs> MeasurementRequestOperationEvt;

        /// <summary>
        /// Tells the device either open or close the relays in the array.
        /// </summary>
        public event EventHandler<RelayArrayEventArgs> RelayArrayEvt;

        /// <summary>
        /// Tells the device to either open or close the relay
        /// </summary>
        public event EventHandler<RelayEventArgs> RelayEvt;

        public event EventHandler<DeviceCredentialsEventArgs> RequestDeviceCredentialEvt;
        /// <summary>
        /// Tells the device to restart.
        /// </summary>
        public event EventHandler<RestartEventArgs> RestartEvt;

        /// <summary>
        /// Tells the device to upload its current configuration
        /// </summary>
        public event EventHandler<SmartRestResponseEventArgs> SmartRestResponseEvt;

        /// <summary>
        /// Tells the device to install the software send in the operation.
        /// </summary>
        public event EventHandler<SoftwareListEventArgs> SoftwareListEvt;

        /// <summary>
        /// Tells the device to upload its current configuration
        /// </summary>
        public event EventHandler<UploadConfigurationFileEventArgs> UploadConfigurationFileEvt;

        /// <summary>
        /// An unexpected error occurred
        /// </summary>
        public event EventHandler<ErrorMessageEventArgs> ErrorEvt;

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected => _mqttClient.IsConnected;

        /// <summary>
        /// Connects the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ConnectAsync()
        {
            try
            {
                MqttClientOptions options = GetConnectOptions();
                await _mqttClient.ConnectAsync(options);
            }
            catch
            {
                Debug.WriteLine("### CONNECTING FAILED ###");
                return false;
            }

            return true;
        }


        /// <summary>
        /// Disconnects the asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<bool> DisconnectAsync()
        {
            return Task.FromResult(true);
        }

        private Dictionary<int, Func<List<string>, bool>> AddOperationsToStore()
        {
            return new Dictionary<int, Func<List<string>, bool>>
            {
                { 510, (msgs) => {
                    RestartEvt?.Invoke(msgs.ElementAt(1), new RestartEventArgs() { });
                    return true;
                } },
                { 511, (msgs) => {
                    CommandEvt?.Invoke(msgs.ElementAt(1), new CommandEventArgs()
                    {
                        CommandText = msgs.ElementAt(2)
                    });
                    return true;
                } },
                { 513, (msgs) => {
                    ConfigurationEvt?.Invoke(msgs.ElementAt(1), new ConfigurationEventArgs()
                    {
                        Configuration = msgs.ElementAt(2)
                    });
                    return true;
                } },
                { 515, (msgs) => {
                    FirmwareEvt?.Invoke(msgs.ElementAt(1), new FirmwareEventArgs()
                    {
                        FirmwareName = msgs.ElementAt(2),
                        FirmwareVersion = msgs.ElementAt(3),
                        Url = msgs.ElementAt(4)
                    });
                    return true;
                } },
                { 516, (msgs) => {
                    var softwareList = new List<Software>();

                    foreach (var batch in msgs.GetRange(2, msgs.Count - 2).Batch(3))
                    {
                        softwareList.Add(new Software()
                        {
                            Name = batch.ElementAt(0),
                            Version = batch.ElementAt(1),
                            Url = batch.ElementAt(2)
                        });
                    }

                    SoftwareListEvt?.Invoke(msgs.ElementAt(1), new SoftwareListEventArgs()
                    {
                        SoftwareList = softwareList
                    });
                    return true;
                } },
                { 517, (msgs) => {
                    MeasurementRequestOperationEvt?.Invoke(msgs.ElementAt(1), new MeasurementRequestOperationEventArgs()
                    {
                        RequestName = msgs.ElementAt(2)
                    });
                    return true;
                } },
                { 518, (msgs) => {
                    RelayEvt?.Invoke(msgs.ElementAt(1), new RelayEventArgs()
                    {
                        RelayState = msgs.ElementAt(2)
                    });
                    return true;
                } },
                { 519, (msgs) => {
                    var relayStates = new List<string>();

                    foreach (var state in msgs.GetRange(2, msgs.Count - 2))
                    {
                        relayStates.Add(state);
                    }

                    RelayArrayEvt?.Invoke(msgs.ElementAt(1), new RelayArrayEventArgs()
                    {
                        RelayStates = relayStates
                    });
                    return true;
                } },
                { 520, (msgs) => {
                    UploadConfigurationFileEvt?.Invoke(msgs.ElementAt(1), new UploadConfigurationFileEventArgs()
                    {
                    });
                    return true;
                } },
                { 521, (msgs) => {
                    DownloadConfigurationFileEvt?.Invoke(msgs.ElementAt(1), new DownloadConfigurationFileEventArgs()
                    {
                        Url = msgs.ElementAt(2)
                    });
                    return true;
                } },
                { 522, (msgs) => {
                    LogfileRequestEvt?.Invoke(msgs.ElementAt(1), new LogfileRequestEventArgs()
                    {
                        LogFileName = msgs.ElementAt(2),
                        StartDate = msgs.ElementAt(3),
                        EndDate = msgs.ElementAt(4),
                        SearchText = msgs.ElementAt(5),
                        MaximumLines = msgs.ElementAt(6)
                    });
                    return true;
                } },
                { 523, (msgs) => {
                    CommunicationModeEvt?.Invoke(msgs.ElementAt(1), new CommunicationModeEventArgs()
                    {
                        Mode = msgs.ElementAt(2)
                    });
                    return true;
                } },
                { 106, (msgs) => {
                    var childrenOfDevice = new List<string>();

                    foreach (var child in msgs.GetRange(1, msgs.Count - 1))
                    {
                        childrenOfDevice.Add(child);
                    }

                    ChildrenOfDeviceEvt?.Invoke(msgs.ElementAt(1), new ChildrenOfDeviceEventArgs()
                    {
                        ChildrenOfDevice = childrenOfDevice
                    });
                    return true;
                } },
                { 20, (msgs) => {
                    IsExistTemplateCollectionEvt?.Invoke(this, new TemplateCollectionEventArgs()
                    {
                        TemplateCollectionName = msgs.ElementAt(1),
                        IsExist = true,
                        IdCollection = Int32.Parse(msgs.ElementAt(2))
                    });
                    return true;
                } },
                { 41, (msgs) => {
                    IsExistTemplateCollectionEvt?.Invoke(this, new TemplateCollectionEventArgs()
                    {
                        TemplateCollectionName = msgs.ElementAt(1),
                        IsExist = false,
                        IdCollection = 0
                    });
                    return true;
                } }
            };
        }

        private Dictionary<int, Func<List<string>, bool>> AddOtherStore()
        {
            return new Dictionary<int, Func<List<string>, bool>>
            {
                { 70, (msgs) => {
                    RequestDeviceCredentialEvt?.Invoke(this, new DeviceCredentialsEventArgs()
                    {
                        Tenant = msgs.ElementAt(1),
                        Username  = msgs.ElementAt(2),
                        Password = msgs.ElementAt(3)
                    });
                    return true;
                } }
            };
        }

        private Dictionary<int, Func<List<string>, bool>> AddSmartRestToStore()
        {
            return new Dictionary<int, Func<List<string>, bool>>
            {
                { 20, (msgs) => {
                    IsExistTemplateCollectionEvt?.Invoke(this, new TemplateCollectionEventArgs()
                    {
                        TemplateCollectionName = msgs.ElementAt(1),
                        IsExist = true,
                        IdCollection = Int32.Parse(msgs.ElementAt(2))
                    });
                    return true;
                } },
                { 41, (msgs) => {
                    IsExistTemplateCollectionEvt?.Invoke(this, new TemplateCollectionEventArgs()
                    {
                        TemplateCollectionName = msgs.ElementAt(1),
                        IsExist = false,
                        IdCollection = 0
                    });
                    return true;
                } }
            };
        }
        private bool ExecuteOther(int operation, List<string> msgs)
        {
            Func<List<string>, bool> method;
            if (!_storeStaticOther.TryGetValue(operation, out method))
            {
                return false;
            }

            return method(msgs);
        }

        private bool ExecuteSmartRest(int operation, List<string> msgs)
        {
            Func<List<string>, bool> method;
            if (!_storeSmartRest.TryGetValue(operation, out method))
            {
                return false;
            }

            return method(msgs);
        }
        private bool ExecuteStaticOperation(int operation, List<string> msgs)
        {
            Func<List<string>, bool> method;
            if (!_storeStaticOperations.TryGetValue(operation, out method))
            {
                return false;
            }

            return method(msgs);
        }

        private void SubscribeEvt()
        {
            _mqttClient.Connected += async (s, e) =>
            {
                Debug.WriteLine("### CONNECTED WITH SERVER ###");

                await _mqttClient.SubscribeAsync(new List<TopicFilter>
                {
                    new TopicFilter("s/ds", MqttQualityOfServiceLevel.AtMostOnce),//static template
                    new TopicFilter("s/dt" , MqttQualityOfServiceLevel.AtMostOnce),//to verify if a template collection exists
                    new TopicFilter("s/e" , MqttQualityOfServiceLevel.AtMostOnce),//error messages
                    new TopicFilter("s/dcr", MqttQualityOfServiceLevel.AtMostOnce)//device credentials
                });

                Debug.WriteLine("### SUBSCRIBED ###");
            };

            _mqttClient.Disconnected += async (s, e) =>
            {
                Debug.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(2));

                try
                {
                    MqttClientOptions options = GetConnectOptions();
                    await _mqttClient.ConnectAsync(options);
                }
                catch
                {
                    Debug.WriteLine("### RECONNECTING FAILED ###");
                }
            };

            _mqttClient.ApplicationMessageReceived += (s, e) =>
            {
                Debug.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Debug.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Debug.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");

                if (e.ApplicationMessage.Topic.Equals("s/e"))
                {
                    Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    List<string> msgs = Encoding.UTF8.GetString(e.ApplicationMessage.Payload)
                                                       .Split(new char[] { ',' }).ToList();

                    if (msgs != null && msgs.Any())
                    {
                        var msgID = msgs.FirstOrDefault();
                        ErrorEvt?.Invoke(msgID, new ErrorMessageEventArgs()
                        {
                            Message = msgs.ElementAt(2)
                        });

                    }
                }
                if (e.ApplicationMessage.Topic.Equals("s/ds"))
                {
                    Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    List<string> msgs = Encoding.UTF8.GetString(e.ApplicationMessage.Payload)
                                                       .Split(new char[] { ',' }).ToList();

                    if (msgs != null && msgs.Any())
                    {
                        var msgID = msgs.FirstOrDefault();
                        if (!String.IsNullOrEmpty(msgID))
                        {
                            ExecuteStaticOperation(Int32.Parse(msgID), msgs);
                        }
                    }
                }
                if (e.ApplicationMessage.Topic.Equals("s/dt"))
                {
                    Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    List<string> msgs = Encoding.UTF8.GetString(e.ApplicationMessage.Payload)
                                                       .Split(new char[] { ',' }).ToList();

                    if (msgs != null && msgs.Any())
                    {
                        var msgID = msgs.FirstOrDefault();
                        if (!String.IsNullOrEmpty(msgID))
                        {
                            ExecuteSmartRest(Int32.Parse(msgID), msgs);
                        }
                    }
                }
                if (e.ApplicationMessage.Topic.Equals("s/dcr"))
                {
                    Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    List<string> msgs = Encoding.UTF8.GetString(e.ApplicationMessage.Payload)
                                                       .Split(new char[] { ',' }).ToList();

                    var msgID = msgs.FirstOrDefault();
                    if (!String.IsNullOrEmpty(msgID))
                    {
                         ExecuteOther(Int32.Parse(msgID), msgs);
                    }
                }
                if (e.ApplicationMessage.Topic.StartsWith("s/dc/"))
                {

                    var topic = e.ApplicationMessage.Topic;
                    var template = topic.Substring(5, topic.Length - 5);

                    string[] lines = Encoding.UTF8.GetString(e.ApplicationMessage.Payload).Split(new[] { '\n' }, StringSplitOptions.None);

                    var smartRestResponseEventArgs = new SmartRestResponseEventArgs();

                    smartRestResponseEventArgs.Topic = template;
                    smartRestResponseEventArgs.Responses = new List<SmartRestResponse>();

                    foreach (var line in lines)
                    {
                        List<string> msgs = line.Split(new char[] { ',' }).ToList();
                        var msgId = msgs.ElementAt(0);
                        var fields = msgs.GetRange(1, msgs.Count - 1);
                        smartRestResponseEventArgs.Responses.Add(new SmartRestResponse() { MessageID = msgId, Fields = fields });
                    }

                    SmartRestResponseEvt?.Invoke(topic, smartRestResponseEventArgs);
                }

            };
        }

        private MqttClientOptions GetConnectOptions()
        {
            var options = new MqttClientOptions
            {
                ClientId = _config.ClientId,
                CleanSession = true
            };
            if (!String.IsNullOrEmpty(_config.UserName) && !String.IsNullOrEmpty(_config.Password))
            {
                options.Credentials = new MqttClientCredentials() { Username = _config.UserName, Password = _config.Password };
            }
            Enum.TryParse(_config.ConnectionType, out ConnectionTypes connType);

            if (connType.Equals(ConnectionTypes.WS) || connType.Equals(ConnectionTypes.WSS))
            {
                options.ChannelOptions = new MqttClientWebSocketOptions
                {
                    Uri = _config.Server,
                    TlsOptions = connType.Equals(ConnectionTypes.WSS) ? new MqttClientTlsOptions() { UseTls = true } : new MqttClientTlsOptions() { UseTls = false }
                };
            }
            else if (connType.Equals(ConnectionTypes.TCP))
            {
                options.ChannelOptions = new MqttClientTcpOptions { Server = _config.Server };
            }
            else if (connType.Equals(ConnectionTypes.TLS))
            {
                //options.ChannelOptions = new MqttClientTlsOptions { Server = _config.Server };
            }
            options.CommunicationTimeout = TimeSpan.FromSeconds(60);
            return options;
        }

        public struct CustomValue
        {
            public string CustomValueAsString
            {
                get
                {
                    return String.Concat(",", Path, ",", Type.ToString(), ",", Value);
                }
            }

            public string Path { get; set; }
            public CustomValueType Type { get; set; }
            public string Value { get; set; }
        }

    }

    public class Software
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Version { get; set; }
    }
}