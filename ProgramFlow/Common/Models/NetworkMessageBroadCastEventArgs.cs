using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
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
}
