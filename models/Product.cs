using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ContosoPets.models;



public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? SupplierId { get; set; }

    [XmlIgnore]
    public int? CategoryId { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }

    [XmlIgnore]
    public virtual Category? Category { get; set; }

    public virtual IList<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
