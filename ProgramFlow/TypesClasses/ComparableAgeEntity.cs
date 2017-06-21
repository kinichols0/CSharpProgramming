using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.TypesClasses
{
    /// <summary>
    /// IComparable implementation
    /// </summary>
    public class ComparableAgeEntity : IComparable
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public ComparableAgeEntity(string name, int age)
        {
            Name = name;
            Age = age;
        }

        /// <summary>
        /// "obj" is the ComparableEntity being compared to this ComparableEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;

            if (obj is ComparableAgeEntity incomingComparable)
                return this.Age.CompareTo(incomingComparable.Age);

            throw new ArgumentException("Object is not a ComparableEntity.");
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append("{ ");
            str.Append("Name:" + Name + ", ");
            str.Append("Age:" + Age + " }");
            return str.ToString();
        }
    }
}
