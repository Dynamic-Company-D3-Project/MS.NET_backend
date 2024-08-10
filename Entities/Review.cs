using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Review
{
    public long ReviewId { get; set; }

    public int Rating { get; set; }

    public string? Reivew { get; set; }

    public DateTime? ReviewDate { get; set; }

    public long? OrderId { get; set; }

    public int? SubCategoryId { get; set; }

    public long? UserId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Subcategory? SubCategory { get; set; }

    public virtual User? User { get; set; }
}
