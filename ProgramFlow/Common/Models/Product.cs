using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string ProductName { get; set; }

        public override string ToString()
        {
            return string.Format("[ ProductId: {0}, CategoryId: {1}, ProductName: {2} ]", ProductId, CategoryId, ProductName);
        }
    }
}
