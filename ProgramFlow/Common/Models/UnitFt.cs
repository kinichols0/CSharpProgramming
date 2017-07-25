using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    /// <summary>
    /// Class implements an explicit cast operator and
    /// overloaded operators
    /// </summary>
    public class UnitFt
    {
        private double feet;

        public UnitFt(double ft)
        {
            feet = ft;
        }

        /// <summary>
        /// Allows an explicit cast from UnitFt to UnitCm
        /// UnitCm unitCm = (UnitCm)unitFt
        /// </summary>
        /// <param name="unitFt"></param>
        public static explicit operator UnitCm(UnitFt unitFt)
        {
            return new UnitCm(unitFt.Feet * 30);
        }

        /// <summary>
        /// Overload the "+" operator to be able to add UnitFt objects
        /// </summary>
        /// <param name="ft1"></param>
        /// <param name="ft2"></param>
        /// <returns></returns>
        public static UnitFt operator +(UnitFt ft1, UnitFt ft2)
        {
            return new UnitFt(ft1.Feet + ft2.Feet);
        } 

        public double Feet
        {
            get
            {
                return feet;
            }

            set
            {
                feet = value;
            }
        }
    }
}
