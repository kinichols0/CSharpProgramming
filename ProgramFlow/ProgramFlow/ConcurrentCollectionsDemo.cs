using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public static class ConcurrentCollectionsDemo
    {
        /// <summary>
        /// Blocking collection demo. Thread safe collection of items providing
        /// blocking and bounding capabilities. Producer and consumer pattern use.
        /// Blocks Add and Take operations on the collection.
        /// </summary>
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
                int localSum = 0;
                while (blockingCollection.TryTake(out int localItem))
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

        /// <summary>
        /// Concurrent dictionary demo. Thread safe collection of key value
        /// pairs.
        /// </summary>
        public static void ConcurrentDictionaryDemo()
        {
            Console.WriteLine("Started concurrent dictionary demo...");

            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            List<Task> tasks = new List<Task>() {
                new Task(() =>
                {
                    for(int i = 0; i < 20; i++)
                    {
                        if(concurrentDictionary.TryAdd(i, string.Format("added item for key {0}", i)))
                            Console.WriteLine("Task 1 added {0}", i);
                        else 
                            Console.WriteLine("Task 1 cannot add item for key {0}", i);
                    }
                }),
                new Task(() =>
                {
                    for(int i = 20; i > 0; i--)
                    {
                        if(concurrentDictionary.TryAdd(i, string.Format("added item for key {0}", i)))
                            Console.WriteLine("Task 2 added {0}", i);
                        else
                            Console.WriteLine("Task 2 cannot add item for key {0}", i);
                    }
                })
            };

            List<Task> updateTasks = new List<Task>();
            updateTasks.Add(new Task(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    if (concurrentDictionary.TryUpdate(i, string.Format("updated key {0}", i), string.Format("added item for key {0}", i)))
                        Console.WriteLine("Task 3 updated {0}", i);
                    else
                        Console.WriteLine("Task 3 could not update {0}", i);
                }
            }));

            updateTasks.Add(new Task(() =>
            {
                for (int i = 20; i > 0; i--)
                {
                    if (concurrentDictionary.TryUpdate(i, string.Format("updated key {0}", i), string.Format("added item for key {0}", i)))
                        Console.WriteLine("Task 4 updated {0}", i);
                    else
                        Console.WriteLine("Task 4 could not update {0}", i);
                }
            }));

            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Concurrent Dictionary has {0} item(s).", concurrentDictionary.Count);

            updateTasks.ForEach(t => t.Start());
            Task.WaitAll(updateTasks.ToArray());

            Console.WriteLine("Ended concurrent dictionary demo...");
        }

        /// <summary>
        /// Concurrent bag demo. Supports parallel operations on a thread safe
        /// unordered collection of objects
        /// </summary>
        public static void ConcurrentBagDemo()
        {
            Console.WriteLine("Started Concurrent Bag demo...");

            ConcurrentBag<string> bag = new ConcurrentBag<string>();
            for(int i = 0; i < 50; i++)
            {
                bag.Add("Index " + i);
            }

            Action<int> emptyBag = (taskId) =>
            {
                while (!bag.IsEmpty)
                {
                    if (bag.TryTake(out string item))
                        Console.WriteLine("Task-{0} took ({1}) from the bag.", taskId, item);
                }
            };

            Parallel.For(1, 3, emptyBag);
            
            Console.WriteLine("Ended Concurrent Bag demo...");
        }
    }
}
