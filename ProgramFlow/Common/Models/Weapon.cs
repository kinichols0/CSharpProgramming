using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    public abstract class Weapon
    {
        protected int damage;
        protected int range;

        // can be overridden
        public virtual int CalculateDamage(int modifier)
        {
            return damage * modifier;
        }

        // Must be implemented
        public abstract bool HasHit(int roll, int threshold);
    }
}
