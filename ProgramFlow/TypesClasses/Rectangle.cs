using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.TypesClasses
{
    public struct Rectangle
    {
        public int length;

        public int width;

        public Rectangle(int length, int width)
        {
            this.length = length;
            this.width = width;
        }

        public void DoubleRectangle()
        {
            this.length += this.length;
            this.width += this.width;
        }
    }
}
