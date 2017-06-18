using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public class TaskDemo
    {
        public static void Run()
        {
            Console.WriteLine("Explicit Task demo started...\n\n");

            // Define and run the task
            Task task1 = Task.Run(() => 
            {
                for(int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Iteration {0}", (i + 1));
                    Thread.Sleep(1000);
                }
            });

            // pause execution for one second
            Thread.Sleep(1000);

            Console.WriteLine("Hello world");

            // Do not exit this block until task completes
            task1.Wait();

            Console.WriteLine("Explicit task demo complete\n\n");
        }

        public static void RunTasks()
        {
            Console.WriteLine("Run Tasks demo start...\n\n");

            Random rnd = new Random();
            string[] names = { "Bob", "Peter", "Jacob", "Mark", "Fred" };

            // List of anonymous types with a Name and Age property
            var namesWithAges = names.Select(t => new {Name = t, Age = rnd.Next(20, 65) }).ToList();

            // instead of taking 5 seconds to complete, it should take a total of 1 second
            List<Task> tasks = namesWithAges.Select(t => new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Name: {0}\nAge: {1}\n\n", t.Name, t.Age);
            })).ToList();

            // start each task
            tasks.ForEach(t => t.Start());

            // wait for all tasks to complete
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Run tasks demo ended...\n\n");
        }

        public static void RunTasksWithContinution()
        {
            Console.WriteLine("Tasks with continuation demo started...\n\n");

            Task<int>[] tasks = new Task<int>[10];
            Task[] continuationTasks = new Task[tasks.Length];

            for(int i = 0; i < tasks.Length; i++)
            {
                // Tasks that square the index
                tasks[i] = Task.Factory.StartNew((object cnt) =>
                {
                    int num = (int)cnt;
                    Console.WriteLine("ThreadId: {2} - {0} squared is {1}\n", num, (num * num)
                        , Thread.CurrentThread.ManagedThreadId);
                    return (num * num);
                }, i);

                // Tasks that cube the index
                continuationTasks[i] = tasks[i].ContinueWith(t =>
                {
                    // Check if thread ran to completion
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        int root = (int)Math.Sqrt(t.Result);
                        Console.WriteLine("TaskId: {2} - {0} cubed is {1}\n", root, (root * root * root), t.Id);
                    }
                    else
                    {
                        Console.WriteLine("{0} did not run to completion.\n", t.Id);
                    }
                });
            }

            // wait for all tasks complete
            // task exception handling
            try
            {
                Task.WaitAll(tasks);
                Task.WaitAll(continuationTasks);
                Console.WriteLine("Tasks with continuation demo ended...\n\n");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Argument exception error.\n{0}", e);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception error.\n{0}", e);
            }
        }

        public static void CancelAfterTenSecondsDemo()
        {
            Console.WriteLine("Cancel after 10 seconds demo started...\n\n");

            var cancellationTS = new CancellationTokenSource();
            CancellationToken ct = cancellationTS.Token;

            Task task = Task.Run(() =>
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    for (int i = 1; i <= 100; i++)
                    {
                        if (sw.ElapsedMilliseconds > 10000)
                            throw new OperationCanceledException("Task took longer than 10 seconds\n");

                        Thread.Sleep(1000);
                        Console.WriteLine("{0} second(s) have passed\n", i);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }, cancellationTS.Token);

            try
            {
                task.Wait();
            }
            catch(AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine("{0} {1}", e.Message, v.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("General Exception:", e.ToString());
            }

            Console.WriteLine("Cancel after 10 seconds demo ended...\n\n");
        }
    }
}
