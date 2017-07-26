using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    public struct Point3d
    {
        public static explicit operator Point2d(Point3d value)
        {
            return new Point2d(value.X, value.Y);
        }

        public static implicit operator Point3d(Point2d value)
        {
            return new Point3d(value.X, value.Y, 0d);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public Point3d(double x, double y, double z) : this()
        {

        }
    }
}
