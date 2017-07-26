using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using CSharpProgramming.Common.Models;

namespace CSharpProgramming.ProgramFlow
{
    public static class ThreadingDemo
    {
        #region Basic Thread Demo

        public static void BasicRun()
        {
            Console.WriteLine("Threading basic demo run started...");

            int resetEventsCnt = 4;
            AutoResetEvent[] resetEvents = new AutoResetEvent[resetEventsCnt];
            Random rnd = new Random();

            for (int i = 0; i < resetEventsCnt; i++)
            {
                // initialize reset event
                resetEvents[i] = new AutoResetEvent(false);

                // initialize threads with the reset events
                var dictionary = new Dictionary<string, object>()
                {
                    ["ResetEvent"] = resetEvents[i],
                    ["RunTimeSeconds"] = rnd.Next(1, 4),
                    ["ThreadId"] = i + 1
                };

                /* paramater method must be in form of ParameterizedThreadStart delegate
                * delegate void ParameterizedThreadStart(object data);
                * */
                Thread thread = new Thread(BasicRunMethod);

                /* dictionary passed as the data parameter to the method in the form of ParameterizedThreadStart delegate
                * passed to the constructor of Thread in the previous line of code
                * */
                thread.Start(dictionary);
            }

            // wait for all rest events to signal
            WaitHandle.WaitAll(resetEvents);
        }

        /// <summary>
        /// Method in the form of ParameterizedThreadStart delegate so it can be passed to a 
        /// Thread(param) method as param.
        /// </summary>
        /// <param name="obj"></param>
        private static void BasicRunMethod(object obj)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)obj;
            AutoResetEvent resetEvent = (AutoResetEvent)data["ResetEvent"];
            int threadId = (int)data["ThreadId"];
            int runTime = (int)data["RunTimeSeconds"];

            try
            {
                // run the thread process
                for(int i = 0; i < runTime; i++)
                {
                    Console.WriteLine("Thread {0} executing...", threadId);
                    Thread.Sleep(1000);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Error for thread Id {0}", threadId);
            }
            finally
            {
                // signal that this thread is completed
                Console.WriteLine("Thread {0} done!", threadId);
                resetEvent.Set();
            }
        }

        #endregion

        #region Basic Background Worker Demo
        /// <summary>
        /// Basic background worker demo. The background worker is calculating area while other work executes.
        /// </summary>
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

            // other work to do while the background worker is running
            for(int i=1; i<=5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Doing other work...{0} second(s) have passed...", i);
            }

            Console.WriteLine("Ended background worker demo...\n\n");
        }

        /// <summary>
        /// Background worker's DoWork implementation. Code to run when the background worker starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BackgroundWorkerEventHandler(object sender, DoWorkEventArgs e)
        {
            // Get the object passed as a parameter from the e.Argument property.
            Shape shape = (Shape)e.Argument;
            for(int i=1; i<=5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Calculating area...{0} second(s) have passed...", i);
            }
            e.Result = shape.CalcArea;
        }

        /// <summary>
        /// Background RunWorkerCompleted implementation. Code to run when the worker completes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BackgroundWorkerRunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double area = (double)e.Result;
            Console.WriteLine("Area calculated. The area is {0}", area.ToString());
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

        /// <summary>
        /// Background workers can run intensive tasks on another thread to free up other threads such as threads for the UI
        /// </summary>
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

        /// <summary>
        /// Background worker's DoWork implementation. Code to run when the worker starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Backgroundworker WorkCompleted implementation. Code to run when the work is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region EventWaitHandle, AutoResetEvent and ManualResetEvent demos

        public static void EventWaitHandleDemo()
        {
            Console.WriteLine("EventWaitHandle, the base class of AutoResetEvent and ManualResetEvent");
            int threadsToProduce = 5;

            // create AutoReset EventWaitHandle
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            for(int i = 1; i <= threadsToProduce; i++)
            {
                Thread thread = new Thread((handle) => ThreadOperation(waitHandle)) { Name = "Thread_" + i };
                thread.Start();
            }
            Thread.Sleep(1000);
            Console.WriteLine("\nPress any key to release all threads");
            Console.ReadKey();

            // set to signaled and remain signaled
            waitHandle.Set();
            Thread.Sleep(1000);
            Console.WriteLine("\nPress any key to manually reset the EventWaitHandle and start a new thread.");
            Console.ReadKey();

            // reset the manual waitHandle
            waitHandle.Reset();
            Thread threadNew = new Thread((handle) => ThreadOperation(waitHandle)) { Name = "Thread_New" };
            threadNew.Start();
            Thread.Sleep(1000);
            Console.WriteLine("\nPress any key to release the thread");
            Console.ReadKey();

            // release the thread
            waitHandle.Set();
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Demo differences between ManualResetEvent and AutoResetEvent
        /// </summary>
        public static void AutoResetManualResetEventDiffs()
        {
            Console.WriteLine("ManualResetEvent demo");

            // ManualResetEvent demo
            ManualResetEvent manuelReset = new ManualResetEvent(false);
            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread((evh) => ThreadOperation(manuelReset)) { Name = "Thread_" + i };
                thread.Start();
            }
            Thread.Sleep(500);
            Console.Write("Press enter to signal all threads to complete...");
            Console.ReadKey();

            /* Signal all threads that reference this ManualReset object to continue.
             * The Set() fro MaunualResetEvent does not automatically reset to unsignaled
             * so all threads blocked on WaitOne() will be released.
             * */
            manuelReset.Set();// set this ManualResetEvent object to signaled
            Thread.Sleep(500);
            Console.WriteLine("\nPress any key to manually call Reset() so another thread can be blocked on WaitOne().");
            Console.ReadKey();

            // Manually reset so next initialized threads can block on this ManualResetEvent instance
            manuelReset.Reset();

            // Initialize a new thread to use the current ManualResetEvent instance for block and release signals
            Thread threadNew = new Thread((mr) => ThreadOperation(manuelReset)) { Name = "Thread_New" };
            threadNew.Start();
            Thread.Sleep(500);
            Console.WriteLine("Press any key to release Thread_New");
            Console.ReadKey();

            // set this ManualResetEvent object to signaled
            manuelReset.Set();
            Thread.Sleep(500);

            // AutoResetEvent demo
            Console.WriteLine("\nPress any key to start AutoResetEvent demo.");
            Console.ReadKey();
            AutoResetEvent autoReset = new AutoResetEvent(false);
            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread((evh) => ThreadOperation(autoReset)) { Name = "Thread_" + i };
                thread.Start();
            }

            /* AutoResetEvent when signaled automatically resets to unsignaled after each Set() call so 
             * Set() will need to be called for each blocked thread.
             **/
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("\nPress any key to signal a thread to complete...");
                Console.ReadKey();

                // Signals/blocks one thread then resets to unsignaled leaving remaining blocked threads blocked
                autoReset.Set();
                Thread.Sleep(500);
            }
        }

        private static void ThreadOperation(object obj)
        {
            if (obj is EventWaitHandle evh)
            {
                string resetEventType = obj is AutoResetEvent ? "AutoResetEvent" : "ManualResetEvent";

                string name = Thread.CurrentThread.Name;
                Console.WriteLine("Thread {0} is executing thread work and calls WaitOne() on {1} reset event type.", name, resetEventType);

                // Block this thread until signaled
                evh.WaitOne();

                Console.WriteLine("Thread {0} is done.", name);
            }
        }

        #endregion
    }
}
