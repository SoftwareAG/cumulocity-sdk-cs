using System;
using System.Threading;
using System.Threading.Tasks;
using Cumulocity.MQTT;
using Cumulocity.MQTT.Model;

namespace HelloExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advanced Example");

            Task.Run(() => RunClientAsync());
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

        private static async Task RunClientAsync()
        {
            //configure MQTT connection
            var cnf = new Configuration()
            {
                Server = "ws://url/mqtt",
                UserName = @"tenant/user",
                Password = @"p@ssword",
                ClientId = "4927468bdd4b4171a23e31476ff82675",
                Port = "80",
                ConnectionType = "WS"
            };

            var cl = new Client(cnf);
            await cl.ConnectAsync();

            //connect to the Cumulocity
            Console.WriteLine(String.Format("Connected {0}", cl.IsConnected));

            //create device
            await CreateDevice(cl);
            //set hardware information
            await ConfigureHardware(cl);
            //listen for operation
            await SetExecutingOperations(cl);
            //Create a measurement
            await CreateCustomMeasurement(cl);
            //Create an event
            await CreateBasicEvent(cl);
            //Create an alarm
            await CreateCriticalAlarm(cl);

        }

        private static async Task CreateDevice(Client cl)
        {
            await cl.StaticInventoryTemplates.DeviceCreation("TestDevice3", "", (e) => { return Task.FromResult(false); });
        }

        private static async Task ConfigureHardware(Client cl)
        {
            await cl.StaticInventoryTemplates
                    .ConfigureHardware("S123456789", "model", "1.0", (e) => { return Task.FromResult(false); });
        }

        private static async Task SetExecutingOperations(Client cl)
        {
            cl.RestartEvt += Cl_RestartEvt;
            await cl.StaticOperationTemplates
                    .SetExecutingOperationsAsync("c8y_Restart", (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateCustomMeasurement(Client cl)
        {
            await cl.StaticMeasurementTemplates
                .CreateCustomMeasurementAsync("c8y_Temperature", "T", "25", string.Empty, string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateBasicEvent(Client cl)
        {
            await cl.StaticEventTemplates
                .CreateBasicEventAsync(
                    "c8y_MyEvent", "Something was triggered",
                    string.Empty,
                    (e) => { return Task.FromResult(false); });
        }

        private static async Task CreateCriticalAlarm(Client cl)
        {
            await cl.StaticAlarmTemplates
                .CreateCriticalAlarmAsync("c8y_TemperatureAlarm", "Alarm of type c8y_TemperatureAlarm raised", string.Empty, (e) => { return Task.FromResult(false); });
        }

        private static void Cl_RestartEvt(object sender, RestartEventArgs e)
        {
            Console.WriteLine("RestartEvt");
        }
    }
}