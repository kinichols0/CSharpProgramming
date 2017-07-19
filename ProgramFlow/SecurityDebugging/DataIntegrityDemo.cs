using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using CSharpProgramming.Common.Utilities;

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
            var originalHash = SecurityUtility.ComputSHA256Hash(message);
            Console.WriteLine("Message hash value:\n{0}\n",
                string.Join(" ", originalHash.ToList().Select(b => b.ToString())));

            // Display new message
            var newMessage = "This is a new message!";
            Console.WriteLine("New message \n{0}\n", newMessage);

            // Display new message hash value
            var newHash = SecurityUtility.ComputSHA256Hash(newMessage);
            Console.WriteLine("New message hash value:\n{0}\n",
                string.Join(" ", newHash.ToList().Select(b => b.ToString())));

            // Compare hashes
            Console.WriteLine("New hash equals original hash: {0}", newHash.Equals(originalHash));
            Console.WriteLine("Original equals original hash: {0}", originalHash.Equals(originalHash));
        }

        /// <summary>
        /// Cryptographic digital signatures use public key algorithms to provide data integrity.
        /// </summary>
        public static void DigitalSignatureDemo()
        {
            Console.WriteLine("Digital Signature demo...\n");

            // initialize the message
            var msg = "This message should not change";
            Console.WriteLine("Message:\n{0}\n", msg);

            // hash message
            byte[] hashedMessage = SecurityUtility.ComputSHA256Hash(msg);
            string hashName = "SHA256";
            Console.WriteLine("Encoded message:\n{0}\n", string.Join(" ", hashedMessage.Select(t => t.ToString())));

            // Get public/private key
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();

            // Create a RSAPKCS1SignatureFormatter and pass it the RSA provider to give it the private key.
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsaProvider);

            // specify the hash algorithm to use. Here we use SHA1.
            rsaFormatter.SetHashAlgorithm(hashName);

            // Create a signature for the msg and set it to a byte array
            byte[] signedHashValue = rsaFormatter.CreateSignature(hashedMessage);
            Console.WriteLine("Signed value:\n{0}\n", string.Join(" ", signedHashValue.Select(t => t.ToString())));

            // Verify the value
            RSACryptoServiceProvider rsaVerifier = new RSACryptoServiceProvider();

            // get public key info from previous RSA
            RSAParameters rsaParams = rsaProvider.ExportParameters(false);
            rsaVerifier.ImportParameters(rsaParams);

            // initialize the rsa deformatter to verify the signature on the hashed message byte array
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsaProvider);
            rsaDeformatter.SetHashAlgorithm(hashName);

            // verifies if the hash has been altered verifies that the data came from the signee.
            bool verified = rsaDeformatter.VerifySignature(hashedMessage, signedHashValue);
            Console.WriteLine("Validated Signature: {0}", verified);
        }
    }
}
