using CSharpProgramming.ProgramFlow;
using CSharpProgramming.SecurityDebugging;
using CSharpProgramming.TypesClasses;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSharpProgramming
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Program started...{0}\n", DateTime.Now.ToString());

            // prompt user the available processes to run
            PrintPrompt();
            
            // read the input and run the corresponding process
            string key = Console.ReadLine();
            if (int.TryParse(key, out int demoNum))
            {
                switch (demoNum)
                {
                    case 1: ParallelDemo.RunParallelForBasic();
                        break;
                    case 2: ParallelDemo.ParallelForAdditionRun();
                        break;
                    case 3: ParallelDemo.ParallelForEach();
                        break;
                    case 4: ParallelDemo.ParallelInvokeRun();
                        break;
                    case 5: TaskDemo.Run();
                        break;
                    case 6: TaskDemo.RunTasks();
                        break;
                    case 7: TaskDemo.RunTasksWithContinution();
                        break;
                    case 8: TaskDemo.CancelAfterTenSecondsDemo();
                        break;
                    case 9: ThreadingDemo.BasicRun();
                        break;
                    case 10: ThreadingDemo.RunBackagroundWorker();
                        break;
                    case 11: ThreadingDemo.ThreadPoolDemoRun();
                        break;
                    case 12: ThreadingDemo.BackgroundWorkerCancellationDemo();
                        break;
                    case 13: PLinqDemo.LinqBasicDemo();
                        break;
                    case 14: PLinqDemo.PLinqBasicDemo();
                        break;
                    case 15: PLinqDemo.PLinqForAllDemo();
                        break;
                    case 16: ConcurrentCollectionsDemo.BlockingCollectionAddDemo();
                        break;
                    case 17: ConcurrentCollectionsDemo.ConcurrentDictionaryDemo();
                        break;
                    case 18: ConcurrentCollectionsDemo.ConcurrentBagDemo();
                        break;
                    case 19: AsyncAwaitDemo.BasicDemo().Wait();
                        break;
                    case 20: EventPublisherDemo();
                        break;
                    case 21: DelegateAnonymousMethodDemo.DelegateImplementationDemo();
                        break;
                    case 22: BasicStructDemo();
                        break;
                    case 23: InheritanceDemo();
                        break;
                    case 24: BoxingUnboxingDemo();
                        break;
                    case 25: IComparableDemo();
                        break;
                    case 26: IEnumerableDemo();
                        break;
                    case 27: AttributesAndReflectionDemo();
                        break;
                    case 28: CodeDOMDemo();
                        break;
                    case 29: StringBuilderDemo();
                        break;
                    case 30: StringReaderStringWriterDemo();
                        break;
                    case 31: JsonDemo.JsonSerializationDemo();
                        break;
                    case 32: DataIntegrityDemo.HashingDemo();
                        break;
                    case 33: RegularExpressionsDemo.CharacterClassSyntaxDemo();
                        break;
                    case 34: RegularExpressionsDemo.AnchorSyntaxDemo();
                        break;
                    case 35: RegularExpressionsDemo.GroupingConstructsDemo();
                        break;
                    case 36: RegularExpressionsDemo.QuantifierSyntaxDemo();
                        break;
                    case 37: RegularExpressionsDemo.BackReferenceConstructsDemo();
                        break;
                    case 38: RegularExpressionsDemo.AlternationConstructDemo();
                        break;
                    case 39: RegularExpressionsDemo.SubstitutionsSyntaxDemo();
                        break;
                    case 40: StringOperationsDemo();
                        break ;
                    case 41: KeyEncryptionDecryption.GeneratingKeysDemo();
                        break;
                    case 42: KeyEncryptionDecryption.StoringAsymmetricKeysDemo();
                        break;
                    case 43: KeyEncryptionDecryption.SymmetricEncryptionDecryptionDemo();
                        break;
                    case 44: KeyEncryptionDecryption.AsymmetricEncryptionDecryptionRSADemo();
                        break;
                    case 45: KeyEncryptionDecryption.AESByteEncryptionDecryptionDemo();
                        break;
                    default: Console.WriteLine("Could not find a process corresponding to {0} to run. Program will exit now.", demoNum);
                        break;
                }
            }
            else
                Console.WriteLine("Not valid input. Program will exit now.");

            Console.WriteLine("Program ended...,{0}", DateTime.Now.ToString());

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
            Console.WriteLine();
        }

        private static void EventPublisherDemo()
        {
            WriteOutputEventPublisher publisher = new WriteOutputEventPublisher();
            WriteOutputEventSubscriber subA = new WriteOutputEventSubscriber("SubA", publisher);
            WriteOutputEventSubscriber subB = new WriteOutputEventSubscriber("SubB", publisher);
            publisher.StartDemo("This is an event demo.");
            subA.UnsubscribeFromEvent();
            publisher.StartDemo("Raising another event.");
        }

        private static void BasicStructDemo()
        {
            Console.WriteLine("Started struct demo...\n");

            Rectangle rect = new Rectangle(5, 10);
            Console.WriteLine("Initialized a struct with dimensions {0}x{1} (LxW).", rect.length, rect.width);

            Rectangle rect2;
            rect2.length = 10;
            rect2.width = 20;
            Console.WriteLine("Initialized a struct with dimensions {0}x{1} (LxW).", rect2.length, rect2.width);

            Console.WriteLine("\nEnded struct demo...");
        }

        private static void InheritanceDemo()
        {
            Console.WriteLine("Started basic inheritance demo...");

            StudentProfileData studentProfile = new StudentProfileData();
            studentProfile.Print();

            StudentProfileData studenProfile2 = new StudentProfileData("Kelvin", "Nichols", "Computer Science", ClassStanding.Senior);
            studenProfile2.Print();

            Console.WriteLine("Ended basic inheritance demo...");
        }

        private static void BoxingUnboxingDemo()
        {
            Console.WriteLine("Started basic boxing and unboxing demo...");

            int i = 55;
            Console.WriteLine("int i = {0}", i);
            Console.WriteLine("boxing i");

            object o = i;// implicit boxing, explicit boxing ex: object o = (object)i
            Console.WriteLine("i is still {0}", o);

            object x = 122;
            Console.WriteLine("object x = {0}", x);
            Console.WriteLine("unboxing x");
            i = (int)x;
            Console.WriteLine("x is still {0}", x);

            Console.WriteLine("Ended basic boxing and unboxing demo...");
        }

        private static void IComparableDemo()
        {
            Console.WriteLine("Started basic IComparable demo...");

            List<ComparableAgeEntity> entities = new List<ComparableAgeEntity>()
                        {
                            new ComparableAgeEntity("Name1", 26),
                            new ComparableAgeEntity("Name2", 30),
                            new ComparableAgeEntity("Name3", 5),
                            new ComparableAgeEntity("Name4", 6),
                            new ComparableAgeEntity("Name5", 50),
                            new ComparableAgeEntity("Name6", 40),
                            new ComparableAgeEntity("Name7", 33),
                            new ComparableAgeEntity("Name8", 12),
                            new ComparableAgeEntity("Name9", 65),
                            new ComparableAgeEntity("Name10", 70)
                        };

            Console.WriteLine("All comparable entities");
            entities.ForEach(t => Console.WriteLine(t));

            Console.WriteLine("\nComparable entities sorted by age from youngest to oldest");
            entities.Sort();
            entities.ForEach(t => Console.WriteLine(t));

            Console.WriteLine("\nComparable entities sorted by age from oldest to youngest");
            entities = entities.OrderByDescending(t => t).ToList();
            entities.ForEach(t => Console.WriteLine(t));

            Console.WriteLine("Ended basic IComparable demo...");
        }

        private static void IEnumerableDemo()
        {
            Console.WriteLine("Started IEnumerable Implementation demo...");

            // One way to initialize the collection
            EnumerableCollection<ComparableAgeEntity> objs = new EnumerableCollection<ComparableAgeEntity>() {
                            new ComparableAgeEntity("Name1", 26),
                            new ComparableAgeEntity("Name2", 30),
                            new ComparableAgeEntity("Name3", 5),
                            new ComparableAgeEntity("Name4", 6),
                            new ComparableAgeEntity("Name5", 50),
                            new ComparableAgeEntity("Name6", 40),
                            new ComparableAgeEntity("Name7", 33),
                            new ComparableAgeEntity("Name8", 12),
                            new ComparableAgeEntity("Name9", 65),
                            new ComparableAgeEntity("Name10", 70)
                        };

            // Another way to initialize the collection
            ComparableAgeEntity[] comparableEntities = new ComparableAgeEntity[]
            {
                            new ComparableAgeEntity("Name1", 26),
                            new ComparableAgeEntity("Name2", 30),
                            new ComparableAgeEntity("Name3", 5),
                            new ComparableAgeEntity("Name4", 6),
                            new ComparableAgeEntity("Name5", 50),
                            new ComparableAgeEntity("Name6", 40),
                            new ComparableAgeEntity("Name7", 33),
                            new ComparableAgeEntity("Name8", 12),
                            new ComparableAgeEntity("Name9", 65),
                            new ComparableAgeEntity("Name10", 70)
            };
            var collection = new EnumerableCollection<ComparableAgeEntity>(comparableEntities);

            // Custom Add implementation
            collection.Add(new ComparableAgeEntity("NameX", 16));

            // Custom foreach implementation
            collection.ForEach(t => Console.WriteLine(t));

            Console.WriteLine("Ended IEnumerable Implementation demo...");
        }

        private static void AttributesAndReflectionDemo()
        {
            Console.WriteLine("Started Attributes and Reflection demo...");

            // Type info
            Console.WriteLine("\nGetting type info of Order class.");
            TypeInfo tInfo = typeof(Order).GetTypeInfo();
            Console.WriteLine("Assembly qualified name of Order: " + tInfo.AssemblyQualifiedName);

            // Member info
            Console.WriteLine("\nGetting member info of Order class and displaying attributes:");
            MemberInfo mInfo = typeof(Order);
            foreach (var attr in mInfo.GetCustomAttributes())
            {
                Console.WriteLine(attr.GetType().Name + " - attribute properties:");
                var attrProps = attr.GetType().GetRuntimeProperties();
                foreach (var prop in attrProps)
                {
                    Console.WriteLine(prop.Name + " : " + attr.GetType().GetProperty(prop.Name).GetValue(attr, null));
                }
            }

            Console.WriteLine("\nMember Properties");
            var pInfo = typeof(Order).GetProperties();
            foreach (var prop in pInfo)
            {
                Console.WriteLine("\n" + prop.Name + " - attributes:");
                var propAttrs = prop.GetCustomAttributes();
                foreach (var propAttr in propAttrs)
                {
                    Console.WriteLine(propAttr.GetType().Name);

                    var attrProps = propAttr.GetType().GetProperties();
                    foreach (var attrProp in attrProps)
                    {
                        Console.WriteLine(attrProp.Name + " : " + propAttr.GetType().GetProperty(attrProp.Name).GetValue(propAttr));
                    }
                }

            }

            // Order object initialization
            Console.WriteLine("\nInitialize Order object:");
            var order = new Order() { Id = 132, CustomerId = 2543, ItemId = 523456, OrderPlacedDate = DateTime.Now };
            Console.WriteLine(order);

            // Mapping Order properties to attributes
            Console.WriteLine("\nMapping Order object to attributes");
            Console.WriteLine("DB Table Name: " + ((order.GetType().GetCustomAttribute(typeof(DBTableAttribute)) is DBTableAttribute tblAttr) ? tblAttr.Name : null));

            foreach (var prop in order.GetType().GetProperties())
            {
                StringBuilder builder = new StringBuilder();
                foreach (var attr in prop.GetCustomAttributes())
                {
                    if (attr is DBColumnAttribute colAttr)
                        builder.AppendLine(colAttr.Name + " : " + prop.GetValue(order));
                }
                Console.WriteLine(builder.ToString());
            }

            // String's assembly info
            Console.WriteLine("\nString's Assembly info:\nFull name - " + typeof(System.String).Assembly);
            Console.WriteLine("Code base - " + typeof(String).Assembly.CodeBase);

            Console.WriteLine("\nEnded Attributes and Reflection demo...");
        }

        /// <summary>
        /// Code Document Object Model (CodeDOM)
        /// Generates an Employee class within Demos namespace
        /// </summary>
        private static void CodeDOMDemo()
        {
            Console.WriteLine("Starting CodeDOM demo...\n");

            // declare a class
            CodeTypeDeclaration customerClass = new CodeTypeDeclaration("Employee");
            customerClass.IsClass = true;
            customerClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;

            // add empty constructor
            CodeConstructor emptyConstructor = new CodeConstructor();
            emptyConstructor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            customerClass.Members.Add(emptyConstructor);

            // add parameter constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "name"));
            // constructor.Statements.Add(new CodeSnippetExpression("nameField = name"));
            CodeVariableReferenceExpression nameFieldExpression = new CodeVariableReferenceExpression("nameField");
            constructor.Statements.Add(new CodeAssignStatement(nameFieldExpression, new CodeVariableReferenceExpression("name")));
            constructor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            customerClass.Members.Add(constructor);

            // declare and id field
            CodeMemberField idField = new CodeMemberField()
            {
                Name = "idField",
                Type = new CodeTypeReference("System.Int32"),
                Attributes = MemberAttributes.Private
            };
            customerClass.Members.Add(idField);

            // declare and add id property
            CodeMemberProperty idProperty = new CodeMemberProperty()
            {
                Name = "EmployeeId",
                Type = new CodeTypeReference("System.Int32"),
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };
            idProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "idField")));
            idProperty.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "idField"), new CodePropertySetValueReferenceExpression()));
            customerClass.Members.Add(idProperty);

            // declare and name field
            CodeMemberField nameField = new CodeMemberField()
            {
                Name = "nameField",
                Type = new CodeTypeReference("System.String"),
                Attributes = MemberAttributes.Private
            };
            customerClass.Members.Add(nameField);

            // declare and add string property
            CodeMemberProperty nameProperty = new CodeMemberProperty()
            {
                Name = "Name",
                Type = new CodeTypeReference("System.String"),
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };
            nameProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "nameField")));
            nameProperty.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "nameField"), new CodePropertySetValueReferenceExpression()));
            customerClass.Members.Add(nameProperty);

            // declare a method
            CodeMemberMethod helloMethod = new CodeMemberMethod()
            {
                Name = "SayHelloTo",
                ReturnType = new CodeTypeReference("System.String"),
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };

            // add method parameters
            helloMethod.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "name"));

            // add method statements
            var expression = new CodeSnippetExpression(@"""Hello "" + name +"", my name is"" + Name");
            helloMethod.Statements.Add(new CodeMethodReturnStatement(expression));

            // add method to class
            customerClass.Members.Add(helloMethod);

            //  define a namespace
            CodeNamespace demoNameSpace = new CodeNamespace("Demos");
            demoNameSpace.Imports.Add(new CodeNamespaceImport("System"));
            demoNameSpace.Imports.Add(new CodeNamespaceImport("System.Collections"));

            // add class to namespace
            demoNameSpace.Types.Add(customerClass);

            // CodeDOM object graph that models the source code to compile
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            // add demos namespace to the compile unit
            compileUnit.Namespaces.Add(demoNameSpace);

            Console.WriteLine("Generating CodeDOMFile.cs file.");

            // CSharpCode provider that will generate the file
            CSharpCodeProvider provider = new CSharpCodeProvider();
            string sourceFile = "..\\..\\OutputFiles\\CodeDOMFile.cs";
            using(StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                using (IndentedTextWriter tw = new IndentedTextWriter(sw, "    "))
                {
                    // Generate source code
                    provider.GenerateCodeFromCompileUnit(compileUnit, tw, new CodeGeneratorOptions());
                }
            }

            Console.WriteLine("\nEnded CodeDOM demo...");
        }

        /// <summary>
        /// StringBuilder demo, a mutable string class
        /// </summary>
        private static void StringBuilderDemo()
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
            sb.Remove(sb.Length-3, 3);
            Console.WriteLine(sb);

            // write out each character
            Console.WriteLine("Writing out each character");
            for (int i = 0; i < sb.Length; i++)
                Console.WriteLine(sb[i]);
        }

        private static void StringReaderStringWriterDemo()
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
        private static void StringOperationsDemo()
        {
            Console.WriteLine("String operations demo...\n");

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
    }
}
