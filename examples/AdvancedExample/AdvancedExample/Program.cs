using Cumulocity.MQTT;
using Cumulocity.MQTT.Enums;
using Cumulocity.MQTT.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Cumulocity.MQTT.Client;

namespace AdvancedExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advanced Example");

            Task.Run(() => RunClientAsync());
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

        private static async Task RunClientAsync()
        {
            var cnf = new Configuration()
            {
                Server = "ws://piotr.staging.c8y.io/mqtt",
                UserName = @"piotr/admin",
                Password = @"",
                ClientId = "4927468bdd4b4171a23e31476ff82675",
                Port = "80",
                ConnectionType = "WS"
            };

            var cl = new Client(cnf);
            await cl.ConnectAsync();
            Console.WriteLine(String.Format("Connected {0}", cl.IsConnected));

            //The entry point
            //await CallAllMethods(cl);
        }


        private static async Task CallAllMethods(Client cl)
        {
            //MqttStatic
            await CreateDevice(cl);
            await ConfigureHardware(cl);
            await ChildDeviceCreation(cl);
            await GetChildDevices(cl);
            await ConfigureMobile(cl);
            await ConfigurePosition(cl);
            await SetConfiguration(cl);
            await SetSupportedOperations(cl);
            await SetFirmware(cl);
            await SetSoftwareList(cl);
            await SetRequiredAvailability(cl);
            await CreateCustomMeasurement(cl);
            await CreateSignalStrengthMeasurement(cl);
            await CreateTemperatureMeasurement(cl);
            await CreateBatteryMeasurement(cl);
            await CreateCriticalAlarm(cl);
            await CreateMajorAlarm(cl);
            await CreateMinorAlarm(cl);
            await CreateWarningAlarm(cl);
            await UpdateSeverityOfExistingAlarm(cl);
            await ClearExistingAlarm(cl);
            await CreateLocationUpdateEvent(cl);
            await CreateLocationUpdateEventWithDeviceUpdate(cl);
            await GetPendingOperations(cl);
            await SetExecutingOperations(cl);
            await SetOperationToFailed(cl);
            await SetOperationToSuccessful(cl);
            await CheckTemplateCollectionExists(cl);
            await CreateTemplateDataGetTemplate(cl);
            SendRequestDataAsyncGetTemplate4(cl);
            await CreateTemplateDataAsyncPostTemplateMeasurement(cl);
            await CreateTemplateDataPostTemplateAlarm(cl);
            await CreateTemplatePostTemplateEvent(cl);
            await CreateTemplateDataPostTemplateInventory(cl);
            await CreateTemplateDataUpdateTemplateInventory(cl);
            await CreateTemplateDataUpdateTemplateAlarm(cl);
            await CreateTemplateDataUpdateTemplateClearingAlarm(cl);
            await CreateTemplateDataUpdateTemplateOperation(cl);
            await SendRequestDataPostTemplateAlarm(cl);
            await SendRequestDataPostTemplateMeasurement(cl);
            await SendRequestDataPostTemplateEvent(cl);
        }

        private static async Task SetExecutingOperations(Client cl)
        {
            cl.RestartEvt += Cl_RestartEvt;
            await cl.MqttStaticOperationTemplates
                    .SetExecutingOperationsAsync("c8y_Restart", (e) => { return Task.FromResult(false); });
        }

        private static async Task SetOperationToFailed(Client cl)
        {
            await cl.MqttStaticOperationTemplates
                    .SetOperationToFailedAsync("c8y_Restart", "Could not restart", (e) => { return Task.FromResult(false); });
        }

        private static async Task SetOperationToSuccessful(Client cl)
        {
            await cl.MqttStaticOperationTemplates
                    .SetOperationToSuccessfulAsync("c8y_Restart", string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task GetPendingOperations(Client cl)
        {
            await cl.MqttStaticOperationTemplates
                    .GetPendingOperationsAsync((e) => { return Task.FromResult(false); });
        }

        private static async Task CreateLocationUpdateEventWithDeviceUpdate(Client cl)
        {
            await cl.MqttStaticEventTemplates
                    .CreateLocationUpdateEventWithDeviceUpdateAsync(
                                 "52.209539",
                                 "16.831993",
                                 "76",
                                 "134",
                                 string.Empty,
                                 (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateLocationUpdateEvent(Client cl)
        {
            await cl.MqttStaticEventTemplates
                    .CreateLocationUpdateEventAsync(
                     "52.209538",
                     "16.831992",
                     "76",
                     "134",
                     string.Empty,
                     (e) => { return Task.FromResult(false); });
        }

        private static async Task ClearExistingAlarm(Client cl)
        {
            await cl.MqttStaticAlarmTemplates
                .ClearExistingAlarmAsync("c8y_TemperatureAlarm", (e) => { return Task.FromResult(false); });
        }

        private static async Task UpdateSeverityOfExistingAlarm(Client cl)
        {
            await cl.MqttStaticAlarmTemplates
                .UpdateSeverityOfExistingAlarmAsync("c8y_AirPressureAlarm", "CRITICAL", (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateWarningAlarm(Client cl)
        {
            await cl.MqttStaticAlarmTemplates
                .CreateWarningAlarmAsync("c8y_AirPressureAlarm", "Warning of type c8y_AirPressureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateMinorAlarm(Client cl)
        {
            await cl.MqttStaticAlarmTemplates
                .CreateMinorAlarmAsync("c8y_WaterAlarm", "Alarm of type c8y_WaterAlarm raised", string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateMajorAlarm(Client cl)
        {
            await cl.MqttStaticAlarmTemplates
                .CreateMajorAlarmAsync("c8y_BatteryAlarm", " Major Alarm of type c8y_BatteryAlarm raised", string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateCriticalAlarm(Client cl)
        {
            await cl.MqttStaticAlarmTemplates
                .CreateCriticalAlarmAsync("c8y_TemperatureAlarm", "Alarm of type c8y_TemperatureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateBatteryMeasurement(Client cl)
        {
            await cl.MqttStaticMeasurementTemplates
                .CreateBatteryMeasurementAsync("95", "2017-09-13T15:01:14.000+02:00", (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateTemperatureMeasurement(Client cl)
        {
            await cl.MqttStaticMeasurementTemplates
                .CreateTemperatureMeasurementAsync("25", "2018-02-15T05:01:14.000+02:00", (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateSignalStrengthMeasurement(Client cl)
        {
            await cl.MqttStaticMeasurementTemplates
                .CreateSignalStrengthMeasurementAsync("-90", "23", "2017-09-13T14:00:14.000+02:00", (e) => { return Task.FromResult(false); });
        }

        private static async Task SendRequestDataPostTemplateEvent(Client cl)
        {
            await cl.MqttCustomSmartRest.SendRequestDataAsync("PostTemplateEvent", "5555", new List<string> { "", "100" });
        }

        private static async Task SendRequestDataPostTemplateMeasurement(Client cl)
        {
            await cl.MqttCustomSmartRest.SendRequestDataAsync("PostTemplateMeasurement", "7777", new List<string> { "", "25" });
        }

        private static async Task SendRequestDataPostTemplateAlarm(Client cl)
        {
            await cl.MqttCustomSmartRest.SendRequestDataAsync("PostTemplateAlarm", "6666", new List<string> { "2018-02-15T16:03:14.000+02:00", "100", "ACTIVE", "MAJOR" });
        }

        private static async Task CreateTemplateDataUpdateTemplateOperation(Client cl)
        {
            await cl.MqttCustomSmartRest.CreateTemplateDataAsync("UpdateTemplateOperation",
                                                 new List<Request> {
                                                 new OperationRequest("1111",
                                                 null,
                                                 "com_cumulocity_model_WebCamDevice",
                                                 new OperationFragment(
                                                     "status",
                                                         OperationStatus.SUCCESSFUL),
                                                     new List<CustomValue>{
                                                     new CustomValue {
                                                         Path = "c8y_Fragment.val",
                                                         Type = CustomValueType.NUMBER,
                                                         Value = String.Empty
                                                     } })
                                                 },
                                                 new List<Response> {
                                                     new Response("8889",
                                                     String.Empty,
                                                     "c8y_IsDevice",
                                                     new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                                 });
        }

        private static async Task CreateTemplateDataUpdateTemplateClearingAlarm(Client cl)
        {
            await cl.MqttCustomSmartRest.CreateTemplateDataAsync("UpdateTemplateClearingAlarm",
                                                 new List<Request> {
                                                 new AlarmUpdateRequest("0000",
                                                 null,
                                                 "c8y_CustomAlarm",
                                                 new AlarmFragment("status",AlarmStatus.CLEARED),
                                                 new List<CustomValue>{
                                                     new CustomValue {
                                                         Path = "c8y_CustomFragment",
                                                         Type = CustomValueType.STRING,
                                                         Value = String.Empty
                                                     },

                                                 })
                                                 },
                                                 new List<Response> {
                                                     new Response("8889",
                                                     String.Empty,
                                                     "c8y_IsDevice",
                                                     new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                                 });
        }

        private static async Task CreateTemplateDataUpdateTemplateAlarm(Client cl)
        {
            await cl.MqttCustomSmartRest.CreateTemplateDataAsync("UpdateTemplateAlarm",
                                               new List<Request> {
                                                 new AlarmUpdateRequest("2222",
                                                 null,
                                                 "c8y_CustomAlarm",
                                                 new AlarmFragment("status",null),
                                                 new List<CustomValue>{
                                                 })
                                               },
                                               new List<Response> {
                                                     new Response("8889",
                                                     String.Empty,
                                                     "c8y_IsDevice",
                                                     new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                               });
        }

        private static async Task CreateTemplateDataUpdateTemplateInventory(Client cl)
        {
            await cl.MqttCustomSmartRest.CreateTemplateDataAsync("UpdateTemplateInventory",
                                                 new List<Request> {
                                                 new InventoryRequest("3333",
                                                 null,
                                                 "c8y_MySerial",
                                                 new List<CustomValue>{
                                                     new CustomValue {Path = "c8y_CustomInventory.M.value",
                                                         Type = CustomValueType.NUMBER,
                                                         Value = String.Empty
                                                     }
                                                 },HttpMethods.PUT)
                                                 },
                                                 new List<Response> {
                                                     new Response("8889",
                                                     String.Empty,
                                                     "c8y_IsDevice",
                                                     new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                                 });
        }

        private static async Task CreateTemplateDataPostTemplateInventory(Client cl)
        {
            await cl.MqttCustomSmartRest.CreateTemplateDataAsync("PostTemplateInventory",
                                                 new List<Request> {
                                                 new InventoryRequest("4444",
                                                 null,
                                                 "c8y_MySerial",
                                                 new List<CustomValue>{
                                                     new CustomValue {Path = "c8y_CustomInventory.M.value",
                                                         Type = CustomValueType.NUMBER,
                                                         Value = String.Empty
                                                     }
                                                 },HttpMethods.POST)
                                                 },
                                                 new List<Response> {
                                                     new Response("8889",
                                                     String.Empty,
                                                     "c8y_IsDevice",
                                                     new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                                 });
        }

        private static async Task CreateTemplatePostTemplateEvent(Client cl)
        {
            await cl.MqttCustomSmartRest.CreateTemplateDataAsync("PostTemplateEvent",
                                 new List<Request> {
                                             new EventRequest("5555",
                                             null,
                                             "c8y_CustomEvent",
                                             "CustomEvent",
                                             String.Empty,
                                             new List<CustomValue>{
                                                 new CustomValue {Path = "c8y_CustomEvent.M.value",
                                                     Type = CustomValueType.NUMBER,
                                                     Value = String.Empty
                                                 }
                                             })
                                 },
                                 new List<Response> {
                                                 new Response("8889",
                                                 String.Empty,
                                                 "c8y_IsDevice",
                                                 new List<string> {
                                                     "type",
                                                     "c8y_MQTTDevice",
                                                     "c8y_Mobile.cellId" })
                                 });
        }

        private static async Task CreateTemplateDataPostTemplateAlarm(Client cl)
        {
            await cl.MqttCustomSmartRest
                    .CreateTemplateDataAsync("PostTemplateAlarm",
                                        new List<Request> {
                                             new AlarmRequest("6666",
                                             null,
                                             "c8y_CustomAlarm",
                                             "CustomAlarm",
                                             "ACTIVE",
                                             "MAJOR",
                                             String.Empty,
                                             new List<CustomValue>{
                                                 new CustomValue {
                                                     Path = "c8y_CustomAlarm.M.value",
                                                     Type = CustomValueType.NUMBER,
                                                     Value = String.Empty
                                                 }
                                             },HttpMethods.POST)
                                        },
                                        new List<Response> {
                                                 new Response("8889",
                                                 String.Empty,
                                                 "c8y_IsDevice",
                                                 new List<string> {
                                                     "type",
                                                     "c8y_MQTTDevice",
                                                     "c8y_Mobile.cellId" }),
                                                 new Response("8888",
                                                 String.Empty,
                                                 "c8y_IsDevice",
                                                 new List<string> {
                                                     "type",
                                                     "c8y_MQTTDevice",
                                                     "c8y_Mobile.cellId" })
                                        });
        }

        private static async Task CreateTemplateDataAsyncPostTemplateMeasurement(Client cl)
        {
            await cl.MqttCustomSmartRest
                    .CreateTemplateDataAsync("PostTemplateMeasurement",
                                    new List<Request> {
                                         new MeasurementRequest("7777",
                                         null,
                                         "c8y_CustomMeasurement",
                                         String.Empty,
                                         new List<CustomValue>{
                                             new CustomValue {Path = "c8y_MyMeasurement.M.value",
                                                 Type = CustomValueType.NUMBER,
                                                 Value = String.Empty
                                             }
                                         },HttpMethods.POST)
                                    },
                                    new List<Response> {
                                             new Response("8889",
                                             String.Empty,
                                             "c8y_IsDevice",
                                             new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" }),
                                             new Response("8888",
                                             String.Empty,
                                             "c8y_IsDevice",
                                             new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                    });
        }

        private static void SendRequestDataAsyncGetTemplate4(Client cl)
        {
            var resultGetTemplate = Task.Run(() => cl.MqttCustomSmartRest.SendRequestDataAsync("GetTemplate4", "9998", new List<string> { "4927468bdd4b4171a23e31476ff82675" })).Result;
        }

        private static async Task CreateTemplateDataGetTemplate(Client cl)
        {
            await cl.MqttCustomSmartRest
                    .CreateTemplateDataAsync("GetTemplate4",
                                                    new List<Request> {
                                                    new InventoryGetRequest("9999",null, String.Empty, true),
                                                    new InventoryGetRequest("9998",null, "c8y_Serial", false)
                                                    },
                                                    new List<Response> {
                                                        new Response("8889",
                                                        String.Empty,
                                                        "c8y_IsDevice",
                                                        new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" }),
                                                        new Response("8888",
                                                        String.Empty,
                                                        "c8y_IsDevice",
                                                        new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
                                                    });
        }

        private static async Task CheckTemplateCollectionExists(Client cl)
        {
            cl.IsExistTemplateCollectionEvt += (s, e) =>
            {
                var item = e.IsExist;
            };
            await cl.MqttCustomSmartRest
                    .CheckTemplateCollectionExists("test2", (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateCustomMeasurement(Client cl)
        {
            await cl.MqttStaticMeasurementTemplates
                    .CreateCustomMeasurementAsync("c8y_Temperature", "T", "25", string.Empty, string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task SetRequiredAvailability(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .SetRequiredAvailability(60,
                                             (e) => { return Task.FromResult(false); });
        }

        private static async Task SetSoftwareList(Client cl)
        {
            List<Software> list = new List<Software>();
            list.Add(new Software() { Name = "Software01", Url = "url1", Version = "1.0" });
            list.Add(new Software() { Name = "Software02", Url = "url2", Version = "2.1" });

            await cl.MqttStaticInventoryTemplates.SetSoftwareList(list,
                                         (e) => { return Task.FromResult(false); });
        }

        private static async Task SetFirmware(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .SetFirmware(
                                "Extreme",
                                "Ultra 1.0",
                                @"http://sth.url",
                                (e) => { return Task.FromResult(false); });
        }

        private static async Task SetSupportedOperations(Client cl)
        {
            IList<string> supportedOperations = new List<string>();
            supportedOperations.Add("c8y_Restart");
            supportedOperations.Add("c8y_Configuration");

            //Will set the supported operations of the device
            await cl.MqttStaticInventoryTemplates.SetSupportedOperations(
                                        supportedOperations,
                                        (e) => { return Task.FromResult(false); });
        }

        private static async Task SetConfiguration(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .SetConfiguration(
                            "val1 = 1\nval2 = 2",
                            (e) => { return Task.FromResult(false); });
        }

        private static async Task ConfigurePosition(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .ConfigurePosition(
                                "52.409538",
                                "16.931992",
                                "76",
                                "134",
                                (e) => { return Task.FromResult(false); });
        }

        private static async Task ConfigureMobile(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .ConfigureMobile(
                                "356938035643809",
                                "8991101200003204510",
                                "410-07-4777770001",
                                "410",
                                "07",
                                "477",
                                "0001",
                                (e) => { return Task.FromResult(false); });
        }

        private static async Task GetChildDevices(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .GetChildDevices((e) => { return Task.FromResult(false); });
        }

        private static async Task ChildDeviceCreation(Client cl)
        {
            cl.ChildrenOfDeviceEvt += (s, e) => {
                foreach (var device in e.ChildrenOfDevice)
                {
                    Console.WriteLine(device);
                }
            };
            await cl.MqttStaticInventoryTemplates
                    .ChildDeviceCreationAsync("D32Q", "Device Name", "c8y_MQTTDevice", (e) => { return Task.FromResult(false); });
        }

        private static async Task ConfigureHardware(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .ConfigureHardware("S123456789", "model", "1.0", (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateDevice(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .DeviceCreation("TestDevice3", "", (e) => { return Task.FromResult(false); });
        }

        private static void Cl_RestartEvt(object sender, RestartEventArgs e)
        {
            Console.WriteLine("Restart");
        }
    }
}
