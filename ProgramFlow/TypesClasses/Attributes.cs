using System;
using System.Collections;

namespace CSharpProgramming.TypesClasses
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DBColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public DBColumnAttribute() { }

        public DBColumnAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DBTableAttribute : Attribute
    {
        public string Name { get; set; }

        public DBTableAttribute()
        {

        }

        public DBTableAttribute(string name)
        {
            Name = name;
        }
    }

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
        public DateTime OrderPlacedDate { get; set; }

        public override string ToString()
        {
            return string.Format("[ Id:{0}, ItemId:{1}, CustomerId:{2}, OrderPlacedDate:{3} ]", Id, CustomerId, ItemId, OrderPlacedDate.ToString("d"));
        }
    }
}