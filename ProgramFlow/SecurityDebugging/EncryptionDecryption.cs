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
    public class EncryptionDecryption
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
        /// This demo uses Rijndael algorithm. Other symmetric algorithms are AES, DES, RC2, and TripleDES
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
        /// Asymmetric encryption is performed on a small number of bytes and is therefore useful for only small.
        /// This demo uses RSA for asymmetric encryption
        /// amounts of data
        /// </summary>
        public static void AsymmetricEncryptionDecryptionRSADemo()
        {
            try
            {
                Console.WriteLine("Asymmetric RSA Encryption/Decryption demo\n");

                // create the message
                string message = "Encrypted message";
                Console.WriteLine("Message to encrypt:\n{0}\n", message);

                // Byte encode the message
                UnicodeEncoding encoding = new UnicodeEncoding();
                var encodedMsg = encoding.GetBytes(message);
                Console.WriteLine("Byte encoded message:\n{0}\n",
                        string.Join(" ", encodedMsg.Select(t => t.ToString())));

                using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
                {
                    // encrypt the data, exclude the private key
                    byte[] encryptedData = RSAEncrypt(encodedMsg, rsaProvider.ExportParameters(false), false);
                    Console.WriteLine("Byte encrypted message message:\n{0}\n",
                        string.Join(" ", encryptedData.Select(t => t.ToString())));
                    
                    // decrypt the data, include the private key
                    byte[] decryptedData = RSADecrypt(encryptedData, rsaProvider.ExportParameters(true), false);
                    Console.WriteLine("Byte encrypted message message:\n{0}\n",
                        string.Join(" ", decryptedData.Select(t => t.ToString())));
                    
                    // Display the text
                    Console.WriteLine("Decrypted text:\n{0}\n", encoding.GetString(decryptedData));
                }
            }catch(ArgumentNullException)
            {
                Console.WriteLine("Encryption demo failed.");
            }
        }

        /// <summary>
        /// AES Encryption/Decryption deom
        /// </summary>
        public static void AESByteEncryptionDecryptionDemo()
        {
            Console.WriteLine("AES Encryption/Decryption demo\n");

            string message = "This was an encrypted this message that has been decrypted.";

            using (Aes aes = Aes.Create())
            {
                // encrypt the string
                byte[] encrypted = AES_EncryptString(message, aes.Key, aes.IV);
                Console.WriteLine("Encrypted string in byte array:\n{0}\n",
                    string.Join(" ", encrypted.Select(t => t.ToString())));

                // decrypt the string
                string decryptedMessage = AES_DecryptString(encrypted, aes.Key, aes.IV);
                Console.WriteLine("Decrypted message:\n{0}\n", decryptedMessage);

            }
        }

        /// <summary>
        /// Encrypt/Decrypt file using AES symmetric algorithm
        /// </summary>
        public static void EncryptDecryptFile()
        {
            try
            {
                Console.WriteLine("File Encryption demo\n");

                string fileSource = @"..\..\OutputFiles\EncryptionDecryption\sourceFile.txt";
                string fileEncryptedDestination = @"..\..\OutputFiles\EncryptionDecryption\destinationFile.enc";
                string outputFile = @"..\..\OutputFiles\EncryptionDecryption\destinationFileDecrypted.txt";

                using (Aes aes = Aes.Create())
                {
                    // encrypt the file
                    Console.WriteLine("Encrypting the file.");
                    EncryptFile(aes, fileSource, fileEncryptedDestination);

                    // decrypt the file
                    Console.WriteLine("Decrypting the file.");
                    DecryptFile(aes, fileEncryptedDestination, outputFile);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error:\n{0}\n", ex);
            }
        }

        /// <summary>
        /// Encrypt the file
        /// </summary>
        /// <param name="aes"></param>
        /// <param name="fileSource"></param>
        /// <param name="fileDestination"></param>
        private static void EncryptFile(Aes aes, string fileSource, string fileDestination)
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Initialize the file stream that will have encrypted contents written to it
            using (FileStream targetFileStream = new FileStream(fileDestination, FileMode.Create))
            {
                // crypto stream that will contain the cipher text
                using (CryptoStream encryptedStream = new CryptoStream(targetFileStream, encryptor, CryptoStreamMode.Write))
                {
                    // read from the file and write the encrypted contents to the destination file
                    using (FileStream sourceFileStream = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
                    {
                        // indexes for incrypting a chunk at a time
                        int count = 0;
                        int offset = 0;

                        int blockSizeBytes = aes.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];
                        int bytesRead = 0;

                        do
                        {
                            // writes bytes to the byte array and returns the total number of bytes read
                            count = sourceFileStream.Read(data, 0, blockSizeBytes);

                            // set the offset to the count 
                            offset += count;

                            encryptedStream.Write(data, 0, count);

                            bytesRead += blockSizeBytes;
                        }
                        while (count > 0);
                    }
                    encryptedStream.FlushFinalBlock();
                }
            }
            Console.WriteLine("File encrypted.");
        }

        /// <summary>
        /// Decrypt the file
        /// </summary>
        private static void DecryptFile(Aes aes, string encryptedFile, string outputFile)
        {
            using (FileStream encryptedFileStream = new FileStream(encryptedFile, FileMode.Open))
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (FileStream decryptedFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    int cnt = 0;
                    int offset = 0;

                    int blockSizeBytes = aes.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];

                    using (CryptoStream decryptedStream = new CryptoStream(decryptedFileStream, decryptor, CryptoStreamMode.Write))
                    {
                        do
                        {
                            cnt = encryptedFileStream.Read(data, 0, blockSizeBytes);
                            offset += cnt;
                            decryptedStream.Write(data, 0, cnt);
                        }
                        while (cnt > 0);

                        decryptedStream.FlushFinalBlock();
                    }
                }
            }

            Console.WriteLine("File decrypted");
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
                // set the Key and Initialization vector (IV)
                aesObj.Key = key;
                aesObj.IV = iv;

                // Initialize the crypto transform. defines the symmetric encryptor object.
                ICryptoTransform encryptor = aesObj.CreateEncryptor(aesObj.Key, aesObj.IV);

                // Initialize a new Memory Stream used for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    // Initialize the crypto stream passing the Stream we're using, the crypto transform object, and the stream mode
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Initialize the stream writer we will use to write to the stream
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            // write data to the encrypted stream
                            writer.Write(text);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            // return the encrypted bytes.
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

                ICryptoTransform decryptor = aesObj.CreateDecryptor(aesObj.Key, aesObj.IV);

                // Initialize MemoryStream based on the passed in byte array
                using (MemoryStream ms = new MemoryStream(encryptedText))
                {
                    // Initialize the crypto stream passing the target Stream, the transformation algorithm to use, and stream mode
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Initalize the stream reader to read from the Crypto Stream
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            // Read the stream
                            text = reader.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Encrypt data using RSA
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rsaKeyInfo"></param>
        /// <param name="doOAEPadding"></param>
        /// <returns></returns>
        private static byte[] RSAEncrypt(byte[] data, RSAParameters rsaKeyInfo, bool doOAEPadding)
        {
            try
            {
                byte[] encryptedMessage = null;

                using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
                {
                    // set the keys from the rsa parameters passed in
                    rsaProvider.ImportParameters(rsaKeyInfo);

                    encryptedMessage = rsaProvider.Encrypt(data, doOAEPadding);
                }

                return encryptedMessage;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Decrypt data using RSA
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="rsaKeyInfo"></param>
        /// <param name="doOAEPadding"></param>
        /// <returns></returns>
        private static byte[] RSADecrypt(byte[] encryptedData, RSAParameters rsaKeyInfo, bool doOAEPadding)
        {
            try
            {
                byte[] decryptedData = null;

                using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
                {
                    // set the keys from the RSA params passed in
                    rsaProvider.ImportParameters(rsaKeyInfo);

                    decryptedData = rsaProvider.Decrypt(encryptedData, doOAEPadding);
                }

                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e);
                return null;
            }
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
