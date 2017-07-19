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
    public class SecurityUtility
    {
        /// <summary>
        /// ComputeHash implementation using SHA246 hashing algorithm
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] ComputSHA256eHash(string message)
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
        public static byte[] AES_EncryptBytes(byte[] content, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider() { Key = key, IV = iv, Padding = PaddingMode.PKCS7 })
            {
                byte[] encryptedFileBytes = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    // initialize the AES encryptor
                    ICryptoTransform encryptor = aes.CreateEncryptor();

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
        }

        /// <summary>
        /// Decrypt an array of bytes using a symmetric algorithm
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encryptedContents"></param>
        /// <returns></returns>
        public static byte[] AES_DecryptBytes(byte[] encryptedContents, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider() { Key = key, IV = iv, Padding = PaddingMode.PKCS7 })
            {
                byte[] decryptedBytes = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Initialize the decryptor
                    ICryptoTransform decryptor = aes.CreateDecryptor();

                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        // write decrypted bytes to the memory stream
                        cryptoStream.Write(encryptedContents, 0, encryptedContents.Length);
                    }
                    decryptedBytes = memoryStream.ToArray();
                }
                return decryptedBytes;
            }
        }

        /// <summary>
        /// Encrypt a string using AES algorithm
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] AES_EncryptString(string message, Aes aes)
        {
            byte[] encryptedBytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform transformer = aes.CreateEncryptor();
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
        /// Decrypt a string using the AES algorithm
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AES_DecryptString(byte[] encryptedStringBytes, Aes aes)
        {
            string plainText = null;
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (CryptoStream cs = new CryptoStream(stream, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(encryptedStringBytes, 0, encryptedStringBytes.Length);
                }
                plainText = Encoding.UTF8.GetString(stream.ToArray());
            }
            return plainText;
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
        public static byte[] RSA_DecryptBytes(RSAParameters rsaParams, byte[] encryptedContents)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                return rsa.Decrypt(encryptedContents, true);
            }
        }
    }
}
