using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Abstracts;

namespace CSharpProgramming.Common.Models
{
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
