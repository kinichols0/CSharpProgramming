using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using ProgramFlow.TaskDemos.Models;

namespace ProgramFlow.TaskDemos
{
    public static class ParallelDemo
    {
        public static void RunParallelForBasic()
        {
            Console.WriteLine("Parallel For Demo started...\n\n");

            CancellationTokenSource cancellationSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions();
            options.CancellationToken = cancellationSource.Token;

            var rnd = new Random();// intitialize random number generator
            int breakIndex = rnd.Next(1, 11);// index we'll break execution
            long? lowest = null;

            Console.WriteLine("Breaking at iteration {0}\n\n", breakIndex);

            try
            {
                ParallelLoopResult result = Parallel.For(1, 11, options, (i, state) =>
                {
                    Console.WriteLine("Starting iteration {0}", i);

                    int delay;
                    Monitor.Enter(rnd);// lock the Random obj while the current thread is using it
                    delay = rnd.Next(1, 1001);
                    Monitor.Exit(rnd);// release the Random obj for use by another thread
                    Console.WriteLine("Delaying iteration {0} for {1} milisecond(s)", i, delay);
                    Thread.Sleep(delay);// delay execution

                    // if we should exit the current iteration and the lowest break iteration index is lower then the current, break the loop
                    if (state.ShouldExitCurrentIteration && state.LowestBreakIteration < i)
                    {
                        Console.WriteLine("Do not contiue executing iteration {0}", i);
                        return;
                    }

                    // if we're at the break index execute Break here
                    if (i == breakIndex)
                    {
                        Console.WriteLine("Break within iteration {0}", i);
                        state.Break();// do not execute iterations after the current unless already started
                        lowest = state.LowestBreakIteration;
                    }

                    Console.WriteLine("Completed iteration {0}", i);
                });

                // while loop executes until all loop iterations are completed
                while (!result.IsCompleted) { }

                if (lowest.HasValue)
                    Console.WriteLine("Lowest break iteration was {0}\n\n", lowest.Value);
                else
                    Console.WriteLine("No break occured\n\n");
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Operation canceled {0}", e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error occured: {0}", e.ToString());
            }
            finally
            {
                // dispose the cancellation source
                cancellationSource.Dispose();
            }

            Console.WriteLine("Parallel For Demo has ended...\n\n");
        }

        public static void ParallelForAdditionRun()
        {
            Console.WriteLine("Parallel For addition run started...\n\n");

            long total = 0;
            var rnd = new Random();
            var numberOfAdditions = rnd.Next(10, 21);// total number of iterations

            Console.WriteLine("Current total is {0} \nTotal number of iterations will be {1}", total, numberOfAdditions - 1);

            var result = Parallel.For(1, numberOfAdditions, (i, state) =>
            {
                // start the stopwatch
                Stopwatch sw = new Stopwatch();
                sw.Start();

                // lock the random number generator while this process gets a random number to add
                long amountToAdd;
                Monitor.Enter(rnd);
                amountToAdd = rnd.Next(1, 101);
                Monitor.Exit(rnd);

                // lock the total sum variable while adding to it.
                Interlocked.Add(ref total, amountToAdd);

                // stop the stopwatch and record elapsed time
                sw.Stop();
                Console.WriteLine("Iteration {0} added {1} to total. \nThread execution time span: {2}\n\n", i, amountToAdd, sw.Elapsed.ToString());
            });

            Console.WriteLine("Total is now {0}", total);
            Console.WriteLine("Parallel For addition run ended...\n\n");
        }

        public static void ParallelForEach()
        {
            Console.WriteLine("Parallel foreach started...\n\n");
            string[] names = { "Kelvin", "Brittany", "Jayde", "Harold", "John", "Peter", "Thomas", "Richard" };
            var rnd = new Random();

            Parallel.ForEach<string, int>(names, // source
                () => { return Thread.CurrentThread.ManagedThreadId; }, // threadLocal storage data
                (name, loopState, threadId) => {// body
                    int sleepTime;
                    lock (rnd)// Lock the random number resource while in use
                    {
                        sleepTime = rnd.Next(100, 1001);
                    }
                    Thread.Sleep(sleepTime);

                    Console.WriteLine("{0}: {1}\n", threadId, name);
                    return threadId;
                },
                (threadId) => {// final action to be made
                    
                });

            Console.WriteLine("Parallel foreach ended...\n\n");
        }

        public static void ParallelInvokeRun()
        {
            Console.WriteLine("Parallel Invoke demo started....\n\n");

            Parallel.Invoke(
            () => {// Lamda
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine("Hello Cat {0}", i);
                }
            },
            delegate() {// delegate
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine("Hello Dog {0}", i);
                }
            });

            Console.WriteLine("\n\nParallel Invoke demo ended...");
        }
    }
}
