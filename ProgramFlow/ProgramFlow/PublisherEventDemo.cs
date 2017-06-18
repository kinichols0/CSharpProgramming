using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    /// <summary>
    /// Custom event arguments for this demo
    /// </summary>
    public class DemoEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public DemoEventArgs(string message)
        {
            Message = message;
        }
    }

    /// <summary>
    /// Publisher that raises an event that can be subscribed to
    /// </summary>
    public class WriteOutputEventPublisher
    {
        public delegate void WriteOutputDelegate(object sender, DemoEventArgs e);

        public event WriteOutputDelegate WriteOutputEventCallback;

        public void StartDemo(string message)
        {
            Console.WriteLine("Started Basic Event demo...");

            // Raise an event.
            RaiseWriteOutputEvent(new DemoEventArgs(message));

            Console.WriteLine("Ended basic event demo...");
        }

        protected virtual void RaiseWriteOutputEvent(DemoEventArgs e)
        {
            WriteOutputDelegate handler = WriteOutputEventCallback;

            if (handler != null)
                handler(this, e);
        }
    }

    /// <summary>
    /// Subscriber that can subscribe to a publisher's event and
    /// run code when that event is raised.
    /// </summary>
    public class WriteOutputEventSubscriber
    {
        private string name;
        private WriteOutputEventPublisher pub;

        public WriteOutputEventSubscriber(string name, WriteOutputEventPublisher wPub)
        {
            this.name = name;
            pub = wPub;

            // Subscribe to the publisher's event
            wPub.WriteOutputEventCallback += HandleWriteOutPutEvent;
        }

        private void HandleWriteOutPutEvent(object sender, DemoEventArgs e)
        {
            // Code that runs when the event is raised by the publisher
            Console.WriteLine(name + " received this message: " + e.Message);
        }

        public void UnsubscribeFromEvent()
        {
            pub.WriteOutputEventCallback -= HandleWriteOutPutEvent;
        }
    }
}
