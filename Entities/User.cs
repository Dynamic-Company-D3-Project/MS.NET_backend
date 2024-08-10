using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class User
{
    public long Id { get; set; }

    public DateTime? CreationTime { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int Age { get; set; }

    public string? ImagePath { get; set; }

    public ulong? IsDeleted { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<UserSupport> UserSupports { get; } = new List<UserSupport>();
}
