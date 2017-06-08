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
            Console.WriteLine("Program started..." + DateTime.Now.ToString());
            Console.WriteLine();

            //// Run implicit concurrent task demo
            //ConcurrentTask.RunDemo();

            //// Run basic explicit task demo
            //ExplicitTask.RunDemo();

            //// Run demo of basic Parallel For method execution
            //ParallelForDemo.RunParallelForBasic();
            ParallelForDemo.ParallelForAdditionRun();

            Console.WriteLine("Program ended..." + DateTime.Now.ToString());

            Console.ReadLine();
        }
    }
}
