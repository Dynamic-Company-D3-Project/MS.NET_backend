using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Address
{
    public long AddressId { get; set; }

    public string? AddressType { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public long? HouseNo { get; set; }

    public long Pincode { get; set; }

    public string State { get; set; } = null!;

    public string? Street { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }
}
