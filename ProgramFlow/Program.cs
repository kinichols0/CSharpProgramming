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
            Console.WriteLine("Program started...{0}\n\n", DateTime.Now.ToString());

            //// Run demo of basic Parallel For method execution
            //ParallelDemo.RunParallelForBasic();
            //ParallelDemo.ParallelForAdditionRun();
            //ParallelDemo.ParallelForEach();
            ParallelDemo.ParallelInvokeRun();

            Console.WriteLine("\n\nProgram ended...,{0}", DateTime.Now.ToString());

            Console.ReadLine();
        }
    }
}
