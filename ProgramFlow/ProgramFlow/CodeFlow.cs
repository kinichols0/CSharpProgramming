using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.ProgramFlow
{
    public static class CodeFlow
    {
        /// <summary>
        /// Checked demo. Checked can be used to explicitly enable overflow checking
        /// for integral-type operations and conversions.
        /// Checked enables custom overflow checking at runtime.
        /// </summary>
        public static void CheckedBlockDemo()
        {
            try
            {
                checked
                {// error will be thrown
                    int num = 50;
                    Console.WriteLine("Adding ints {0} + {1}", int.MaxValue, 50);
                    Console.WriteLine("Sum {0}", int.MaxValue + num);
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e);
            }

            try
            {
                int num1 = 50;
                Console.WriteLine("Adding ints {0} + {1}", int.MaxValue, num1);
                Console.WriteLine("Sum {0}", checked(num1 + int.MaxValue));// checked alternative notation
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Disable overflow checking
        /// </summary>
        public static void UncheckedDemo()
        {
            checked
            {
                int max = int.MaxValue;
                int num = 10;
                
                try
                {
                    Console.WriteLine("Adding {0} + {1}", max, num);

                    // since overflow is checked, the below will throw an exception
                    int sum = num + max;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Could not add {0} and {1}", max, num);
                    Console.WriteLine(e.Message);
                }

                /* use unchecked to break out of checked blocks for areas that may not 
                 * cause overflow errors, improves performance 
                 */
                unchecked
                {
                    int sum = max + num;// allowed because of the unchecked block
                    Console.WriteLine("overflow not checked: sum of {0} value and {1} is {2}", max, num, sum);
                }
            }
        }

        /// <summary>
        /// Unsafe demo.
        /// Any code involving pointers must be excuted within a method 
        /// declared unsafe or a block of code declared unsafe. The keyword
        /// fixed can only be used in an unsafe scope.
        /// Fixed statements keeps the garabage collector from removing movable
        /// objects.
        /// </summary>
        public unsafe static void UnsafeCodeFixedPointersDemo()
        {
            int[] array = new int[]{ 1, 2, 3, 4, 5 };
            Console.WriteLine(string.Join("|", array.Select(t => t.ToString())));

            /* initialize a pointer using an array
             * pins the array making it immovable. The garbage collector
             * cannot pick it up. Fixed only works with fixed size buffers
             * such as arrays.
             **/
            fixed (int* p = array)
            {
                // the pointer is at 3
                Console.WriteLine("Pointer: {0}", *p);

                // the pointer increments to 4
                Console.WriteLine("Pointer: {0}", ++(*p));

                // the pointer increments to 5
                Console.WriteLine("Pointer: {0}", ++(*p));

                // convert the pointer to a double pointer type
                double* t = (double*)p;

                Console.WriteLine("Pointer: {0}", *t);
            }
            /* after the block of code, the pointer is unpinned a subject to garbage collection */
            Console.WriteLine(string.Join("|", array.Select(t => t.ToString())));
        }
    }
}
