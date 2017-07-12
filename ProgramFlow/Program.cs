using CSharpProgramming.DataAccessFileIO;
using CSharpProgramming.ProgramFlow;
using CSharpProgramming.SecurityDebugging;
using CSharpProgramming.TypesClasses;
using System;
using System.Reflection;
using Serilog;

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
                        ParallelDemo.RunParallelForBasic();
                        break;
                    case 2:
                        ParallelDemo.ParallelForAdditionRun();
                        break;
                    case 3:
                        ParallelDemo.ParallelForEach();
                        break;
                    case 4:
                        ParallelDemo.ParallelInvokeRun();
                        break;
                    case 5:
                        TaskDemo.Run();
                        break;
                    case 6:
                        TaskDemo.RunTasks();
                        break;
                    case 7:
                        TaskDemo.RunTasksWithContinution();
                        break;
                    case 8:
                        TaskDemo.CancelAfterTenSecondsDemo();
                        break;
                    case 9:
                        ThreadingDemo.BasicRun();
                        break;
                    case 10:
                        ThreadingDemo.RunBackagroundWorker();
                        break;
                    case 11:
                        ThreadingDemo.ThreadPoolDemoRun();
                        break;
                    case 12:
                        ThreadingDemo.BackgroundWorkerCancellationDemo();
                        break;
                    case 13:
                        PLinqDemo.LinqBasicDemo();
                        break;
                    case 14:
                        PLinqDemo.PLinqBasicDemo();
                        break;
                    case 15:
                        PLinqDemo.PLinqForAllDemo();
                        break;
                    case 16:
                        ConcurrentCollectionsDemo.BlockingCollectionAddDemo();
                        break;
                    case 17:
                        ConcurrentCollectionsDemo.ConcurrentDictionaryDemo();
                        break;
                    case 18:
                        ConcurrentCollectionsDemo.ConcurrentBagDemo();
                        break;
                    case 19:
                        AsyncAwaitDemo.BasicDemo().Wait();
                        break;
                    case 20:
                        EventsDelegatesDemos.EventPublisherDemo();
                        break;
                    case 21:
                        DelegateAnonymousMethodDemo.DelegateImplementationDemo();
                        break;
                    case 22:
                        TypesImplementationDemos.BasicStructDemo();
                        break;
                    case 23:
                        TypesImplementationDemos.InheritanceDemo();
                        break;
                    case 24:
                        TypesImplementationDemos.BoxingUnboxingDemo();
                        break;
                    case 25:
                        TypesImplementationDemos.IComparableDemo();
                        break;
                    case 26:
                        TypesImplementationDemos.IEnumerableDemo();
                        break;
                    case 27:
                        DynamicsReflection.AttributesAndReflectionDemo();
                        break;
                    case 28:
                        DynamicsReflection.CodeDOMDemo();
                        break;
                    case 29:
                        StringManipulation.StringBuilderDemo();
                        break;
                    case 30:
                        StringManipulation.StringReaderStringWriterDemo();
                        break;
                    case 31:
                        JsonDemo.JsonSerializationDemo();
                        break;
                    case 32:
                        DataIntegrityDemo.HashingDemo();
                        break;
                    case 33:
                        RegularExpressionsDemo.CharacterClassSyntaxDemo();
                        break;
                    case 34:
                        RegularExpressionsDemo.AnchorSyntaxDemo();
                        break;
                    case 35:
                        RegularExpressionsDemo.GroupingConstructsDemo();
                        break;
                    case 36:
                        RegularExpressionsDemo.QuantifierSyntaxDemo();
                        break;
                    case 37:
                        RegularExpressionsDemo.BackReferenceConstructsDemo();
                        break;
                    case 38:
                        RegularExpressionsDemo.AlternationConstructDemo();
                        break;
                    case 39:
                        RegularExpressionsDemo.SubstitutionsSyntaxDemo();
                        break;
                    case 40:
                        Log.Information("Executing string demo.");
                        StringManipulation.StringOperationsDemo();
                        break;
                    case 41:
                        EncryptionDecryption.GeneratingKeysDemo();
                        break;
                    case 42:
                        EncryptionDecryption.StoringAsymmetricKeysDemo();
                        break;
                    case 43:
                        EncryptionDecryption.SymmetricEncryptionDecryptionDemo();
                        break;
                    case 44:
                        EncryptionDecryption.AsymmetricEncryptionDecryptionRSADemo();
                        break;
                    case 45:
                        EncryptionDecryption.AESByteEncryptionDecryptionDemo();
                        break;
                    case 46:
                        DataIntegrityDemo.DigitalSignatureDemo();
                        break;
                    case 47:
                        EncryptionDecryption.EncryptDecryptFile();
                        break;
                    case 48:
                        FileIO.ReadAndWriteToFileStream();
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

            Console.ReadLine();
        }

        public static void PrintPrompt()
        {
            Console.WriteLine("Which demo do you want to run?\n");
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
            Console.WriteLine();
        }
    }
}
