using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramFlow.TaskDemos
{
    public class ExplicitTask
    {
        public static void RunDemo()
        {
            Console.WriteLine("Explicit Task demo started...");
            Console.WriteLine();

            // Define and run the task
            Task task1 = Task.Run(() => 
            {
                for(int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Iteration " + (i + 1));
                    Thread.Sleep(1000);
                }
            });

            Thread.Sleep(1000);
            Console.WriteLine("Hello world");

            // Do not exit this block until task completes
            task1.Wait();

            Console.WriteLine();
            Console.WriteLine("Explicit task demo complete");
        }
    }
}
