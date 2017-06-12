using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramFlow.TaskDemos
{
    public class ConcurrentCollectionsDemo
    {
        public static void BlockingCollectionAddDemo()
        {
            Console.WriteLine("Started blocking collection demo...");

            var blockingCollection = new BlockingCollection<int>();
            int sum = 0;
            int numbers = 100;

            // Add integers to the blocking collection
            for (int i = 0; i <= numbers; i++)
            {
                blockingCollection.Add(i);
            }

            // Mark no more adding allowed
            blockingCollection.CompleteAdding();

            // Delegate that removes items from the blocking collection and add them to the sum
            Action AddToSum = () =>
            {
                int localItem;
                int localSum = 0;

                while (blockingCollection.TryTake(out localItem))
                {
                    localSum += localItem;
                }

                Interlocked.Add(ref sum, localSum);
            };

            // Run the delegate in four separate processes
            Parallel.Invoke(AddToSum, AddToSum, AddToSum, AddToSum);

            Console.WriteLine("Sum[0...{0}] = {1}", numbers, sum);
            Console.WriteLine("BlockingCollection.IsCompleted = {0}", blockingCollection.IsCompleted);
            Console.WriteLine("Ended blocking collection demo...");
        }
    }
}
