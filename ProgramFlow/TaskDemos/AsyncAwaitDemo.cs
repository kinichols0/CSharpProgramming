using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramFlow.TaskDemos
{
    public class AsyncAwaitDemo
    {
        public async static void BasicDemo()
        {
            Console.WriteLine("Started basic async/await demo...");

            Console.WriteLine("starting work...");

            var task = DoWork();

            for(int i = 1; i <= 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Doing other work for {0} second(s)", i);
            }

            await task;

            Console.WriteLine("Ended basic async/await demo...");
        }

        public static Task DoWork()
        {
            var task = Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Working for {0} second(s)", i);
                }
            });

            return task;
        }
    }
}
