using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramFlow.TaskDemos.Models
{
    public class Shape
    {
        public double Base { get; set; }

        public double Height { get; set; }

        public double CalcArea
        {
            get
            {
                return 0.5 * Base * Height;
            }
        }
    }
}
