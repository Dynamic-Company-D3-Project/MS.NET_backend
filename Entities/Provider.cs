using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Provider
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

    public string? City { get; set; }

    public string? Country { get; set; }

    public ulong? IsDeleted { get; set; }

    public string? ProviderImagePath { get; set; }

    public long? ZipCode { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<ProviderSupport> ProviderSupports { get; } = new List<ProviderSupport>();
}
