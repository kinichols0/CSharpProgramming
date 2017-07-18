using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DBTableAttribute : Attribute
    {
        public string Name { get; set; }

        public DBTableAttribute()
        {

        }

        public DBTableAttribute(string name)
        {
            Name = name;
        }
    }
}
