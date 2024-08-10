using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class UserSupport
{
    public int SupportId { get; set; }

    public string Description { get; set; } = null!;

    public int? Status { get; set; }

    public string Title { get; set; } = null!;

    public long? BookingId { get; set; }

    public long? UserId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual User? User { get; set; }
}
