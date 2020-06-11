using System;
using Cumulocity.SDK.Client;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Notification;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.API.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.API.Cep.Notification;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System.Threading;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => RunClientAsync());
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

        private static async Task RunClientAsync()
        {
            Console.WriteLine("Realtime Notification Demo!!!");

            // Enter the Platform URL as first argument of PlatformParameters.
            // Enter userid in the form <tenentID>/<username> as first argument of CumulocityCredentials.
            // Enter password as the second argument of CumulocityCredentials.
            PlatformParameters platformParameters = new PlatformParameters("", new CumulocityCredentials("", ""), new ClientConfiguration());

            InventoryRealtimeDeleteAwareNotificationsSubscriber subscriber = new InventoryRealtimeDeleteAwareNotificationsSubscriber(platformParameters);

            try
            {
                //Input the DeviceID of the device you want to subscribe for as the first argument of Subscribe method.
                subscriber.Subscribe("", new Handler(new SimpleOperationProcessor()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public class Handler : ISubscriptionListener<string, ManagedObjectDeleteAwareNotification>
        {
            private SimpleOperationProcessor operationProcessor;

            public Handler(SimpleOperationProcessor processor)
            {
                this.operationProcessor = processor;
                Console.WriteLine("Inside Handler Constructor");
            }

            public void OnError(ISubscription<string> subscription, Exception ex)
            {
                Console.WriteLine("Inside OnError");
                Console.WriteLine(ex.Message.ToString());
            }

            public void OnNotification(ISubscription<string> subscription, ManagedObjectDeleteAwareNotification notification)
            {
                Console.WriteLine("Inside OnNotification");
                operationProcessor.Process(notification);
            }
        }

        public class SimpleOperationProcessor : ManagedObjectDeleteAwareNotification
        {
            private IList<ManagedObjectDeleteAwareNotification> operations = new List<ManagedObjectDeleteAwareNotification>();
            public bool Process(ManagedObjectDeleteAwareNotification operation)
            {
                operations.Add(operation);
                return true;
            }
            public virtual IList<ManagedObjectDeleteAwareNotification> Operations
            {
                get
                {
                    return operations;
                }
                set
                {
                    this.operations = value;
                }
            }
        }


    }
}