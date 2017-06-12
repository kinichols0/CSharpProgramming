using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProgramFlow.TaskDemos;

namespace ProgramFlow
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            Console.WriteLine("Program started...{0}\n", DateTime.Now.ToString());

            // prompt available process to run
            Console.WriteLine("Which demo do you want to run?");
            Console.WriteLine("[1] - ParallelFor Basic Run\n[2] - ParallelFor Concurrent Addition Run\n" +
                "[3] - ParallelForEach demo\n[4] - ParallelInvoke demo");
            Console.WriteLine("[5] - Task demo\n[6] - Multiple Task Demo\n[7] - Continuation Task demo\n[8] - Cancel task after 10 seconds demo.");
            Console.WriteLine("[9] - Thread demo\n[10] - Background Worker demo\n[11] - Threadpool demo\n[12] - Cancel background worker demo.");
            Console.WriteLine("[13] - Linq basic demo \n[14] - PLinq basic demo \n[15] PLinq ForAll demo");
            Console.WriteLine("[16] - Concurrent Blocking Collection demo\n ");

            // read the input and run the corresponding process
            string key = Console.ReadLine();
            if (int.TryParse(key, out int demoNum))
            {
                switch (demoNum)
                {
                    case 1: ParallelDemo.RunParallelForBasic();
                        break;
                    case 2: ParallelDemo.ParallelForAdditionRun();
                        break;
                    case 3: ParallelDemo.ParallelForEach();
                        break;
                    case 4: ParallelDemo.ParallelInvokeRun();
                        break;
                    case 5: TaskDemo.Run();
                        break;
                    case 6: TaskDemo.RunTasks();
                        break;
                    case 7: TaskDemo.RunTasksWithContinution();
                        break;
                    case 8: TaskDemo.CancelAfterTenSecondsDemo();
                        break;
                    case 9: ThreadingDemo.BasicRun();
                        break;
                    case 10: ThreadingDemo.RunBackagroundWorker();
                        break;
                    case 11: ThreadingDemo.ThreadPoolDemoRun();
                        break;
                    case 12: ThreadingDemo.BackgroundWorkerCancellationDemo();
                        break;
                    case 13: PLinqDemo.LinqBasicDemo();
                        break;
                    case 14: PLinqDemo.PLinqBasicDemo();
                        break;
                    case 15: PLinqDemo.PLinqForAllDemo();
                        break;
                    case 16: ConcurrentCollectionsDemo.BlockingCollectionAddDemo();
                        break;
                    default: Console.WriteLine("Could not find a process corresponding to {0} to run. Program will exit now.", demoNum);
                        break;
                }
            }
            else
                Console.WriteLine("Not valid input. Program will exit now.");

            Console.WriteLine("Program ended...,{0}", DateTime.Now.ToString());

            Console.ReadLine();
        }
    }
}
