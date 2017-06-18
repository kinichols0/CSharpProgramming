using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpProgramming.ProgramFlow;

namespace CSharpProgramming
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            Console.WriteLine("Program started...{0}\n", DateTime.Now.ToString());

            // prompt available process to run
            PrintPrompt();

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
                    case 17: ConcurrentCollectionsDemo.ConcurrentDictionaryDemo();
                        break;
                    case 18: ConcurrentCollectionsDemo.ConcurrentBagDemo();
                        break;
                    case 19: AsyncAwaitDemo.BasicDemo().Wait();
                        break;
                    case 20:
                        WriteOutputEventPublisher publisher = new WriteOutputEventPublisher();
                        WriteOutputEventSubscriber subA = new WriteOutputEventSubscriber("SubA", publisher);
                        WriteOutputEventSubscriber subB = new WriteOutputEventSubscriber("SubB", publisher);
                        publisher.StartDemo("This is an event demo.");
                        subA.UnsubscribeFromEvent();
                        publisher.StartDemo("Raising another event.");
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

        public static void PrintPrompt()
        {
            Console.WriteLine("Which demo do you want to run?\n");
            Console.WriteLine("[1] - ParallelFor Basic Run");
            Console.WriteLine("[2] - ParallelFor Concurrent Addition Run");
            Console.WriteLine("[3] - ParallelForEach demo");
            Console.WriteLine("[4] - ParallelInvoke demo");
            Console.WriteLine("[5] - Task demo");
            Console.WriteLine("[6] - Multiple Task Demo");
            Console.WriteLine("[7] - Continuation Task demo");
            Console.WriteLine("[8] - Cancel task after 10 seconds demo.");
            Console.WriteLine("[9] - Thread demo");
            Console.WriteLine("[10] - Background Worker demo");
            Console.WriteLine("[11] - Threadpool demo");
            Console.WriteLine("[12] - Cancel background worker demo.");
            Console.WriteLine("[13] - Linq basic demo ");
            Console.WriteLine("[14] - PLinq basic demo");
            Console.WriteLine("[15] PLinq ForAll demo");
            Console.WriteLine("[16] - Concurrent Blocking Collection demo");
            Console.WriteLine("[17] - Cocnurrent Dictionary Collection demo");
            Console.WriteLine("[18] - Concurrent Bag Demo ");
            Console.WriteLine("[19] - Async/Await Basic demo");
            Console.WriteLine("[20] - Event demo");
            Console.WriteLine();
        }
    }
}
