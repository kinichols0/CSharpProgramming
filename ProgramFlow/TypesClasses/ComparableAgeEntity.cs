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

        /// <summary>
        /// Overridden equals message
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {   
            if(obj != null && obj is ComparableAgeEntity inObj)
                return inObj.Age == Age 
                    && inObj.Name == Name;
            return false;
        }

        /// <summary>
        /// Overridden GetHashCode method. Since Equals method was overriden
        /// then GetHashCode should be overridden to reflect the Equals method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (!string.IsNullOrEmpty(Name))
                return Age + Name.GetHashCode();
            return Age;
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
