using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ContosoPets.models;



public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    
    public virtual List<Product> Products { get; } = new List<Product>();
}
