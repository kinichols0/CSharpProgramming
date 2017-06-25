using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CSharpProgramming.SecurityDebugging
{
    public class DataIntegrityDemo
    {
        /// <summary>
        /// Data can be compared using a hash value to check for integrity. If the hash value
        /// of one set of data is different then the hash value of another set then both data
        /// are different. If their hash values are the same then the data is implied
        /// to be identitical.
        /// </summary>
        public static void HashingDemo()
        {
            Console.WriteLine("Data Integrity Hashing demo...");

            // display the original message
            var message = "This is my original message!";
            Console.WriteLine("My original message:\n{0}\n", message);

            // get and write the hash value to the console
            var originalHash = SHA1HashGenerator(message);
            Console.WriteLine("Message hash value:\n{0}\n",
                string.Join(" ", originalHash.ToList().Select(b => b.ToString())));

            // Display new message
            var newMessage = "This is a new message!";
            Console.WriteLine("New message \n{0}\n", newMessage);

            // Display new message hash value
            var newHash = SHA1HashGenerator(newMessage);
            Console.WriteLine("New message hash value:\n{0}\n",
                string.Join(" ", newHash.ToList().Select(b => b.ToString())));

            // Compare hashes
            Console.WriteLine("New hash equals original hash: {0}", newHash.Equals(originalHash));
            Console.WriteLine("Original equals original hash: {0}", originalHash.Equals(originalHash));
        }

        private static byte[] SHA1HashGenerator(string msg)
        {
            // convert the string into an array of Unicode bytes using UnicodeEncoding class
            UnicodeEncoding uEncoding = new UnicodeEncoding();
            byte[] msgBytes = uEncoding.GetBytes(msg);

            // compute the hash using SHA1Managed class
            SHA1Managed sha1Hash = new SHA1Managed();
            byte[] hashValue = sha1Hash.ComputeHash(msgBytes);

            return hashValue;
        }
    }
}
