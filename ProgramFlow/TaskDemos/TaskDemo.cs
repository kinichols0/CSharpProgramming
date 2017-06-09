using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramFlow.TaskDemos
{
    public class TaskDemo
    {
        public static void Run()
        {
            Console.WriteLine("Explicit Task demo started...");
            Console.WriteLine();

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

            Console.WriteLine();
            Console.WriteLine("Explicit task demo complete");
        }

        public static void RunTasks()
        {
            Console.WriteLine("Run Tasks demo start...\n\n");

            Random rnd = new Random();
            string[] names = { "Bob", "Peter", "Jacob", "Mark", "Fred" };

            // List of anonymous types with a Name and Age property
            var namesWithAges = names.Select(t => new {Name = t, Age = rnd.Next(20, 65) }).ToList();

            //// Action delegate, not used
            //Action<string, int> actionMethod = (string name, int age) =>
            //{
            //    Console.WriteLine("Name: {0}\nAge: {1}\n\n", name, age);
            //};

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

            Console.WriteLine("Run tasks demo ended...");
        }
    }
}
