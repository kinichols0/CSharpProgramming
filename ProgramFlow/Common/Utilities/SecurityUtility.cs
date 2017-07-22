using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace CSharpProgramming.Common.Utilities
{
    public static class SecurityUtility
    {
        /// <summary>
        /// ComputeHash implementation using SHA246 hashing algorithm
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] ComputSHA256Hash(string message)
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] unicodeBytes = unicodeEncoding.GetBytes(message);

            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(unicodeBytes);
        }

        /// <summary>
        /// Encrypt an array of bytes using a AES (Advanced Encryption Standard)
        /// symmetric encryption alogorithm
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="fileSource"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] SymmetricKeyEncryptBytes(byte[] content, SymmetricAlgorithm sysAlg)
        {
            byte[] encryptedFileBytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                // initialize the AES encryptor
                ICryptoTransform encryptor = sysAlg.CreateEncryptor();

                using (CryptoStream encryptedStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    // encrypt and write the bytes to the memory stream
                    encryptedStream.Write(content, 0, content.Length);
                }
                encryptedFileBytes = stream.ToArray();
            }
            // return the encrypted bytes written to the memory stream
            return encryptedFileBytes;
        }

        /// <summary>
        /// Decrypt an array of bytes using a symmetric algorithm
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encryptedContents"></param>
        /// <returns></returns>
        public static byte[] AES_DecryptBytes(byte[] encryptedContents, SymmetricAlgorithm sysAlg)
        {
            byte[] decryptedBytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Initialize the decryptor
                ICryptoTransform decryptor = sysAlg.CreateDecryptor();

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                {
                    // write decrypted bytes to the memory stream
                    cryptoStream.Write(encryptedContents, 0, encryptedContents.Length);
                }
                decryptedBytes = memoryStream.ToArray();
            }
            return decryptedBytes;
        }

        /// <summary>
        /// Encrypt a string using a symmetric algorithm
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] SymmetricKeyEncryptString(string message, SymmetricAlgorithm symmetricAlgorithm)
        {
            byte[] encryptedBytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform transformer = symmetricAlgorithm.CreateEncryptor();
                using(CryptoStream cs = new CryptoStream(stream, transformer, CryptoStreamMode.Write))
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    cs.Write(messageBytes, 0, messageBytes.Length);
                }
                encryptedBytes = stream.ToArray();
            }
            return encryptedBytes;
        }

        /// <summary>
        /// Decrypt a string using a symmetric algorithm
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string SymmetricKeyDecryptString(byte[] encryptedStringBytes, SymmetricAlgorithm symmetricAlgorithm)
        {
            string plainText = null;
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor();
                using (CryptoStream cs = new CryptoStream(stream, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(encryptedStringBytes, 0, encryptedStringBytes.Length);
                }
                plainText = Encoding.UTF8.GetString(stream.ToArray());
            }
            return plainText;
        }

        /// <summary>
        /// Encrypt a file using Symmetric Algorithm and write the encrypted contents to
        /// a newly created output file
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        /// <param name="symmetricAlgorithm"></param>
        public static void CreateSymmetricKeyEncryptedFile(string sourceFilePath, string destinationFilePath, SymmetricAlgorithm symmetricAlgorithm)
        {
            using(FileStream stream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor();
                using(CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using(FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                    {
                        int bufferLength = symmetricAlgorithm.BlockSize / 8, bytesRead = 0;
                        byte[] buffer = new byte[bufferLength];

                        do
                        {
                            bytesRead = sourceStream.Read(buffer, 0, bufferLength);
                            cryptoStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt a file using a symmetric algorithm and write the decrypted file
        /// contents to an output file
        /// </summary>
        /// <param name="encryptedSourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        /// <param name="SymmetricAlgorithm"></param>
        public static void DecryptedSymmetricKeyEncryptedFile(string encryptedSourceFilePath, string destinationFilePath, SymmetricAlgorithm SymmetricAlgorithm)
        {
            using (FileStream stream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                ICryptoTransform cryptoTransform = SymmetricAlgorithm.CreateDecryptor();
                using (CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using (FileStream sourceStream = new FileStream(encryptedSourceFilePath, FileMode.Open, FileAccess.Read))
                    {
                        int bufferLength = SymmetricAlgorithm.BlockSize / 8, bytesRead = 0;
                        byte[] buffer = new byte[bufferLength];

                        do
                        {
                            bytesRead = sourceStream.Read(buffer, 0, bufferLength);
                            cryptoStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
        }

        /// <summary>
        /// RSA Asymmetric encryption. Import the public key from the public key
        /// xml and encrypt the contents
        /// </summary>
        /// <param name="publicKeyXml"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static byte[] RSA_EncryptBytes(byte[] contents, string publicKeyXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKeyXml);
                return rsa.Encrypt(contents, true);
            }
        }

        /// <summary>
        /// RSA Asymmetric encryption. Import the RSA Parameters and decrypt the
        /// encrypted contents
        /// </summary>
        /// <param name="rsaParams"></param>
        /// <param name="encryptedContents"></param>
        /// <returns></returns>
        public static byte[] RSA_DecryptBytes(byte[] encryptedContents, string publicPrivateKeyXmlString)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicPrivateKeyXmlString);
                return rsa.Decrypt(encryptedContents, true);
            }
        }
    }
}
