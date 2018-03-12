using System;
using System.Threading.Tasks;
using Cumulocity.MQTT;
using Cumulocity.MQTT.Model;

namespace HelloExample
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
            //configure MQTT connection
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

            //connect to the Cumulocity
            Console.WriteLine(String.Format("Connected {0}", cl.IsConnected));

            //create device
            await CreateDevice(cl);
            //set hardware information
            await ConfigureHardware(cl);
            //listen for operation
            await SetExecutingOperations(cl);
        }

        private static async Task CreateDevice(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .DeviceCreation("TestDevice3", "", (e) => { return Task.FromResult(false); });
        }

        private static async Task ConfigureHardware(Client cl)
        {
            await cl.MqttStaticInventoryTemplates
                    .ConfigureHardware("S123456789", "model", "1.0", (e) => { return Task.FromResult(false); });
        }

        private static async Task SetExecutingOperations(Client cl)
        {
            cl.RestartEvt += Cl_RestartEvt;
            await cl.MqttStaticOperationTemplates
                    .SetExecutingOperationsAsync("c8y_Restart", (e) => { return Task.FromResult(false); });
        }

        private static void Cl_RestartEvt(object sender, RestartEventArgs e)
        {
            Console.WriteLine("RestartEvt");
        }
    }
}
