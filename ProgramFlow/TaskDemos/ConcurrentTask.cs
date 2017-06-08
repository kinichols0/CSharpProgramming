using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramFlow.TaskDemos
{
    public static class ConcurrentTask
    {
        public static void RunDemo()
        {
            Console.WriteLine();
            Console.WriteLine("Started implicit concurrent task...");

            // tasks executed concurrently
            Parallel.Invoke(() => HelloCat(), () => HelloDog());

            Console.WriteLine();
            Console.WriteLine("Ended implicit concurrent task...");
        }

        private static void HelloCat()
        {
            for (int i = 1; i <= 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("Hello Cat {0}", i);
            }
        }

        private static void HelloDog()
        {
            for (int i = 1; i <= 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("Hello Dog {0}", i);
            }
        }
    }
}
