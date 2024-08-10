using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Category
{
    public long Id { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Subcategory> Subcategories { get; } = new List<Subcategory>();
}
