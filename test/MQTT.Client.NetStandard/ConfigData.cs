using System;
using System.Linq;

namespace MQTT.Test
{
    public sealed class ConfigData
    {
        private static readonly ConfigData instance = new ConfigData();

        static ConfigData()
        {
        }

        private ConfigData()
        {
            TcpServer = "piotr.staging.c8y.io";
            TlsServer = "piotr.staging.c8y.io";
            WsServer = "ws://piotr.staging.c8y.io/mqtt";
            WssServer = "wss://piotr.staging.c8y.io/mqtt";
            UserName = @"piotr/admin";
            Password = @"test1234";
            ClientId = "4927468bdd4b4171a23e31476ff82674";
            TlsPort = "8883";
            TcpPort = "1883";
            WsPort = "80";
            WssPort = "443";
        }

        public static ConfigData Instance
        {
            get
            {
                return instance;
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string TcpServer { get; }
        public string TlsServer { get; }
        public string WsServer { get; }
        public string WssServer { get; }
        public string UserName { get; }
        public string Password { get; }
        public string TlsPort { get; }
        public string TcpPort { get; }
        public string WsPort { get; }
        public string WssPort { get; }
        public string ClientId { get; }
    }
}
