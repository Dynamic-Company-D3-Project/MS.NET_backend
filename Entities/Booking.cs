using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WEBAPI.Entities;

public partial class Booking
{
    public long BookingId { get; set; }

    public DateTime BookingDate { get; set; }

    public string? Status { get; set; }

    public TimeSpan BookingTime { get; set; }

    public long ?ProviderId { get; set; }

    public int SubcategoryId { get; set; }

    public long UserId { get; set; }

    public long AddressId { get; set; }

    public virtual Provider? Provider { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProviderSupport> ProviderSupports { get; } = new List<ProviderSupport>();

    public virtual Subcategory Subcategory { get; set; } = null!;

    //public virtual Address Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UserSupport> UserSupports { get; } = new List<UserSupport>();
}
