﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public static class TaskDemo
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

        /// <summary>
        /// Run tasks with continuation
        /// </summary>
        public static void RunTasksWithContinution()
        {
            Console.WriteLine("Tasks with continuation demo started...\n\n");

            // an array of tasks
            Task[] tasks = new Task[10];

            for(int i = 0; i < tasks.Length; i++)
            {
                // Tasks that square the index
                tasks[i] = Task.Factory.StartNew((object cnt) =>
                {
                    int num = (int)cnt;
                    Console.WriteLine("ThreadId: {2} - {0} squared is {1}\n", num, (num * num)
                        , Thread.CurrentThread.ManagedThreadId);
                    return (num * num);
                }, i)
                .ContinueWith((t) =>
                {
                    // continuation tasks is only ran if prior task ran to completion
                    int root = (int)Math.Sqrt(t.Result);
                    Console.WriteLine("TaskId: {2} - {0} cubed is {1}\n", root, (root * root * root), t.Id);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
            }

            // wait for all tasks complete
            // task exception handling
            try
            {
                // wait for all tasks to complete
                Task.WaitAll(tasks);
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

        /// <summary>
        /// Task cancellation demo
        /// </summary>
        public static void CancelAfterSpecifiedSecondsDemo()
        {
            // get the cancellation token
            var cancellationTS = new CancellationTokenSource();
            CancellationToken ct = cancellationTS.Token;

            /* initialize the Action<object> delegate each task will run
            * the object is expected to be of type Dictionary<string, object>
            * */
            Action<object> taskToRun = (obj) =>
            {
                // cast out the expected object
                Dictionary<string, object> dict = (Dictionary<string, object>)obj;

                string name = dict["name"] as string;
                int seconds = (int)dict["seconds"];

                // loop for a specified number of seconds
                for (int i = 1; i <= seconds; i++)
                {
                    // throw an OperationCanceledException if cancellation has been requested
                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine("{0} has been canceled.\n", name);

                        // throw the operation canceled exception
                        ct.ThrowIfCancellationRequested();
                    }

                    // sleep for one second
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} is running. {1} second(s) has passed\n", name, i);
                }
                Console.WriteLine("{0} ran to completion.\n", name);
            };

            // start tasks and pass the delegate to run, the number of seconds to run, and the cancellation token
            Task[] tasks = new Task[]
            {
                Task.Factory.StartNew(taskToRun, new Dictionary<string, object>(){ { "seconds", 4 }, { "name", "TaskA" } }, ct),
                Task.Factory.StartNew(taskToRun, new Dictionary<string, object>(){ { "seconds", 2 }, { "name", "TaskB" } }, ct),
                Task.Factory.StartNew(taskToRun, new Dictionary<string, object>(){ { "seconds", 10 }, { "name", "TaskC" } }, ct),
                Task.Factory.StartNew(taskToRun, new Dictionary<string, object>(){ { "seconds", 12 }, { "name", "TaskD" } }, ct)
            };

            // Cancel tasks after 5 seconds
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
            }

            // cancel tasks
            cancellationTS.Cancel();

            try
            { 
                // wait for all tasks
                Task.WaitAll(tasks);
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine("{0} {1}", e.Message, v.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("General Exception:", e.ToString());
            }
            finally
            {
                cancellationTS.Dispose();
            }
        }
    }
}
