using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public override string ToString()
        {
            return string.Format("[ CategoryId: {0}, CategoryName: {1} ]", this.CategoryId, this.CategoryName);
        }
    }
}
