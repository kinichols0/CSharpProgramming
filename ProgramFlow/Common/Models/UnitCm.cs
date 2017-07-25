using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CSharpProgramming.Common.Models
{
    /// <summary>
    /// Class implements an explicit cast operator to UnitFt
    /// and an implicit cast operator to double
    /// </summary>
    public class UnitCm
    {
        private double centimeters;

        public UnitCm(double cm)
        {
            centimeters = cm;
        }

        /// <summary>
        /// Allows an explicit cast from UnitCm to UnitFt
        /// UnitFt unit = (UnitFt)unitCm;
        /// </summary>
        /// <param name="ft"></param>
        public static explicit operator UnitFt(UnitCm cm)
        {
            return new UnitFt(cm.Centimeters / 30);
        }

        /// <summary>
        /// Allows an implicit cast from UnitCm to double
        /// </summary>
        /// <param name="cm"></param>
        public static implicit operator double(UnitCm cm)
        {
            return cm.Centimeters;
        }

        public double Centimeters
        {
            get
            {
                return centimeters;
            }

            set
            {
                centimeters = value;
            }
        }
    }
}
