using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.TypesClasses
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

    public class ProfessorProfileData : ProfileData
    {
        public string AlmaMater { get; set; }

        /// <summary>
        /// Call constructor for empty constructor with values
        /// </summary>
        public ProfessorProfileData() :
            this("n/a", "n/a", "n/a")
        {

        }

        public ProfessorProfileData(string firstname, string lastName, string almaMater) :
            base(firstname, lastName)
        {
            AlmaMater = almaMater;
        }

        public override void Print()
        {
            base.Print();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
