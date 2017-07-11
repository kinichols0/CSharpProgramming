using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public class EventsDelegatesDemos
    {
        public static void EventPublisherDemo()
        {
            WriteOutputEventPublisher publisher = new WriteOutputEventPublisher();
            WriteOutputEventSubscriber subA = new WriteOutputEventSubscriber("SubA", publisher);
            WriteOutputEventSubscriber subB = new WriteOutputEventSubscriber("SubB", publisher);
            publisher.StartDemo("This is an event demo.");
            subA.UnsubscribeFromEvent();
            publisher.StartDemo("Raising another event.");
        }
    }

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

        /// <summary>
        /// When this method is ran it raises the event each subscriber will receive
        /// </summary>
        /// <param name="message"></param>
        public void StartDemo(string message)
        {
            Console.WriteLine("Started Basic Event demo...");

            // Raise the event
            RaiseWriteOutputEvent(new DemoEventArgs(message));

            Console.WriteLine("Ended basic event demo...");
        }

        /// <summary>
        /// Invokes the WriteOutputDelegate event if it is not null
        /// </summary>
        /// <param name="e"></param>
        protected virtual void RaiseWriteOutputEvent(DemoEventArgs e)
        {
            // if event delegate is not null, execute it
            WriteOutputEventCallback?.Invoke(this, e);
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
