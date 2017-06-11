using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using ProgramFlow.TaskDemos.Models;

namespace ProgramFlow.TaskDemos
{
    public class ThreadingDemo
    {
        #region Basic Thread Demo

        private static AutoResetEvent autoEvent;

        public static void BasicRun()
        {
            Console.WriteLine("Threading basic demo run started...");

            autoEvent = new AutoResetEvent(false);

            // create and start the thread
            Thread thread = new Thread(BasicRunMethod);
            thread.Start();

            // wait for the one thread to end
            autoEvent.WaitOne();

            Console.WriteLine("Ended threading basic run...\n");
        }

        private static void BasicRunMethod()
        {
            // run the thread process
            Console.WriteLine("Basic thread process writing to the console...");

            // signal that the one thread ended
            autoEvent.Set();
        }

        #endregion

        #region Basic Background Worker Demo
        public static void RunBackagroundWorker()
        {
            Console.WriteLine("Started background worker demo...\n\n");

            // Initialize background worker
            BackgroundWorker worker = new BackgroundWorker();

            // Hook up event handlers
            worker.DoWork += new DoWorkEventHandler(BackgroundWorkerEventHandler);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerRunCompleted);

            // Start the worker with the Shape object passed as a param as an argument
            worker.RunWorkerAsync(new Shape() { Base = 20, Height = 55 });

            Console.WriteLine("Ended background worker demo...\n\n");
        }

        private static void BackgroundWorkerEventHandler(object sender, DoWorkEventArgs e)
        {
            Shape shape = (Shape)e.Argument;
            e.Result = shape.CalcArea;
        }

        private static void BackgroundWorkerRunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double area = (double)e.Result;
            Console.WriteLine("The area is {0}", area.ToString());
        }
        #endregion

        #region Threadpool demo
        public static void ThreadPoolDemoRun()
        {
            Console.WriteLine("Started Threadpool demo run...");

            const int threadCount = 10;
            ManualResetEvent[] doneEvents = new ManualResetEvent[threadCount];
            Random rnd = new Random();
            string[] fibResults = new string[threadCount];

            Console.WriteLine("Launching {0} tasks...\n\n", threadCount);

            for(int i = 0; i < threadCount; i++)
            {
                ThreadPoolDemoContext context = new ThreadPoolDemoContext()
                {
                    ResetEvent = doneEvents[i] = new ManualResetEvent(false),
                    ContextData = new Dictionary<string, object>
                    {
                        ["Index"] = i,
                        ["FibNumber"] = rnd.Next(20, 40),
                        ["FibResultsArray"] = fibResults
                    }
                };
                ThreadPool.QueueUserWorkItem(ThreadPoolDemoRunProc, context);
            }

            // wait for all the threads in the pool to complete
            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("All threads are complete...");

            // Display results
            for(int i = 0; i < fibResults.Length; i++)
            {
                Console.WriteLine(fibResults[i]);
            }
        }

        private static void ThreadPoolDemoRunProc(Object threadContext)
        {
            var context = (ThreadPoolDemoContext)threadContext;
            var index = (int)context.ContextData["Index"];
            var fibNumber = (int)context.ContextData["FibNumber"];
            var resultArray = ((string[])context.ContextData["FibResultsArray"]);

            // thread process
            Console.WriteLine("Thread {0} started...", index);
            int fibResult = FibonacciCalc((int)context.ContextData["FibNumber"]);
            Console.WriteLine("Thread {0} result calculated...", index);

            // store the result
            resultArray[index] = string.Format("Fibonacci({0}) = {1}", fibNumber, fibResult);

            // signal the done event
            context.ResetEvent.Set();
        }

        private static int FibonacciCalc(int number)
        {
            if(number <= 1)
                return number;
            return FibonacciCalc(number - 1) + FibonacciCalc(number - 2);
        }
        #endregion

        #region Background worker cancellation demo

        public static void BackgroundWorkerCancellationDemo()
        {
            Console.WriteLine("Started background worker cancellation demo...");

            // initialize the worker. set worker flags to support cancellation and report progress
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            // hook up event handlers
            worker.DoWork += BackgroundWorker_DoWork;
            worker.RunWorkerCompleted += BackgroundWorker_WorkCompleted;

            // start the worker
            worker.RunWorkerAsync();

            var cancelKey = Console.ReadLine();
            if (cancelKey == "cancel" && worker.IsBusy)
                worker.CancelAsync();
            else
                Console.WriteLine("Process has been completed");

            Console.WriteLine("Ended background worker cancellation demo...");
        }

        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Two minute process has begun. \nEnter 'cancel' to end the process sooner.");
            var worker = (BackgroundWorker)sender;

            // Process that runs for two minutes
            for(int i = 0; i < 120; i++)
            {
                // if cancelled break.
                if (worker.CancellationPending)
                    break;

                Thread.Sleep(1000);
                Console.WriteLine("{0} seconds passed", i + 1);
            }
        }

        private static void BackgroundWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                Console.WriteLine("Error: {0}", e.Error.Message);
            else if (e.Cancelled)
                Console.WriteLine("Process canceled");
            else
                Console.WriteLine("Process finished");
        }

        #endregion
    }
}
