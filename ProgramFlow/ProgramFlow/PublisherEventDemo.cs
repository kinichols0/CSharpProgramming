using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public enum ConnectionStatus
    {
        Open, Closed
    }

    public class NetworkStatusEventArgs : EventArgs
    {
        public ConnectionStatus ConnectionStatus { get; private set; }

        public NetworkStatusEventArgs(ConnectionStatus status)
        {
            ConnectionStatus = status;
        }
    }

    public class NetworkMessageBroadCastEventArgs : EventArgs
    {
        public int SenderId { get; private set; }
        public string SenderName { get; private set; }
        public string Message { get; private set; }

        public NetworkMessageBroadCastEventArgs(int senderId, string senderName, string message)
        {
            SenderId = senderId;
            SenderName = senderName;
            Message = message;
        }
    }

    public class NetworkEventPublisher
    {
        // declare an event for network status change
        public delegate void NetworkEventDelegate(object sender, NetworkStatusEventArgs args);
        public event NetworkEventDelegate NetworkStatusChange;

        // delcare an event for when a subscriber broadcasts a message.
        // this is alternative syntax to declare an event.
        public EventHandler<NetworkMessageBroadCastEventArgs> NetworkOnMessageBroadcastAll;

        /// <summary>
        /// Raise network open event
        /// </summary>
        public void RaiseNetworkOpenEvent()
        {
            Console.WriteLine("Network connection is open...");
            NetworkStatus_OnChange(new NetworkStatusEventArgs(ConnectionStatus.Open));
        }

        /// <summary>
        /// Raise network closed event
        /// </summary>
        public void RaiseNetworkCloseEvent()
        {
            Console.WriteLine("Network connection has closed...");
            NetworkStatus_OnChange(new NetworkStatusEventArgs(ConnectionStatus.Closed));
        }

        /// <summary>
        /// Broadcast message from subscriber to all
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        public void BroadCastMessageToAll(int senderId, string senderName, string message)
        {
            Console.WriteLine("{0} to all: {1}", senderName, message);
            NetworkOnMessageBroadcastAll?.Invoke(this, new NetworkMessageBroadCastEventArgs(senderId, senderName, message));
        }

        private void NetworkStatus_OnChange(NetworkStatusEventArgs args)
        {
            NetworkStatusChange?.Invoke(this, args);
        }
    }

    public class NetworkEventSubscriber
    {
        private NetworkEventPublisher publisher;

        public string Name { get; private set; }

        public int Id { get; set; }

        public NetworkEventSubscriber(NetworkEventPublisher publisher, string name, int id)
        {
            // set properties
            Name = name;
            Id = id;

            // subscribe to publisher events
            publisher.NetworkStatusChange += NetworkStatus_OnChange;
            publisher.NetworkOnMessageBroadcastAll += Network_OnMessageBroadcastAll;

            this.publisher = publisher;
        }

        /// <summary>
        /// Call the publisher's BroadCastMessageToAll function to raise the event
        /// to send the message to all subscribers
        /// </summary>
        /// <param name="msg"></param>
        public void BroadcastMessage(string msg)
        {
            publisher.BroadCastMessageToAll(Id, Name, msg);
        }

        /// <summary>
        /// Code to execute when a NetworkStatusChange event is raised
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void NetworkStatus_OnChange(object sender, NetworkStatusEventArgs args)
        {
            if (args.ConnectionStatus == ConnectionStatus.Open)
                Console.WriteLine("{0} has received word that the connection is open.", Name);
            else if (args.ConnectionStatus == ConnectionStatus.Closed)
                Console.WriteLine("{0} has received word that the connection is closed.", Name);
        }

        /// <summary>
        /// Code to execute when NetworkOnMessageBroadcastAll event is raised
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Network_OnMessageBroadcastAll(object sender, NetworkMessageBroadCastEventArgs args)
        {
            if (Id != args.SenderId)
                Console.WriteLine("{0}'s message from {1}: {2}", Name, args.SenderName, args.Message);
        }
    }

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
