using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public class AsyncAwaitDemo
    {
        public async static Task BasicDemo()
        {
            Console.WriteLine("Started basic async/await demo...");
            Console.WriteLine("starting work...");

            // start each task
            var taskSum = AddNumbers();
            var task1 = DoWork();
            var task2 = DoOtherWork();

            // await eacg task
            await task1;
            await task2;
            await taskSum;

            Console.WriteLine("Total sum is {0}", taskSum.Result);
            Console.WriteLine("Ended basic async/await demo...");
        }

        public static async Task DoWork()
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Working for {0} second(s)", i);
                }
            });
        }

        public static async Task DoOtherWork()
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Doing other work for {0} second(s)", i);
                }
            });
        }

        public static async Task<int> AddNumbers()
        {
            return await Task.Run(() =>
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
        }
    }
}
