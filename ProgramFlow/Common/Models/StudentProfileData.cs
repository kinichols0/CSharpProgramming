using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Abstracts;
using CSharpProgramming.Common.Enums;

namespace CSharpProgramming.Common.Models
{
    public class StudentProfileData : ProfileData
    {
        public string Major { get; set; }

        public ClassStanding CurrentClass { get; set; }

        /// <summary>
        /// Call constructor for empty constructor with values
        /// </summary>
        public StudentProfileData() :
            this("n/a", "n/a", "Undecided", ClassStanding.Freshman)
        {

        }

        public StudentProfileData(string firstName, string lastName, string major, ClassStanding standing) :
            base(firstName, lastName)
        {
            Major = major;
            CurrentClass = standing;
        }

        public override void Print()
        {
            Console.WriteLine("Student Profile:\n" + this + "\n");
        }

        public override string ToString()
        {
            return string.Format("First Name: {0}\nLast Name: {1}\nDate of Birth: {2}\nMajor: {3}\nClass Standing: {4}",
                FirstName, LastName, DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/YYYY") : "", Major, CurrentClass.ToString());
        }
    }
}
