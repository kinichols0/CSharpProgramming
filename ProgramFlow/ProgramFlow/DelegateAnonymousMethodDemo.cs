using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public static class DelegateAnonymousMethodDemo
    {
        // Printe message delegate
        delegate void PrintMessageDelegate(string message);

        public static void DelegateImplementationDemo()
        {
            Console.WriteLine("Delegate Implementation demo started...");

            // initialize delegate with inline code
            PrintMessageDelegate printA = delegate (string msg)
            {
                Console.WriteLine("Print A: {0}", msg);
            };

            // initialize delegate with lambda in inline code
            PrintMessageDelegate printB = (msg) =>
            {
                Console.WriteLine("Print B: {0}", msg);
            };

            // initialize delegate with named method
            PrintMessageDelegate printC = new PrintMessageDelegate(PrintMessage);

            // Call each delegate
            printA("This is A's print method implementation.");
            printB("This is B's print method implementation.");
            printC("This is C's print method implementation.");

            Console.WriteLine("Delegate Implmentation demo ended...");
        }

        private static void PrintMessage(string msg)
        {
            Console.WriteLine("Print C: {0}", msg);
        }
    }
}
