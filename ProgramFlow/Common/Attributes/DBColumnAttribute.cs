using System;

namespace CSharpProgramming.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DBColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public DBColumnAttribute() { }

        public DBColumnAttribute(string name)
        {
            Name = name;
        }
    }
}