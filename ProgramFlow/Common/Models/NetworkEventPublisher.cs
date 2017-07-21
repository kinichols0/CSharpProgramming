using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Enums;
using CSharpProgramming.Common.Models;

namespace CSharpProgramming.Common.Models
{
    public class NetworkEventPublisher
    {
        // declare an event for network status change
        public delegate void NetworkEventDelegate(object sender, NetworkStatusEventArgs args);
        public event NetworkEventDelegate NetworkStatusChange;

        // delcare an event for when a subscriber broadcasts a message.
        // this is alternative syntax to declare an event.
        public event EventHandler<NetworkMessageBroadCastEventArgs> NetworkOnMessageBroadcastAll;

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
}
