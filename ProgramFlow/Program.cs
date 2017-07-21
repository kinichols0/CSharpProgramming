using CSharpProgramming.DataAccessFileIO;
using CSharpProgramming.ProgramFlow;
using CSharpProgramming.SecurityDebugging;
using CSharpProgramming.TypesClasses;
using Serilog;
using System;

namespace CSharpProgramming
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // configure the logger
            ILogger logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            // configure the global logger
            Log.Logger = logger;

            // log the start of the program
            Log.Information("C Sharp demo application started.");

            // prompt user the available processes to run
            PrintPrompt();

            // read the input and run the corresponding process
            string key = Console.ReadLine();

            if (int.TryParse(key, out int demoNum))
            {
                switch (demoNum)
                {
                    case 1:
                        Log.Information("ParallelFor Basic Demo.");
                        ParallelDemo.RunParallelForBasic();
                        break;
                    case 2:
                        Log.Information("ParallelFor Addition Demo");
                        ParallelDemo.ParallelForAdditionRun();
                        break;
                    case 3:
                        Log.Information("Parallel.Foreach Demo");
                        ParallelDemo.ParallelForEach();
                        break;
                    case 4:
                        Log.Information("Parallel.Invoke Demo");
                        ParallelDemo.ParallelInvokeRun();
                        break;
                    case 5:
                        Log.Information("Task Demo");
                        TaskDemo.Run();
                        break;
                    case 6:
                        Log.Information("Multiple Task Demo");
                        TaskDemo.RunTasks();
                        break;
                    case 7:
                        Log.Information("Tasks with Continutation Demo");
                        TaskDemo.RunTasksWithContinution();
                        break;
                    case 8:
                        Log.Information("Cancelling Tasks Demo");
                        TaskDemo.CancelAfterTenSecondsDemo();
                        break;
                    case 9:
                        Log.Information("Thread Basic Demo");
                        ThreadingDemo.BasicRun();
                        break;
                    case 10:
                        Log.Information("BackgroundWorker Demo");
                        ThreadingDemo.RunBackagroundWorker();
                        break;
                    case 11:
                        Log.Information("Threadpool Demo");
                        ThreadingDemo.ThreadPoolDemoRun();
                        break;
                    case 12:
                        Log.Information("BackgroundWorker Cancellation Demo");
                        ThreadingDemo.BackgroundWorkerCancellationDemo();
                        break;
                    case 13:
                        Log.Information("Linq Basic Demo");
                        PLinqDemo.LinqBasicDemo();
                        break;
                    case 14:
                        Log.Information("PLinq Basic Demo");
                        PLinqDemo.PLinqBasicDemo();
                        break;
                    case 15:
                        Log.Information("PLinq ForAll Demo");
                        PLinqDemo.PLinqForAllDemo();
                        break;
                    case 16:
                        Log.Information("Adding to BlockingCollection Demo");
                        ConcurrentCollectionsDemo.BlockingCollectionAddDemo();
                        break;
                    case 17:
                        Log.Information("Concurrent Dictionary Demo");
                        ConcurrentCollectionsDemo.ConcurrentDictionaryDemo();
                        break;
                    case 18:
                        Log.Information("ConcurrentBag Demo");
                        ConcurrentCollectionsDemo.ConcurrentBagDemo();
                        break;
                    case 19:
                        Log.Information("Async Await Basic Demo");
                        AsyncAwaitDemo.BasicDemo().Wait();
                        break;
                    case 20:
                        Log.Information("Event Publisher/Subscriber Demo");
                        NetworkEventPublisherDemo.Run();
                        break;
                    case 21:
                        Log.Information("Delegate Anonymous Method Demo");
                        DelegateAnonymousMethodDemo.DelegateImplementationDemo();
                        break;
                    case 22:
                        Log.Information("Struct Basic Demo");
                        TypesImplementation.BasicStructDemo();
                        break;
                    case 23:
                        Log.Information("Inheritance Demo");
                        TypesImplementation.InheritanceDemo();
                        break;
                    case 24:
                        Log.Information("Boxing/Unboxing Demo");
                        TypesImplementation.BoxingUnboxingDemo();
                        break;
                    case 25:
                        Log.Information("IComparable Implementation Demo");
                        TypesImplementation.IComparableDemo();
                        break;
                    case 26:
                        Log.Information("IEnumerable Implementation Demo");
                        TypesImplementation.IEnumerableDemo();
                        break;
                    case 27:
                        Log.Information("Attributes and Reflection Demo");
                        DynamicsReflection.AttributesAndReflectionDemo();
                        break;
                    case 28:
                        Log.Information("CodeDOM Demo");
                        DynamicsReflection.CodeDOMDemo();
                        break;
                    case 29:
                        Log.Information("StringBuilder Demo");
                        StringManipulation.StringBuilderDemo();
                        break;
                    case 30:
                        Log.Information("StringReader/StringWriter Demo");
                        StringManipulation.StringReaderStringWriterDemo();
                        break;
                    case 31:
                        Log.Information("Json Serialization with JSON.Net Demo");
                        JsonDemo.JsonSerializationDemo();
                        break;
                    case 32:
                        Log.Information("Hashing for Data Integrity Demo");
                        DataIntegrityDemo.HashingDemo();
                        break;
                    case 33:
                        Log.Information("Regex - Character Class Syntax Demo");
                        RegularExpressionsDemo.CharacterClassSyntaxDemo();
                        break;
                    case 34:
                        Log.Information("Regex - Anchor Syntax Demo");
                        RegularExpressionsDemo.AnchorSyntaxDemo();
                        break;
                    case 35:
                        Log.Information("Regex - Grouping Construct Demo");
                        RegularExpressionsDemo.GroupingConstructsDemo();
                        break;
                    case 36:
                        Log.Information("Regex - Quantifier Syntax Demo");
                        RegularExpressionsDemo.QuantifierSyntaxDemo();
                        break;
                    case 37:
                        Log.Information("Regex - Back Reference Construct Demo");
                        RegularExpressionsDemo.BackReferenceConstructsDemo();
                        break;
                    case 38:
                        Log.Information("Regex - Alternation Construct Demo");
                        RegularExpressionsDemo.AlternationConstructDemo();
                        break;
                    case 39:
                        Log.Information("Regex - Substitutions Demo");
                        RegularExpressionsDemo.SubstitutionsSyntaxDemo();
                        break;
                    case 40:
                        Log.Information("Executing string demo.");
                        StringManipulation.StringOperationsDemo();
                        break;
                    case 41:
                        Log.Information("Generating Keys Demo");
                        EncryptionDecryption.GeneratingKeysDemo();
                        break;
                    case 42:
                        Log.Information("Storing Asymmetric Keys Demo");
                        EncryptionDecryption.StoringAsymmetricKeysDemo();
                        break;
                    case 43:
                        Log.Information("Symmetric Encryption/Decryption Demo");
                        EncryptionDecryption.SymmetricEncryptionDecryptionDemo();
                        break;
                    case 44:
                        Log.Information("RSA Asymmetric Encryption/Decryption Demo");
                        EncryptionDecryption.AsymmetricEncryptionDecryptionRSADemo();
                        break;
                    case 45:
                        Log.Information("AES Encryption/Decryption Demo");
                        EncryptionDecryption.AESByteEncryptionDecryptionDemo();
                        break;
                    case 46:
                        Log.Information("Digital Signature Demo Using Public Key Algorithms");
                        DataIntegrityDemo.DigitalSignatureDemo();
                        break;
                    case 47:
                        Log.Information("Encrypt/Decrypt File Using AES Symmetric Algorithm");
                        EncryptionDecryption.EncryptDecryptFile();
                        break;
                    case 48:
                        Log.Information("Read/Write to FileStream Demo");
                        FileIO.ReadAndWriteToFileStream();
                        break;
                    case 49:
                        Log.Information("Read/Write Binary Data to File Demo");
                        FileIO.BinaryReadBinaryWriteFileStream();
                        break;
                    case 50:
                        Log.Information("Read/Write to NetworkStream Demo");
                        FileIO.ReadWriteToNetworkStream();
                        break;
                    case 51:
                        Log.Information("Serialize Object to XML Demo");
                        Serialization.SerializeObjectToXmlFileDemo();
                        break;
                    case 52:
                        Log.Information("Deserialize Object from XML Demo");
                        Serialization.DeserializeObjectFromXmlFileDemo();
                        break;
                    case 53:
                        Log.Information("Serialize Object to Binary Demo");
                        Serialization.SerializeObjectToBinaryFileDemo();
                        break;
                    case 54:
                        Log.Information("Deserialize Object from Binary Demo");
                        Serialization.DeserializeObjectFromBinaryFileDemo();
                        break;
                    case 55:
                        Log.Information("Serialize an Object to Json Demo");
                        Serialization.SerializeObjectToJsonDemo();
                        break;
                    case 56:
                        Log.Information("Deserialize Object From Json Demo");
                        Serialization.DeserializeObjectFromJsonDemo();
                        break;
                    case 57:
                        Log.Information("Serialize/Deserialize Object to JSON using MemoryStream Demo.");
                        Serialization.MemoryStreamJsonSerializationDemo();
                        break;
                    case 58:
                        Log.Information("Serialize/Deserialize Object to Xml String Demo.");
                        Serialization.ObjectToXmlStringSerializationDemo();
                        break;
                    default:
                        Console.WriteLine("Could not find a process corresponding to {0} to run. Program will exit now.", demoNum);
                        break;
                }
            }
            else
                Log.Warning("Not valid input. Program will exit now.");

            // log end of the program
            Log.Information("C Sharp Programming demo ended.\n");

            Console.WriteLine("\nPress any key to close the program...");
            Console.ReadLine();
        }

        public static void PrintPrompt()
        {
            Console.WriteLine("\nWhich demo do you want to run?\n");
            Console.WriteLine("[1]  - ParallelFor Basic Run");
            Console.WriteLine("[2]  - ParallelFor Concurrent Addition Run");
            Console.WriteLine("[3]  - ParallelForEach demo");
            Console.WriteLine("[4]  - ParallelInvoke demo");
            Console.WriteLine("[5]  - Task demo");
            Console.WriteLine("[6]  - Multiple Task demo");
            Console.WriteLine("[7]  - Continuation Task demo");
            Console.WriteLine("[8]  - Cancel task after 10 seconds demo.");
            Console.WriteLine("[9]  - Thread demo");
            Console.WriteLine("[10] - Background Worker demo");
            Console.WriteLine("[11] - Threadpool demo");
            Console.WriteLine("[12] - Cancel background worker demo.");
            Console.WriteLine("[13] - Linq basic demo ");
            Console.WriteLine("[14] - PLinq basic demo");
            Console.WriteLine("[15] - PLinq ForAll demo");
            Console.WriteLine("[16] - Concurrent Blocking Collection demo");
            Console.WriteLine("[17] - Cocnurrent Dictionary Collection demo");
            Console.WriteLine("[18] - Concurrent Bag Demo ");
            Console.WriteLine("[19] - Async/Await Basic demo");
            Console.WriteLine("[20] - Event demo");
            Console.WriteLine("[21] - Delegate Implementation demo");
            Console.WriteLine("[22] - Basic struct demo");
            Console.WriteLine("[23] - Inheritance basics demo");
            Console.WriteLine("[24] - Boxing and Unboxing demo");
            Console.WriteLine("[25] - IComparable Implementation demo");
            Console.WriteLine("[26] - IEnumerable Implementation demo");
            Console.WriteLine("[27] - Attributes and Reflection demo");
            Console.WriteLine("[28] - CodeDOM demo");
            Console.WriteLine("[29] - Stringbuilder demo");
            Console.WriteLine("[30] - StringReader/StringWriter demo");
            Console.WriteLine("[31] - Serialization JSON Data and Validation demo");
            Console.WriteLine("[32] - Data Integrity with Hashing Demo");
            Console.WriteLine("[33] - Regex Character Class Syntax demo");
            Console.WriteLine("[34] - Regex Anchor Syntax demo");
            Console.WriteLine("[35] - Regex Grouping Syntax demo");
            Console.WriteLine("[36] - Regex Quantifier Syntax demo");
            Console.WriteLine("[37] - Regex Backreference Constructs demo");
            Console.WriteLine("[38] - Regex Alternation Consturcts demo");
            Console.WriteLine("[39] - Regex Substitutions demo");
            Console.WriteLine("[40] - String operations demo");
            Console.WriteLine("[41] - Generating Symmetric/Assymetric keys demo");
            Console.WriteLine("[42] - Storing asymmetric generated keys in key container demo");
            Console.WriteLine("[43] - Symmetric Encryption/Decryption demo");
            Console.WriteLine("[44] - RSA Asymmetric Encryption/Decryption demo");
            Console.WriteLine("[45] - AES Encryption/Decryption demo");
            Console.WriteLine("[46] - Digital Signature demo");
            Console.WriteLine("[47] - Encrypt file demo.");
            Console.WriteLine("[48] - Copy File Using FileStream demo");
            Console.WriteLine("[49] - Copy File using FileStream, BinaryRead, and BinaryWrite demo");
            Console.WriteLine("[50] - Read/Write to NetworkStream demo");
            Console.WriteLine("[51] - Serialize to XML File demo");
            Console.WriteLine("[52] - Deserialize an Object from XML File demo");
            Console.WriteLine("[53] - Serialize to Text File in Binary Format demo");
            Console.WriteLine("[54] - Deserialize an Object from Text File in Binary Format demo");
            Console.WriteLine("[55] - Serialize to Text File in Json Format demo");
            Console.WriteLine("[56] - Deserialize an Object from Text File in Json Format demo");
            Console.WriteLine("[57] - Serialize/Deserialize Object to JSON using MemoryStream Demo.");
            Console.WriteLine("[58] - Serialize/Deserialize Object to Xml String Demo.");
            Console.WriteLine();
        }
    }
}
