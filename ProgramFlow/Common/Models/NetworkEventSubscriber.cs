using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Enums;

namespace CSharpProgramming.Common.Models
{
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
}
