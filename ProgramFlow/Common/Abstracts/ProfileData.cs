using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Abstracts
{
    public abstract class ProfileData
    {
        public ProfileData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public virtual void Print()
        {
            Console.WriteLine("Profile Data\n" + this + "\n");
        }

        public override string ToString()
        {
            return $"First Name: {FirstName}\nLast Name: {LastName}";
        }
    }
}
