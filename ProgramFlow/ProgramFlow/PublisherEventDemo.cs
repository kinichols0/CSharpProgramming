using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpProgramming.Common.Models;

namespace CSharpProgramming.ProgramFlow
{
    public class NetworkEventPublisherDemo
    {
        /// <summary>
        /// Event Publisher/Subscriber demo
        /// </summary>
        public static void Run()
        {
            // initialize the publisher
            NetworkEventPublisher publisher = new NetworkEventPublisher();

            // initialize subscribers
            NetworkEventSubscriber subscriber1 = new NetworkEventSubscriber(publisher, "Peter", 1);
            NetworkEventSubscriber subscriber2 = new NetworkEventSubscriber(publisher, "Tom", 2);
            NetworkEventSubscriber subscriber3 = new NetworkEventSubscriber(publisher, "Joe", 3);

            // raise network open event
            publisher.RaiseNetworkOpenEvent();

            // sleep for three seconds
            for(int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("open...");
            }

            // broadcast a message from subscriber 1 to all subscribers
            subscriber1.BroadcastMessage("Hello there!");

            // sleep for three seconds
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("open...");
            }

            // raise network close event
            publisher.RaiseNetworkCloseEvent();
        }
    }
}
