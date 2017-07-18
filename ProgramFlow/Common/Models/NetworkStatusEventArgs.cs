using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Enums;

namespace CSharpProgramming.Common.Models
{
    public class NetworkStatusEventArgs : EventArgs
    {
        public ConnectionStatus ConnectionStatus { get; private set; }

        public NetworkStatusEventArgs(ConnectionStatus status)
        {
            ConnectionStatus = status;
        }
    }
}
