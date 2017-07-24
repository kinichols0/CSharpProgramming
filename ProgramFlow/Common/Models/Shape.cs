using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    internal class Shape
    {
        private double _height;
        private double _base;

        public Shape()
        {

        }

        public Shape(double height, double bs)
        {
            _height = height;
            _base = bs;
        }

        public double Base
        {
            get
            {
                return _base;
            }

            set
            {
                _base = value;
            }
        }

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public double CalcArea
        {
            get
            {
                return 0.5 * Base * Height;
            }
        }
    }
}
