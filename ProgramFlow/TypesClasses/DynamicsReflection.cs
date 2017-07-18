using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Models;
using CSharpProgramming.Common.Attributes;

namespace CSharpProgramming.TypesClasses
{
    public class DynamicsReflection
    {
        public static void AttributesAndReflectionDemo()
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
        public static void CodeDOMDemo()
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
            string sourceFile = "..\\..\\OutputFiles\\DynamicCode\\CodeDOMFile.cs";
            using (StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                using (IndentedTextWriter tw = new IndentedTextWriter(sw, "    "))
                {
                    // Generate source code
                    provider.GenerateCodeFromCompileUnit(compileUnit, tw, new CodeGeneratorOptions());
                }
            }

            Console.WriteLine("\nEnded CodeDOM demo...");
        }
    }
}
