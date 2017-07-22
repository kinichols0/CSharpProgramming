using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.Common.Attributes;

namespace CSharpProgramming.Common.Models
{
    [DBTable(Name = "dbo.Order")]
    public class Order
    {
        [DBColumn(Name = "OrderId")]
        public int Id { get; set; }

        [DBColumn(Name = "ProductId")]
        public int ItemId { get; set; }

        [DBColumn(Name = "CustomerId")]
        public int CustomerId { get; set; }

        [DBColumn(Name = "OrderDate")]
        public DateTime? OrderDate { get; set; }

        public override string ToString()
        {
            return string.Format("[ Id:{0}, ItemId:{1}, CustomerId:{2}, OrderPlacedDate:{3} ]", Id, CustomerId, ItemId, OrderDate.HasValue ? OrderDate.Value.ToString("d") : "");
        }
    }
}
