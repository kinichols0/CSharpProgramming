using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace CSharpProgramming.SecurityDebugging
{
    public class KeyEncryptionDecryption
    {
        public static void GeneratingKeysDemo()
        {
            // Initialize TripleDES service provider, implements TripleDES algorithms
            TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();

            // generate new symmetric keys
            TDES.GenerateIV();
            TDES.GenerateKey();
            var key = TDES.Key;
            var iv = TDES.IV;
            Console.WriteLine("Triple DES symmetric keys:\nIV: {0}\nKey: {1}\n", string.Join(" ", TDES.IV.Select(t => t.ToString())), 
                string.Join(" ", TDES.Key.Select(t => t.ToString())));

            // Initialize a class that derives from AsymmetricAlgorithm with the container
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            Console.WriteLine("RSA asymmetric keys:\n{0}\n", rsaProvider.ToXmlString(true));
        }

        /// <summary>
        /// Storing Asymmetric keys in Key Container
        /// </summary>
        public static void StoringAsymmetricKeysDemo()
        {
            string containerName = "StorageContainer";
            SaveKeyInContainer(containerName);
            GetKeyInContainer(containerName);
            DeleteKeyInContainer(containerName);
        }

        /// <summary>
        /// Symmetric encryption is performed on streams and is therefore useful to encrypt large amounts of data.
        /// </summary>
        public static void SymmetricEncryptionDecryptionDemo()
        {
            // Symmetric keys
            byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

            Task[] tasks = new Task[] { Task.Factory.StartNew(() => {
                // create a tcp listener, start it, and listen for a connection every five seconds.
                TcpListener tcpListener = new TcpListener(IPAddress.Any, 10000);
                tcpListener.Start();
                while (!tcpListener.Pending())
                {
                    Console.WriteLine("TCP listener is listening. Will try again in five seconds");
                    Thread.Sleep(5000);
                }

                // Accept the client if one is found
                using(TcpClient client = tcpListener.AcceptTcpClient())
                {
                    // create a network stream from the client
                    using(NetworkStream stream = client.GetStream())
                    {
                        // use the Rijndael class to decrypt the stream
                        RijndaelManaged rmCrypto = new RijndaelManaged();
                        using(CryptoStream cryptoStream = new CryptoStream(stream, rmCrypto.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                        {
                            // read from the stream
                            using(StreamReader reader = new StreamReader(cryptoStream))
                            {
                                Console.WriteLine("The decrypted message:\n{0}\n", reader.ReadToEnd());
                            }
                        }
                    }
                }
            }), Task.Factory.StartNew(() => {
                // sleep for two seconds while listener gets started
                Thread.Sleep(2000);

                // create a tcp client
                using (TcpClient tcp = new TcpClient("localhost", 10000))
                {

                    // create a network stream from the tcp connection
                    using (NetworkStream netStream = tcp.GetStream())
                    {
                        // initialze the class that implements the Rijndael encryption algorithm
                        RijndaelManaged rmCrypto = new RijndaelManaged();

                        // create a CryptoStream to encrypt the Network stream we created
                        using (CryptoStream cryptStream = new CryptoStream(netStream, rmCrypto.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                        {

                            // initialize a streamwriter to write to the NetWork stream.
                            using (StreamWriter writer = new StreamWriter(cryptStream))
                            {

                                // write to the stream
                                writer.Write("This message was encrypted by a symmetric key");
                                Console.WriteLine("Message was sent");
                            }
                        }
                    }
                }
            }) };

            Task.WaitAll(tasks);
        }

        /// <summary>
        /// Asymmetric encryption is performed on a small number of bytes and is therefore useful for only small
        /// amounts of data
        /// </summary>
        public static void AsymmetricEncryptionDecryptionDemo()
        {

        }

        public static void AESByteEncryptionDecryptionDemo()
        {

        }

        /// <summary>
        /// Encrypt string to a byte array using AES symmetric key algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private static byte[] AES_EncryptString(string text, byte[] key, byte[] iv)
        {
            byte[] encrypted;

            using (Aes aesObj = Aes.Create())
            {
                aesObj.Key = key;
                aesObj.IV = iv;

                ICryptoTransform encryptor = aesObj.CreateEncryptor(aesObj.Key, aesObj.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(text);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypt string from byte array using AES symmetric key algorthm
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private static string AES_DecryptString(byte[] encryptedText, byte[] key, byte[] iv)
        {
            string text;

            using (Aes aesObj = Aes.Create())
            {
                aesObj.Key = key;
                aesObj.IV = iv;

                ICryptoTransform decryptor = aesObj.CreateEncryptor(aesObj.Key, aesObj.IV);

                using (MemoryStream ms = new MemoryStream(encryptedText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            text = reader.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Save keys in Key Container
        /// </summary>
        /// <param name="containerName"></param>
        private static void SaveKeyInContainer(string containerName)
        {
            // Containers to properly save the keys
            CspParameters cp = new CspParameters() { KeyContainerName = containerName };

            // Initialize a class that derives from AsymmetricAlgorithm with the container
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cp);

            Console.WriteLine("Key added to container:\n{0}\n", rsaProvider.ToXmlString(true));
        }

        /// <summary>
        /// Get keys in key container. Method looks similar to SaveKeyInContainer. If provider is initialized
        /// with container name that exists, the container will return those existing keys, otherwise new keys
        /// are generated and inserted.
        /// </summary>
        /// <param name="containerName"></param>
        private static void GetKeyInContainer(string containerName)
        {
            // Containers to properly save the keys
            CspParameters cp = new CspParameters() { KeyContainerName = containerName };

            // Initialize a class that derives from AsymmetricAlgorithm with the container
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cp);

            Console.WriteLine("Key retrieved from container:\n{0}\n", rsaProvider.ToXmlString(true));
        }

        /// <summary>
        /// Delete keys in the container
        /// </summary>
        /// <param name="containerName"></param>
        private static void DeleteKeyInContainer(string containerName)
        {
            // Containers to properly save the keys
            CspParameters cp = new CspParameters() { KeyContainerName = containerName };

            // Initialize a class that derives from AsymmetricAlgorithm with the container
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cp);

            // delete
            rsaProvider.PersistKeyInCsp = false;

            // release all resources
            rsaProvider.Clear();

            Console.WriteLine("Key deleted");
        }
    }
}
