using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Implementations;

namespace CSharpProgramming.TypesClasses
{
    public static class StringManipulation
    {
        /// <summary>
        /// StringBuilder demo, a mutable string class
        /// </summary>
        public static void StringBuilderDemo()
        {
            Console.WriteLine("StringBuilder demo...");

            // initialize stringbuilder with "ABC" and fixed length of 50
            StringBuilder sb = new StringBuilder("ABC", 50);
            Console.WriteLine(sb);

            // insert "Alphabet: "
            Console.WriteLine(@"Inserting ""Alphabet:""");
            sb.Insert(0, "Alphabet: ");
            Console.WriteLine(sb);

            // append "_ACB"
            Console.WriteLine(@"Appending ""_ACB""");
            sb.Append("_ACB");
            Console.WriteLine(sb);

            // replace "A" with "$"
            Console.WriteLine(@"Replaceing all ""A"" with ""$""");
            sb.Replace('A', '$');
            Console.WriteLine(sb);

            // remove the last three characters from the string
            Console.WriteLine("Removing the last three characters");
            sb.Remove(sb.Length - 3, 3);
            Console.WriteLine(sb);

            // write out each character
            Console.WriteLine("Writing out each character");
            for (int i = 0; i < sb.Length; i++)
                Console.WriteLine(sb[i]);
        }

        public static void StringReaderStringWriterDemo()
        {
            Console.WriteLine("\nStringWriter demo");

            string sentences = "New Initialized StringBuilder.\n" +
                "This is the second line of text.";

            StringBuilder sb = new StringBuilder();

            // StringReader to read from the "sentences" string
            using (StringReader reader = new StringReader(sentences))
            {
                // read the string and add to the StringBuilder
                Console.WriteLine("Using StringReader to read the string and append to a StringBuilder object.");
                sb.Append(reader.ReadToEnd());

                //while (true)
                //{
                //    string line = reader.ReadLine();
                //    if (line != null)
                //        sb.AppendLine(line);
                //    else
                //        break;
                //}
            }

            // current string
            Console.WriteLine(@"Current StringBuilder object: ""{0}""", sb);

            // string writer to write to the StringBuilder
            using (StringWriter writer = new StringWriter(sb))
            {
                Console.WriteLine("Using StringWriter to append a line.");
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("StringWriter added previous empty lines and this line you're reading");
            }

            Console.WriteLine("Final version of the StringBuilder:\n" + sb);
        }

        /// <summary>
        /// String padding, trimming, changing case
        /// </summary>
        public static void StringOperationsDemo()
        {
            Console.WriteLine("String operations demo.");

            // initialize string from char[]
            char[] chars = new char[] { 'H', 'e', 'l', 'l', 'o', ' ', 't', 'h', 'e', 'r', 'e', '!' };
            string greeting = new string(chars);
            Console.WriteLine("Original string: {0}\n", greeting);

            // increase the length of the string and '-' in the empty spaces
            Console.WriteLine("Padded with five '-' to the left: {0}\n", greeting.PadLeft(greeting.Length + 5, '-'));
            Console.WriteLine("Padded with five '-' to the right: {0}\n", greeting.PadRight(greeting.Length + 5, '-'));

            // trim the '!' from the end of the string
            Console.WriteLine("Trimming '!': {0}\n", greeting.Trim(new char[] { '!' }));

            // trimming 'H' from the beginning of the string
            Console.WriteLine("Trimming 'H': {0}\n", greeting.TrimStart(new char[] { 'H' }));

            // remove four characters starting from index two
            Console.WriteLine("Removing four characters starting at index two: {0}\n", greeting.Remove(2, 4));

            // reomve the substring "there"
            Console.WriteLine("Removing substring 'there': {0}\n", greeting.Replace("there", ""));

            // make all caps
            Console.WriteLine("Upper case: {0}\n", greeting.ToUpper());

            // make lower case
            Console.WriteLine("Lower case: {0}\n", greeting.ToLower());

            // make title case
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            Console.WriteLine("Title case: {0}\n", ti.ToTitleCase(greeting));
        }

        /// <summary>
        /// Use an implementation of the ICustomerFormatter and IFormatProvider interfaces
        /// to format an object as a string.
        /// </summary>
        public static void ICustomFormatIFormProviderDemo()
        {
            string number = "123456789";
            Console.WriteLine(@"Format {0} to ""(xxx)-xxx-xxx"" format.", string.Format(new PhoneNumberFormatter(), "{0}", number));

            // format number passing "P" as the format parameter
            string phoneNumberFormat = string.Format(new PhoneNumberFormatter(), "{0:P}", number);
            Console.WriteLine("Phone number format: {0}", phoneNumberFormat);
        }
    }
}
