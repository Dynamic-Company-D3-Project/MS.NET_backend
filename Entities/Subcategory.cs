using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Subcategory
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int IsVisible { get; set; }

    public DateTime? LastUpdated { get; set; }

    public double Price { get; set; }

    public int Rating { get; set; }

    public long CategoryId { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
