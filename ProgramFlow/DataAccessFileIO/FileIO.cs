using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CSharpProgramming.DataAccessFileIO
{
    public static class FileIO
    {
        /// <summary>
        /// Read data from a file to a FileStream then write the
        /// data to another file stream and output to a file
        /// </summary>
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

                        // break the loop if n = 0, no bytes to read
                        if (n == 0)
                            break;

                        numBytesRead += n;// increment the remaining number of bytes to read by n
                        numBytesToRead -= n;// decrement the remaining number of bytes to read by n
                    }

                    // write the byte stream to the console
                    Console.WriteLine("File byte stream:\n{0}\n", string.Join(" ", byteStream.Select(t => t.ToString())));

                    // write the byte array to the destination file stream
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

        /// <summary>
        /// Read/Write to a file using BinaryReader and BinaryWriter
        /// </summary>
        public static void BinaryReadBinaryWriteFileStream()
        {
            try
            {
                Console.WriteLine("Copy file to stream using BinaryReader and BinaryWriter demo\n");

                string fileSource = @"..\..\OutputFiles\FileIO\sourceFile.txt";
                string fileDestination = @"..\..\OutputFiles\FileIO\destinationFile.txt";

                // Initialize a FileStream object to open and read the source file
                using (FileStream fileStream = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
                {
                    // copy bytes to a byte array
                    byte[] fileBytes;
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        fileBytes = reader.ReadBytes((int)fileStream.Length);
                    }

                    // Initialize a FileStream object to create and write to an output file
                    using (FileStream fileStreamDestination = new FileStream(fileDestination, FileMode.Create, FileAccess.Write))
                    {
                        // write bytes to the output file
                        using (BinaryWriter writer = new BinaryWriter(fileStreamDestination))
                        {
                            writer.Write(fileBytes);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:\n{0}\n", e);
            }
        }

        public static void ReadWriteToNetworkStream()
        {
            // TCPClient will send a message to a TCPListener on the same port and hostname
            Task taskSender = Task.Factory.StartNew(() =>
            {
                // sleep for two seconds while listener gets started
                Thread.Sleep(2000);

                // create a tcp client
                using (TcpClient tcp = new TcpClient("localhost", 10000))
                {
                    // create a network stream from the tcp connection
                    using (NetworkStream netStream = tcp.GetStream())
                    {
                        using (StreamWriter writer = new StreamWriter(netStream))
                        {
                            string message = "Hello there!";
                            writer.WriteLine(message);
                        }
                    }
                }
            });

            // TCP listener will listen for messages and display them when received
            Task taskListener = Task.Factory.StartNew(() => {
                // create a tcp listener, start it, and listen for a connection every five seconds.
                TcpListener tcpListener = new TcpListener(IPAddress.Any, 10000);
                tcpListener.Start();
                while (!tcpListener.Pending())
                {
                    Console.WriteLine("TCP listener is listening for messages...");
                    Thread.Sleep(5000);
                }

                // Accept the client if one is found
                using (TcpClient client = tcpListener.AcceptTcpClient())
                {
                    // create a network stream from the client
                    using (NetworkStream stream = client.GetStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string message = reader.ReadToEnd();
                            Console.WriteLine("Message Received: {0}", message);
                        }
                    }
                }
            });

            Task[] tasks = new Task[] { taskSender, taskListener };

            Task.WaitAll(tasks);
        }
    }
}
