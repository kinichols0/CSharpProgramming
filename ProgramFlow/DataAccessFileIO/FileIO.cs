using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpProgramming.DataAccessFileIO
{
    public class FileIO
    {
        public static void ReadAndWriteToFileStream()
        {
            try
            {
                Console.WriteLine("Copy file to stream demo\n");

                string fileSource = @"..\..\OutputFiles\FileIO\sourceFile.txt";
                string fileDestination = @"..\..\OutputFiles\FileIO\destinationFile.txt";

                using (FileStream fileStream = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
                {
                    // intialize byte array that will hold the stream of bytes from the filestream
                    byte[] byteStream = new byte[fileStream.Length];
                    int numBytesToRead = (int)fileStream.Length;
                    int numBytesRead = 0;

                    // Read the source file into a byte array
                    while (numBytesToRead > 0)
                    {
                        // Read can return anything from 0 to numBytesToRead
                        int n = fileStream.Read(byteStream, numBytesRead, numBytesToRead);

                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }

                    Console.WriteLine("File byte stream:\n{0}\n",
                        string.Join(" ", byteStream.Select(t => t.ToString())));

                    // write the byte array to the destination file stream
                    numBytesToRead = byteStream.Length;
                    using (FileStream fileStreamDestination = new FileStream(fileDestination, FileMode.Create, FileAccess.Write))
                    {
                        fileStreamDestination.Write(byteStream, 0, numBytesRead);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:\n{0}\n", e);
            }
        }
    }
}
