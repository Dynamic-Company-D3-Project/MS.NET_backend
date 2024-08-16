using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Order
{
    public long OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Description { get; set; }

    public decimal? OrderRate { get; set; }

    public string? Status { get; set; }

    public TimeSpan OrderTime { get; set; }

    public long ProviderId { get; set; }

    public int SubcategoryId { get; set; }

    public long UserId { get; set; }

    public long AddressId { get; set; }

    public virtual Provider Provider { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual Subcategory Subcategory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
