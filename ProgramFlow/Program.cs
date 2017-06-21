using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpProgramming.ProgramFlow;
using CSharpProgramming.TypesClasses;

namespace CSharpProgramming
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            Console.WriteLine("Program started...{0}\n", DateTime.Now.ToString());

            // prompt user the available processes to run
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
                    case 21: DelegateAnonymousMethodDemo.DelegateImplementationDemo();
                        break;
                    case 22:
                        Console.WriteLine("Started struct demo...\n");

                        Rectangle rect = new Rectangle(5, 10);
                        Console.WriteLine("Initialized a struct with dimensions {0}x{1} (LxW).", rect.length, rect.width);

                        Rectangle rect2;
                        rect2.length = 10;
                        rect2.width = 20;
                        Console.WriteLine("Initialized a struct with dimensions {0}x{1} (LxW).", rect2.length, rect2.width);

                        Console.WriteLine("\nEnded struct demo...");
                        break;
                    case 23:
                        Console.WriteLine("Started basic inheritance demo...");

                        StudentProfileData studentProfile = new StudentProfileData();
                        studentProfile.Print();

                        StudentProfileData studenProfile2 = new StudentProfileData("Kelvin", "Nichols", "Computer Science", ClassStanding.Senior);
                        studenProfile2.Print();

                        Console.WriteLine("Ended basic inheritance demo...");
                        break;
                    case 24:
                        Console.WriteLine("Started basic boxing and unboxing demo...");

                        int i = 55;
                        Console.WriteLine("int i = {0}", i);
                        Console.WriteLine("boxing i");

                        object o = i;// implicit boxing, explicit boxing ex: object o = (object)i
                        Console.WriteLine("i is still {0}", o);

                        object x = 122;
                        Console.WriteLine("object x = {0}", x);
                        Console.WriteLine("unboxing x");
                        i = (int)x;
                        Console.WriteLine("x is still {0}", x);

                        Console.WriteLine("Ended basic boxing and unboxing demo...");
                        break;
                    case 25:
                        Console.WriteLine("Started basic IComparable demo...");

                        List<ComparableAgeEntity> entities = new List<ComparableAgeEntity>()
                        {
                            new ComparableAgeEntity("Name1", 26),
                            new ComparableAgeEntity("Name2", 30),
                            new ComparableAgeEntity("Name3", 5),
                            new ComparableAgeEntity("Name4", 6),
                            new ComparableAgeEntity("Name5", 50),
                            new ComparableAgeEntity("Name6", 40),
                            new ComparableAgeEntity("Name7", 33),
                            new ComparableAgeEntity("Name8", 12),
                            new ComparableAgeEntity("Name9", 65),
                            new ComparableAgeEntity("Name10", 70)
                        };

                        Console.WriteLine("All comparable entities");
                        entities.ForEach(t => Console.WriteLine(t));

                        Console.WriteLine("\nComparable entities sorted by age from youngest to oldest");
                        entities.Sort();
                        entities.ForEach(t => Console.WriteLine(t));

                        Console.WriteLine("\nComparable entities sorted by age from oldest to youngest");
                        entities = entities.OrderByDescending(t => t).ToList();
                        entities.ForEach(t => Console.WriteLine(t));

                        Console.WriteLine("Ended basic IComparable demo...");
                        break;
                    case 26:
                        Console.WriteLine("Started IEnumerable Implementation demo...");

                        ComparableAgeEntity[] comparableEntities = new ComparableAgeEntity[]
                        {
                            new ComparableAgeEntity("Name1", 26),
                            new ComparableAgeEntity("Name2", 30),
                            new ComparableAgeEntity("Name3", 5),
                            new ComparableAgeEntity("Name4", 6),
                            new ComparableAgeEntity("Name5", 50),
                            new ComparableAgeEntity("Name6", 40),
                            new ComparableAgeEntity("Name7", 33),
                            new ComparableAgeEntity("Name8", 12),
                            new ComparableAgeEntity("Name9", 65),
                            new ComparableAgeEntity("Name10", 70)
                        };

                        // IEnumerable implementation
                        var collection = new EnumerableCollection<ComparableAgeEntity>(comparableEntities);

                        // Custom foreach implementation
                        collection.ForEach(t => Console.WriteLine(t));

                        Console.WriteLine("Ended IEnumerable Implementation demo...");
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
            Console.WriteLine("[6] - Multiple Task demo");
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
            Console.WriteLine("[21] - Delegate Implementation demo");
            Console.WriteLine("[22] - Basic struct demo");
            Console.WriteLine("[23] - Inheritance basics demo");
            Console.WriteLine("[24] - Boxing and Unboxing demo");
            Console.WriteLine("[25] - IComparable Implementation demo");
            Console.WriteLine("[26] - IEnumerable Implementation demo");
            Console.WriteLine();
        }
    }
}
