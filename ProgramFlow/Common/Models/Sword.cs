using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    public class Sword : Weapon
    {
        public override int CalculateDamage(int modifier)
        {
            return base.CalculateDamage(modifier);
        }
        public override bool HasHit(int roll, int threshold)
        {
            return false;
        }
    }
}
