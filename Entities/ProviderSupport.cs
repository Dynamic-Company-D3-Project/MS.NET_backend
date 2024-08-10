using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class ProviderSupport
{
    public long SupportId { get; set; }

    public string Description { get; set; } = null!;

    public string? Status { get; set; }

    public string SupportType { get; set; } = null!;

    public long? BookingId { get; set; }

    public long? ProviderId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Provider? Provider { get; set; }
}
