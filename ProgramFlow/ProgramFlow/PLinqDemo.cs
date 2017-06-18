using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Entities;

namespace CSharpProgramming.ProgramFlow
{
    public class PLinqDemo
    {
        public static void LinqBasicDemo()
        {
            Console.WriteLine("Started Linq basic demo...");

            // categories
            List<Category> categories = new List<Category>()
            {
                new Category() { CategoryId = 1, CategoryName = "Electronics" },
                new Category() { CategoryId = 2, CategoryName = "Health" },
                new Category() { CategoryId = 3, CategoryName = "Sports" }
            };

            // products
            List<Product> products = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1, ProductName = "Laptop" },
                new Product() { ProductId = 2, CategoryId = 1, ProductName = "Monitor" },
                new Product() { ProductId = 3, CategoryId = 2, ProductName = "Lotion" },
                new Product() { ProductId = 4, CategoryId = 2, ProductName = "Soap" },
                new Product() { ProductId = 5, CategoryId = 1, ProductName = "Headphones" },
            };

            // display the categories
            Console.WriteLine("Categories:");
            categories.ForEach(t => Console.WriteLine(t));

            // display the products
            Console.WriteLine("Products:");
            products.ForEach(t => Console.WriteLine(t));

            // query to left join categories to products
            var query = from c in categories
                        join p in products on c.CategoryId equals p.CategoryId into productGroup
                        from g in productGroup.DefaultIfEmpty(null)
                        select new { CategoryName = c.CategoryName, Product = g };

            // execute the query and display the results
            Console.WriteLine("\nLinq Query Results:");
            foreach(var category in query.OrderBy(t => t.CategoryName))
            {
                Console.WriteLine("CategoryName: {0} - Product: {1}", category.CategoryName, category.Product);
            }

            Console.WriteLine("Ended Linq basic demo...");
        }

        public static void PLinqBasicDemo()
        {
            Console.WriteLine("Started PLinq basic demo...");
            Console.WriteLine("Displaying all numbers between 1 and 100 that are odd\n");

            // query that filters the odd numbers in parallel
            var source = Enumerable.Range(1, 100);
            var oddNumbers = from num in source.AsParallel()
                             where num % 2 == 1
                             select num;

            // query that filters the odd numbers in parallel and preserves order
            var oddNumbers2 = from num in source.AsParallel().AsOrdered()
                              where num % 2 == 1
                              select num;

            // display all odd numbers between 1 and 100.
            Console.WriteLine("There are a total of {0} odd numbers. \n{1} \nOrdered odd numbers \n{2}"
                , oddNumbers.Count(), string.Join(",", oddNumbers.ToArray()), string.Join(",", oddNumbers2.ToArray()));

            Console.WriteLine("Ended PLinq basic demo...");
        }

        public static void PLinqForAllDemo()
        {
            Console.WriteLine("Started PLinq ForAll Demo...");

            Console.WriteLine("Displaying all numbers between 1 and 100 divisible by 3\n");

            // parallel collections that each linq process will add back to with order preserved
            ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();

            // query to filter all number between 1 and 100 divisible by 3
            var source = Enumerable.Range(1, 100);
            var query = from num in source.AsParallel()
                        where num % 3 == 0
                        select num;

            // execute the query and print to the console
            query.ForAll(t => Console.WriteLine(t));

            Console.WriteLine("Ended PLinq ForAll demo...");
        }
    }
}
