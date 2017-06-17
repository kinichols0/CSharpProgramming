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
            var taskSum = AddNumbers();
            var taskOther = DoOtherWork();

            await task;
            await taskSum;
            await taskOther;

            Console.WriteLine("Total sum is {0}", taskSum.Result);

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

        public static Task DoOtherWork()
        {
            var task = Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Doing other work for {0} second(s)", i);
                }
            });

            return task;
        }

        public static Task<int> AddNumbers()
        {
            var task = Task<int>.Run(() =>
            {
                int total = 0;
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} + {1} = {2}", total, i, total + i);
                    total += i;
                }
                return total;
            });
            return task;
        }
    }
}
